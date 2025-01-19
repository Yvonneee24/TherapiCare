using Microsoft.AspNetCore.Mvc.Rendering;
using TherapiCareTest.Models;

namespace TherapiCareTest.ViewModels
{
    public class AdminScheduleVM
    {
        public Schedule Schedule { get; set; }
        public string StudentName { get; set; }
        public string ProgramName { get; set; }
        public string SessionName { get; set; }
        public string TherapistName { get; set; }
        public List<SelectListItem> TherapistList { get; set; }
    }
}
