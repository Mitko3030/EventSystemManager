using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSystemManager.Data.Classes
{
    public class Event
    {
        public string? ImageUrl { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        [Required]
        public int VenueId { get; set; }

        [ForeignKey(nameof(VenueId))]
        public Venue? Venue { get; set; }
    }
}