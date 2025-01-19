using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using TherapiCareTest.Models;

namespace TherapiCareTest.ViewModels
{
    public class ScheduleVM
    {
        public Schedule Schedule { get; set; }
        public string? StudentName { get; set; }
        public string? ProgramName { get; set; }
        public string? SessionName { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SlotList { get; set; }
        public int SelectedSlotId { get; set; }
        public decimal? Price { get; set; }
    }
}
