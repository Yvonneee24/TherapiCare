using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using Microsoft.AspNetCore.Authorization;
using Therapi.Utility;

namespace TherapiCareTest.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve the logged-in user's ID
            var userId = _userManager.GetUserId(User);

            // Retrieve the parent information
            var parent = await _context.Parents
                                       .Include(p => p.ApplicationUser)
                                       .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (parent == null)
            {
                return NotFound("Parent not found");
            }

            // Retrieve registered programs
            var registeredPrograms = await _context.TherapyPrograms
                .OrderByDescending(p => p.Id)
                .Take(3)
                .ToListAsync();

            // Retrieve schedules
            var schedules = await _context.Schedules
                .Where(s => s.ProgramStudent.Student.ParentId == parent.ParentId)
                .Include(s => s.Slot)
                    .ThenInclude(slot => slot.Therapist)
                .Include(s => s.ProgramStudent)
                    .ThenInclude(ps => ps.Student)
                .Include(s => s.ProgramStudent.TherapyProgram)
                .OrderByDescending(s => s.Slot.StartTime)
                .Take(3)
                .ToListAsync();

            // Retrieve announcements
            var announcements = await _context.Announcements
                .Where(a => a.IsHidden == false)
                .OrderByDescending(a => a.a_date)
                .Take(3)
                .ToListAsync();

            // Retrieve only the children of the logged-in parent
            var students = await _context.Students
               .Where(s => s.ParentId == parent.ParentId)
               .OrderByDescending(s => s.Id)
               .Take(3)
               .ToListAsync();

            // Pass data to the view
            ViewBag.RegisteredPrograms = registeredPrograms;
            ViewBag.Schedules = schedules;
            ViewBag.Announcements = announcements;
            ViewBag.Students = students;
            ViewBag.ParentName = parent.Name;

            return View();
        }
    }
}
