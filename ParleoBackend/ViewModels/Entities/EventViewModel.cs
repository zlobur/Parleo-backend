using System;
using System.Collections.Generic;

namespace ParleoBackend.ViewModels.Entities
{
    public class EventViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxParticipants { get; set; }    
        
        public decimal Latitude { get; set; }  
        
        public decimal Longitude { get; set; }

        public bool IsFinished { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public MiniatureViewModel Creator { get; set; }

        public LanguageViewModel Language { get; set; }

        public ICollection<MiniatureViewModel> Participants { get; set; }
    }
}
