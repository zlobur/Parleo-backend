﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parleo.DAL.Models.Entities;
using Parleo.DAL.Interfaces;
using Parleo.DAL.Models.Filters;
using Parleo.DAL.Models.Pages;
using System.Collections.Generic;
using Parleo.DAL.Helpers;

namespace Parleo.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppContext _context;

        private readonly int _defaultPageSize = 25;

        public UsersRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(User entity)
        {
            _context.User.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result != 0;
        }

        public async Task<bool> DisableAsync(Guid id)
        {
            //TODO: update the IsEnabled field on the user when it appears on the model
            return true;
        }

        public async Task<Page<User>> GetPageAsync(UserFilter userFilter, User user)
        {
            double latitude = (double)user.Latitude,
                   longitude = (double)user.Longitude;

            // hack for correct first user's output int filter list
            // without it first user will have no lang, hobbies etc...
            await _context.User
                .Include(u => u.Credentials)
                .Include(u => u.Languages)
                .Include(u => u.Hobbies)
                    .ThenInclude(uh => uh.Hobby)
                        .ThenInclude(h => h.Category)
                .FirstOrDefaultAsync();

            IEnumerable<User> users = await _context.User
                .Where(u => u.Id != user.Id)
                .Where(u => userFilter.Gender != null ?
                    u.Gender == userFilter.Gender : true)
                .Where(u => (userFilter.Languages != null &&
                             userFilter.Languages.Count() != 0) ?
                    userFilter.Languages.Any(fl => u.Languages.Any(
                        ul => ul.LanguageCode == fl &&
                            LevelInRange(ul, userFilter.MinLevel))) : true)
                .Where(u => (userFilter.MaxAge != null) ?
                    GetAge(u.Birthdate) <= userFilter.MaxAge : true)
                .Where(u => (userFilter.MinAge != null) ?
                    GetAge(u.Birthdate) >= userFilter.MinAge : true)
                .Include(u => u.Languages)
                .ThenInclude(ul => ul.Language)
                .Include(u => u.Credentials)
                .Include(u => u.Hobbies)
                    .ThenInclude(h => h.Hobby)
                .ToListAsync();

            users = users
                .Where(u => (userFilter.MaxDistance != null) ?
                    LocationHelper.GetDistanceBetween((double)u.Longitude, (double)u.Latitude,
                    longitude, latitude) <= userFilter.MaxDistance : true)
                .ToArray();

            int totalAmount = users.Count();

            if (userFilter.PageSize == null)
            {
                userFilter.PageSize = _defaultPageSize;
            }

            return new Page<User>()
            {
                Entities = users.OrderBy(u => u.CreatedAt)
                    .SkipWhile(m => m.CreatedAt > userFilter.TimeStamp)
                    .Skip((userFilter.PageNumber - 1) * userFilter.PageSize.Value)
                    .Take(userFilter.PageSize.Value).ToList(),
                PageNumber = userFilter.PageNumber,
                PageSize = userFilter.PageSize.Value,
                TotalAmount = totalAmount,
                TimeStamp = DateTimeOffset.UtcNow
            };
        }

        public async Task<User> GetAsync(Guid id)
        {
            var result = await _context.User.Include(u => u.Credentials)
                .Include(u => u.Languages)
                .Include(u => u.Friends)
                .Include(u => u.Hobbies)
                    .ThenInclude(uh => uh.Hobby)
                        .ThenInclude(h => h.Category)
                .Include(u => u.CreatedEvents)
                .Include(u => u.AttendingEvents)
                    .ThenInclude(ue => ue.Event)
                .FirstOrDefaultAsync(user => user.Id == id);

            if (result == null)
            {
                return null;
            }

            result.Friends = await _context.UserFriends.Where(u => u.UserFromId == id)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            _context.User.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result != 0;
        }

        public async Task<Credentials> FindByEmailAsync(string email)
        {
            return await _context.Credentials.Include(c => c.User)
                .FirstOrDefaultAsync(с => с.Email == email);
        }

        public async Task AddAccountTokenAsync(AccountToken accountToken)
        {
            _context.AccountToken.Add(accountToken);
            await _context.SaveChangesAsync();
        }

        public async Task<AccountToken> DeleteAccountTokenByUserIdAsync(Guid userId)
        {
            AccountToken accountToken = await _context.AccountToken.Include(c => c.User)
                .FirstOrDefaultAsync(с => с.UserId == userId);

            if (accountToken != null)
            {
                _context.AccountToken.Remove(accountToken);
                _context.SaveChanges();
            }

            return accountToken;
        }

        public async Task InsertAccountImageNameAsync(string imageName, Guid userId)
        {
            User user = new User()
            {
                Id = userId,
                AccountImage = imageName
            };
            _context.Entry(user).Property(x => x.AccountImage).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task ClearExpiredAccountTokensAsync()
        {
            IEnumerable<AccountToken> expiredTokens = await _context.AccountToken.ToListAsync();
            _context.User.RemoveRange(
                _context.User.Where(user =>
                    expiredTokens.Any(token => token.UserId == user.Id && token.ExpirationDate < DateTime.Now))
            );
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUserHasTokenAsync(string email)
        {
            AccountToken token = await _context.AccountToken
                .Include(c => c.User)
                .Include(c => c.User.Credentials)
                .FirstOrDefaultAsync(act => act.User.Credentials.Email == email);
            return token != null;
        }

        public async Task<bool> AddFriendAsync(Guid userFromId, Guid userToId)
        {
            var friendship = await GetExistingFriendship(userFromId, userToId);
            if (friendship != null && friendship.Status == (int)FriendStatus.Deleted)
            {
                friendship.Status = (int)FriendStatus.InFriends;
                await _context.SaveChangesAsync();
                return true;
            }

            UserFriends userFriends = await CreateUserFriendsEntityAsync(userFromId, userToId);
            if (_context.UserFriends.Any(uf => // if friend is already added
                Equals(userFromId, uf.UserFromId) && Equals(userToId, uf.UserToId)))
            {
                return true; // nothing changes
            }

            try
            {
                _context.UserFriends.Add(userFriends);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<Page<User>> GetUserFriendsAsync(PageRequest pageRequest, Guid userId)
        {
            var friends = await _context.UserFriends.Where(u => u.UserFromId == userId && u.Status == 1)
                .Select(friend => friend.UserTo)
                .Include(u => u.Credentials)
                .ToListAsync();

            if(pageRequest.PageSize == null)
            {
                pageRequest.PageSize = _defaultPageSize;
            }

            return new Page<User>()
            {
                Entities = friends.OrderBy(u => u.CreatedAt)
                .SkipWhile(m => m.CreatedAt > pageRequest.TimeStamp)
                    .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize.Value)
                    .Take(pageRequest.PageSize.Value).ToList(),
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize.Value,
                TotalAmount = friends.Count,
                TimeStamp = DateTimeOffset.UtcNow
            };
        }


        public async Task<bool> RemoveFriendAsync(Guid userFromId, Guid userToId)
        {
            UserFriends friend;
            try
            {
                friend = await _context.UserFriends
                    .Where(u => u.UserFromId == userFromId && u.UserToId == userToId)
                    .FirstOrDefaultAsync();
                friend.Status = (int)FriendStatus.Deleted;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
                
            return true;
        }

        #region Private methods

        private async Task<UserFriends> CreateUserFriendsEntityAsync(Guid userFromId, Guid userToId)
        {
            return new UserFriends()
            {
                UserFromId = userFromId,
                UserFrom = await _context.User.FirstOrDefaultAsync(user => user.Id == userFromId),
                UserToId = userToId,
                UserTo = await _context.User.FirstOrDefaultAsync(user => user.Id == userToId),
                Status = (int)FriendStatus.InFriends
            };
        }

        private async Task<UserFriends> GetExistingFriendship(Guid userFromId, Guid userToId)
        {
            return await _context.UserFriends
                    .Where(u => u.UserFromId == userFromId && u.UserToId == userToId)
                    .FirstOrDefaultAsync();
        }

        private bool LevelInRange(UserLanguage userLanguage, int? minLevel)
        {
            return minLevel != null ? userLanguage.Level >= minLevel : true;
        }

        private int GetAge(DateTimeOffset birth)
        {
            int age = new DateTime(
                DateTime.Now.Subtract(birth.DateTime).Ticks).Year;

            return DateTime.Now.DayOfYear < birth.DayOfYear ? age - 1 : age;
        }

        #endregion
    }
}
