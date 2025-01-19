using System.ComponentModel.DataAnnotations;
using TherapiCareTest.Models;
namespace TherapiCareTest.ViewModels
{
    public class BillingUploadVM
    {
        [Required]
        public IFormFile PaymentReceiptFile { get; set; }

        [Required]
        public int BillingId { get; set; }

        public string? ProgramName { get; set; }
        public decimal? ProgramPrice { get; set; }
        public string? StudentName { get; set; }

    }


}
