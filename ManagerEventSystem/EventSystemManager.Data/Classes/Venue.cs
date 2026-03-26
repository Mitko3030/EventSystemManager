using System.Collections.Generic;

namespace EventSystemManager.Data.Classes
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int Capacity { get; set; }

        // Navigation property
        public List<Event> Events { get; set; } = new List<Event>();
    }
}