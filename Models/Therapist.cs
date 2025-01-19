using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition;

namespace TherapiCareTest.Models
{
    public class Therapist
    {
        [Key]
        public string? TherapistId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<Slot> Slots { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}
