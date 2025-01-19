using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        
        public int SlotId { get; set; }
        [ForeignKey("SlotId")]
        [ValidateNever]
        public Slot Slot { get; set; }
        public int ProgramStudentId { get; set; }
        [ForeignKey("ProgramStudentId")]
        [ValidateNever]
        public ProgramStudent ProgramStudent { get; set; }

    }
}
