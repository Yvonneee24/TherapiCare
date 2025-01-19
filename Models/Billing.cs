using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TherapiCareTest.Data.Enum;


namespace TherapiCareTest.Models
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        [DataType(DataType.Currency)]
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? PaymentReceipt { get; set; }
        [ValidateNever]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
        [Required]
        public PaymentStatus? PaymentStatus { get; set; }
        [ValidateNever]
        public int TherapyProgramId { get; set; }
        [ForeignKey("TherapyProgramId")]
        public TherapyProgram? TherapyProgram { get; set; }

        [ValidateNever]
        public string? ParentId { get; set; }
        [ForeignKey("ParentId")]

        public Parent? Parent { get; set; }
        public Billing()
        {

            PaymentStatus = Data.Enum.PaymentStatus.PENDING;
        }

    }
}
