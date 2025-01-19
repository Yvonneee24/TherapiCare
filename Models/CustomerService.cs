using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class CustomerService
    {
        [Key]
        public string? CustServiceId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("Report")]
        [DisplayName("Status Report")]
        public int reportStatus { get; set; }
    }
}
