using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(applicationUserId))
            {
                return Unauthorized();
            }

            var therapist = await _context.Therapists
                .SingleOrDefaultAsync(t => t.ApplicationUserId == applicationUserId);

            if (therapist == null)
            {
                return NotFound("Therapist not found.");
            }

            var slots = await _context.Slots
                .Where(s => s.TherapistId == therapist.TherapistId)
                .Include(s => s.Therapist)
                .Include(s => s.Schedules)
                .ThenInclude(sch => sch.ProgramStudent)
                .ThenInclude(ps => ps.Student)
                .Include(s => s.Schedules)
                .ThenInclude(sch => sch.ProgramStudent.TherapyProgram)
                .ToListAsync();

            var viewModel = slots.Select(slot => new TherapistSlotVM
            {
                Slot = slot,
                StudentName = slot.Schedules.FirstOrDefault()?.ProgramStudent.Student.Name,
                ProgramName = slot.Schedules.FirstOrDefault()?.ProgramStudent.TherapyProgram.Name,
                TherapistName = slot.Therapist.Name
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id, int sessionId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var therapistList = await _context.Therapists
                .Select(t => new SelectListItem
                {
                    Value = t.TherapistId,
                    Text = t.Name
                })
                .ToListAsync();

            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }
            var session = await _context.Sessions
               .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
            {
                return NotFound();
            }

            var viewModel = new SlotVM
            {
                Slot = slot,
                TherapistList = therapistList
            };
            return View(viewModel);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SlotVM viewModel)
        {
            if (id != viewModel.Slot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Slot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlotExists(viewModel.Slot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            viewModel.TherapistList = await _context.Therapists
                .Select(t => new SelectListItem
                {
                    Value = t.TherapistId,
                    Text = t.Name
                })
                .ToListAsync();

            return View(viewModel);
        }
        private bool SlotExists(int id)
        {
            return _context.Slots.Any(e => e.Id == id);
        }
    }
}
