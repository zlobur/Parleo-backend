﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parleo.DAL.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxParticipants { get; set; }
        // public Guid ChatId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(11, 8)")]
        public decimal Longitude { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndDate { get; set; }

        public UserInfo Creator { get; set; }
        public Language Language { get; set; }
    }
}