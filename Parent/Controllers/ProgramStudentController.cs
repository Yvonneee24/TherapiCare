using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Therapi.Utility;

namespace TherapiCareTest.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class ProgramStudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgramStudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUser.Id);

            if (parent == null)
            {
                return NotFound("Parent record not found.");
            }

            var programStudents = await _context.ProgramStudents
                .Include(ps => ps.TherapyProgram)
                .Include(ps => ps.Student)
                .Where(ps => ps.Student.ParentId == parent.ParentId)
                .ToListAsync();

            return View(programStudents);
        }

        
        public async Task<IActionResult> Create(int? studentId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUser.Id);

            if (parent == null)
            {
                return NotFound("Parent record not found.");
            }

            var viewModel = new ProgramStudentVM
            {
                ProgramList = _context.TherapyPrograms
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    }),

                StudentList = _context.Students
                    .Where(s => s.ParentId == parent.ParentId)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }),

                ProgramStudent = new ProgramStudent()
            };

            if (studentId.HasValue)
            {
                viewModel.ProgramStudent.StudentId = studentId.Value;
            }

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramStudentVM viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.ProgramStudent.TherapyProgram = await _context.TherapyPrograms
                    .FirstOrDefaultAsync(p => p.Id == viewModel.ProgramStudent.ProgramId);

                viewModel.ProgramStudent.Student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == viewModel.ProgramStudent.StudentId);

                var malaysiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // Malaysia's time zone
                viewModel.ProgramStudent.Date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, malaysiaTimeZone);

                _context.Add(viewModel.ProgramStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUser.Id);

            viewModel.ProgramList = _context.TherapyPrograms
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });

            viewModel.StudentList = _context.Students
                .Where(s => s.ParentId == parent.ParentId)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                });

            return View(viewModel);
        }


        private bool ProgramStudentExists(int id)
        {
            return _context.ProgramStudents.Any(e => e.Id == id);
        }
    }
}
