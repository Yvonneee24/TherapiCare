using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Data;

namespace TherapiCareTest.Models
{
    public class TherapyProgram
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Objective { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal WeekdayPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal WeekendPrice { get; set; }
        public ICollection<Session>? Sessions { get; set; }

    }
}
