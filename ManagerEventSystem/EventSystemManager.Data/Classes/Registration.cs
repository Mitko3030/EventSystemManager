using System;

namespace EventSystemManager.Data.Classes
{
    public class Registration
    {
        public int Id { get; set; }

        // Foreign key to Event
        public int EventId { get; set; }
        public Event? Event { get; set; }  // ← changed to nullable

        // Foreign key to Participant
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }  // ← changed to nullable

        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
    }
}