using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TherapiCareTest.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Therapi.Utility;

namespace TherapiCareTest.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (parent == null)
            {
                return NotFound();
            }

            var students = _context.Students.Where(s => s.ParentId == parent.ParentId).ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Passport,Nasionality,Race,BirthPlace,Religion,DOB,Gender,Age,Pediatricians,RecommendedByHospital,DeadlineDiagnoseByDoctor,DiagnosisCondition,OccupationalTherapy,SpeechTherapy,Others")] Student student)
        {
            var userId = _userManager.GetUserId(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (parent == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (student.DOB > DateTime.Now)
                {
                    ModelState.AddModelError("DOB", "Date of Birth cannot be in the future.");
                    return View(student);
                }
                student.ParentId = parent.ParentId;
                _context.Add(student);
                await _context.SaveChangesAsync();
                TempData["success"] = "Children created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (parent == null || student.ParentId != parent.ParentId)
            {
                return Unauthorized();
            }

            student.ParentId = parent.ParentId;

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Passport,Nasionality,Race,BirthPlace,Religion,DOB,Gender,Age,ParentId,Pediatricians,RecommendedByHospital,DeadlineDiagnoseByDoctor,DiagnosisCondition,OccupationalTherapy,SpeechTherapy,Others")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (parent == null || student.ParentId != parent.ParentId)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                if (student.DOB > DateTime.Now)
                {
                    ModelState.AddModelError("DOB", "Date of Birth cannot be in the future.");
                    return View(student);
                }

                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Children edited successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Parent) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (parent == null || student.ParentId != parent.ParentId)
            {
                return Unauthorized();
            }

            student.ParentId = parent.ParentId;

            return View(student);
        }


        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
