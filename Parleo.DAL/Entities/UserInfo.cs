﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parleo.DAL.Entities
{
    public class UserInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Birthdate { get; set; }

        public bool Gender { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal Longitude { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual UserAuth UserAuth { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<UserLanguage> Languages { get; set; }

        public virtual ICollection<UserFriends> Friends { get; set; }

        public virtual ICollection<UserFriends> InFriends { get; set; }
    }
}
