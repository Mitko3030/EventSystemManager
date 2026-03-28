using System.ComponentModel.DataAnnotations;

namespace EventSystemManager.Data.Classes
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

      
        public List<Registration> Registrations { get; set; } = new List<Registration>();

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}