﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parleo.DAL.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public string AccountImage { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Birthdate { get; set; }

        public bool Gender { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal Longitude { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Credentials Credentials { get; set; }

        public AccountToken AccountToken { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }

        public ICollection<UserLanguage> Languages { get; set; }

        public ICollection<UserFriends> Friends { get; set; }

        public ICollection<UserEvent> AttendingEvents { get; set; }

        public ICollection<Chat> CreatedChats { get; set; }

        public ICollection<ChatUser> Chats { get; set; }
        
        public ICollection<UserHobby> Hobbies { get; set; }
    }
}
