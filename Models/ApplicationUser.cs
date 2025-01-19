using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Admin? Admin { get; set; }
        public Therapist? Therapist { get; set; }
        public CustomerService? CustomerService { get; set; }
        public Parent? Parent { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
