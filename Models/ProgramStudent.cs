using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class ProgramStudent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; } = DateTime.UtcNow;
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        [ValidateNever]
        public TherapyProgram TherapyProgram { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Student Student { get; set; }
    }
}
