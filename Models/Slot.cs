using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class Slot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
        public string TherapistId { get; set; }

        [ForeignKey("TherapistId")]
        [ValidateNever]
        public Therapist Therapist { get; set; }
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        [ValidateNever]
        public Session Session { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}
