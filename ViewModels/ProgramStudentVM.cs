using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TherapiCareTest.Models;

namespace TherapiCareTest.ViewModels
{
    public class ProgramStudentVM
    {
        public ProgramStudent ProgramStudent { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ProgramList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> StudentList { get; set; }
    }
}

