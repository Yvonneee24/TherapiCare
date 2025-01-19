using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TherapiCareTest.Data.Enum;

namespace TherapiCareTest.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Passport { get; set; }
        [Required]
        public string? Nasionality { get; set; }
        [Required]
        public string? Race { get; set; }
        [Required]
        public string? BirthPlace { get; set; }
        [Required]
        public string? Religion { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Student), "ValidateDOB")]
        public DateTime DOB { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        public int? Age { get; set; }

        public string? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Parent? Parent { get; set; }

        public ICollection<Report>? Reports { get; set; } = new List<Report>();
        public ICollection<ProgramStudent>? ProgramStudents { get; set; }

        public string? Pediatricians { get; set; }
        public string? RecommendedByHospital { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeadlineDiagnoseByDoctor { get; set; }
        public string? DiagnosisCondition { get; set; }
        public bool OccupationalTherapy { get; set; }
        public bool SpeechTherapy { get; set; }
        public string? Others { get; set; }

        public static ValidationResult ValidateDOB(DateTime dob, ValidationContext context)
        {
            if (dob > DateTime.Now)
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }
            if (dob < DateTime.Now.AddYears(-12))
            {
                return new ValidationResult("DOB must be within the last 12 years.");
            }
            return ValidationResult.Success;
        }
    }
}
