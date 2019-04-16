﻿using System;
using System.Collections.Generic;

namespace Parleo.BLL.Models.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string AccountImage { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public bool Gender { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Email { get; set; }

        public ICollection<MiniatureModel> CreatedEvents { get; set; }

        public ICollection<UserLanguageModel> Languages { get; set; }

        public ICollection<MiniatureModel> Friends { get; set; }

        public ICollection<MiniatureModel> AttendingEvents { get; set; }
    }
}
