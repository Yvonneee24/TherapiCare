using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Data.Enum;

namespace TherapiCareTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // Calculate total students
            var totalStudents = await _context.Students.CountAsync();

            // Calculate total sessions
            var totalSessions = await _context.Sessions.CountAsync();

            // Retrieve all billings
            var billings = await _context.Billings.ToListAsync();

            // Calculate total sales
            //var totalSales = billings.Sum(b => Convert.ToDecimal(b.Amount));

            var totalSales = billings
    .Where(b => b.PaymentStatus == PaymentStatus.PAID)
    .Sum(b => Convert.ToDecimal(b.Amount));

            // Calculate sales this month
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            //var salesThisMonth = billings
            //    .Where(b => b.Date.Month == currentMonth && b.Date.Year == currentYear)
            //    .Sum(b => Convert.ToDecimal(b.Amount));
            var salesThisMonth = billings
    .Where(b => b.PaymentStatus == PaymentStatus.PAID && b.Date.Month == currentMonth && b.Date.Year == currentYear)
    .Sum(b => Convert.ToDecimal(b.Amount));

            var announcements = await _context.Announcements
        .OrderByDescending(a => a.a_date)
        .Take(3) // Limit to the first three announcements
        .ToListAsync();

            // Retrieve the three newest programs using Id as a proxy for creation date
            var programs = await _context.TherapyPrograms
                .OrderByDescending(p => p.Id) // Assuming there is an Id field
                .Take(3)
                .ToListAsync();

            // Pass data to the view using ViewBag
            ViewBag.TotalStudents = totalStudents;
            ViewBag.TotalSessions = totalSessions;
            ViewBag.TotalSales = totalSales;
            ViewBag.SalesThisMonth = salesThisMonth;
            ViewBag.NewestAnnouncements = announcements;
            ViewBag.NewestPrograms = programs;


            return View();
        }
    }
}
