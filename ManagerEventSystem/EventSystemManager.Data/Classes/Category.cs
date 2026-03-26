using System.Collections.Generic;

namespace EventSystemManager.Data.Classes
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Navigation property
        public List<Event> Events { get; set; } = new List<Event>();
    }
}