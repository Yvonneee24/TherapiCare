using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;

namespace TherapiCareTest.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            // Retrieve the parent information
            var parent = await _context.Parents
                                       .Include(p => p.ApplicationUser)
                                       .Include(p => p.Students)
                                       .ThenInclude(s => s.Reports)
                                       .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);


            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            var allReports = parent.Students
                .SelectMany(s => s.Reports)
                .ToList();

            return View(allReports);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }
    }
}
