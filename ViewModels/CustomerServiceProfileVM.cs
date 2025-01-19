using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.ViewModels
{
    public class CustomerServiceProfileVM
    {
        [Required]
        public string CustServiceId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? AppUserId { get; set; }
        [RegularExpression(@"^(?:\+60|0)[1-9](?:-[0-9]{7,8}|[0-9]{7,8})$", ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
    }
}
