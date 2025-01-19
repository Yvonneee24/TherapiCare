using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Linq;
using System.Threading.Tasks;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SlotController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlotController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int sessionId)
        {
            var session = await _context.Sessions
               .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
            {
                return NotFound();
            }

            var slots = await _context.Slots
                .Include(s => s.Therapist)
                .Where(s => s.SessionId == sessionId)
                .ToListAsync();

            var viewModel = new SessionSlotVM
            {
                SessionId = sessionId,
                SessionName = session.Name,
                Slots = slots
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Create(int sessionId)
        {
            var session = await _context.Sessions
               .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
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

            var viewModel = new SlotVM
            {
                Slot = new Slot
                {
                    SessionId = sessionId
                },
                TherapistList = therapistList
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlotVM viewModel)
        {
            var startTime = viewModel.Slot.StartTime.TimeOfDay;
            var endTime = viewModel.Slot.EndTime.TimeOfDay;

            Console.WriteLine($"Start Time: {startTime}, End Time: {endTime}");

            if (startTime < TimeSpan.FromHours(8) || endTime > TimeSpan.FromHours(17))
            {
                ModelState.AddModelError("", "Time must be between 8am and 5pm.");
                Console.WriteLine("Validation failed: Time must be between 8am and 5pm.");
                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Slot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { sessionId = viewModel.Slot.SessionId });
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


        public async Task<IActionResult> Edit(int id, int sessionId)
        {

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
            var therapistList = await _context.Therapists
                .Select(t => new SelectListItem
                {
                    Value = t.TherapistId,
                    Text = t.Name
                })
                .ToListAsync();

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

            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == viewModel.Slot.SessionId);
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Slot);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { sessionId = viewModel.Slot.SessionId });
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

        #region API CALLS
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}