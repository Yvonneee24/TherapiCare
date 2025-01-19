using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class Admin
    {
        [Key]
        public string? AdminId { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
