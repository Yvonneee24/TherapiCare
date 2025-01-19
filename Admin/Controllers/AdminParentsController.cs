using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;

namespace TherapiCareTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminParentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminParentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents
                .FirstOrDefaultAsync(m => m.ParentId == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }


        private bool ParentExists(string id)
        {
            return _context.Parents.Any(e => e.ParentId == id);
        }
    }
}
