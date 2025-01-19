using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;

namespace TherapiCareTest.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class AnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parent/Announcements
        public async Task<IActionResult> Index()
        {
            var announcements = await _context.Announcements.ToListAsync();
            return View(announcements);
        }

        // GET: Parent/Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            // Mark the announcement as viewed
            announcement.IsViewed = true;
            _context.SaveChanges(); // Save changes to update IsViewed status

            return View(announcement);
        }
    }
}
