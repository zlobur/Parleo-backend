﻿using Parleo.DAL.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parleo.DAL.Interfaces
{
    public interface IUtilityRepository
    {
        Task<IReadOnlyCollection<Language>> GetLanguagesAsync();

        Task<IReadOnlyCollection<Hobby>> GetHobbiesAsync();

        Task<bool> IsLanguagesExistsAsync(ICollection<Language> languages);

        Task<bool> IsHobbiesExistsAsync(ICollection<Hobby> hobbies);
    }
}
