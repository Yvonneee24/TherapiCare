using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        public int? ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        [ValidateNever]
        public TherapyProgram TherapyProgram { get; set; }
    }
}
