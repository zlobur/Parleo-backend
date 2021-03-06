﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parleo.DAL.Models.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxParticipants { get; set; }

        public Guid? ChatId { get; set; }

        public string Image { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal Longitude { get; set; }

        public bool IsFinished { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public Guid CreatorId { get; set; }

        public User Creator { get; set; }

        public string LanguageCode { get; set; }

        public Language Language { get; set; }

        public ICollection<UserEvent> Participants { get; set; }

        public Chat Chat { get; set; }
    }
}
