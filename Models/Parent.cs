using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.Models
{
    public class Parent
    {
        [Key]
        public string? ParentId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Postcode { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Occupation { get; set; }
        [Required]
        public string? HouseholdIncome { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
