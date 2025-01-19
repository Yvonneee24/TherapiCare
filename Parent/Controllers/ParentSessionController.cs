using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using System.Linq;
using System.Threading.Tasks;
using TherapiCareTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Therapi.Utility;

namespace TherapiCareTest.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class ParentSessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentSessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParentSession/Index
        public async Task<IActionResult> Index(int programStudentId)
        {
            var programStudent = await _context.ProgramStudents
                .Include(ps => ps.TherapyProgram)
                .FirstOrDefaultAsync(ps => ps.Id == programStudentId);

            if (programStudent == null)
            {
                return NotFound();
            }

            var sessions = await _context.Sessions
                .Where(s => s.ProgramId == programStudent.ProgramId)
                .ToListAsync();

            var viewModel = new ProgramSessionsVM
            {
                ProgramId = programStudent.ProgramId,
                ProgramName = programStudent.TherapyProgram.Name,
                Sessions = sessions,
                ProgramStudentId = programStudentId
            };

            return View(viewModel);
        }
    }
}
