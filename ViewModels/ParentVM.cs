using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.ViewModels
{
    public class ParentVM
    {
        [Required]
        public string ParentId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string? HouseholdIncome { get; set; }

        [Required]
        public string Occupation { get; set; }
        public string? AppUserId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
