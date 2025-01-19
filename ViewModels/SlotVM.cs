using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using TherapiCareTest.Models;
using System;
using TherapiCareTest.Data;

namespace TherapiCareTest.ViewModels
{
    public class SlotVM
    {

        public Slot Slot { get; set; }

        [ValidateNever]
        public List<SelectListItem>? TherapistList { get; set; }
    }
}
