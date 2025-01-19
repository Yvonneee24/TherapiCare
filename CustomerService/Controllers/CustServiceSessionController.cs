using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using System.Threading.Tasks;
using TherapiCareTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Therapi.Utility;

namespace TherapiCareTest.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_CustomerService)]
    public class CustServiceSessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustServiceSessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int programId)
        {
            var program = await _context.TherapyPrograms
                .FirstOrDefaultAsync(tp => tp.Id == programId);

            if (program == null)
            {
                return NotFound();
            }

            var sessions = await _context.Sessions
                .Where(s => s.ProgramId == programId)
                .ToListAsync();

            var viewModel = new ProgramSessionsVM
            {
                ProgramId = programId,
                ProgramName = program.Name,
                Sessions = sessions
            };

            return View(viewModel);
        }

        // GET: Session/Create
        public async Task<IActionResult> Create(int programId)
        {
            var program = await _context.TherapyPrograms
                .FirstOrDefaultAsync(tp => tp.Id == programId);

            if (program == null)
            {
                return NotFound();
            }

            var viewModel = new CreateSessionVM
            {
                Session = new Session
                {
                    ProgramId = programId
                },
                ProgramName = program.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSessionVM viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { programId = viewModel.Session.ProgramId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            var program = await _context.TherapyPrograms
                .FirstOrDefaultAsync(tp => tp.Id == session.ProgramId);

            if (program == null)
            {
                return NotFound();
            }

            var viewModel = new CreateSessionVM
            {
                Session = session,
                ProgramName = program.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateSessionVM viewModel)
        {
            if (id != viewModel.Session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(viewModel.Session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { programId = viewModel.Session.ProgramId });
            }

            var program = await _context.TherapyPrograms
                .FirstOrDefaultAsync(tp => tp.Id == viewModel.Session.ProgramId);

            if (program == null)
            {
                return NotFound();
            }

            viewModel.ProgramName = program.Name;
            return View(viewModel);
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }

        #region API CALLS

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return Ok();
        }
        #endregion
    }
}
