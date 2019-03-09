﻿using Parleo.BLL.Models;
using Parleo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parleo.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<UserAuth> AuthenticateAsync(string email, string password);

        Task<IEnumerable<UserInfo>> GetUsersPageAsync(int number);

        Task<UserInfo> GetUserByIdAsync(Guid id);

        Task<UserInfo> CreateUserAsync(AuthorizationModel authorizationModel);

        Task<bool> UpdateUserAsync(UserInfo user, string password = null);

        Task<bool> DisableUserAsync(Guid id);

    }
}