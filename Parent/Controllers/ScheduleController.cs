using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;
using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Therapi.Utility;

namespace TherapiCareTest.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ScheduleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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

            var schedules = await _context.Schedules
        .Where(s => s.ProgramStudent.Student.ParentId == parent.ParentId) 
        .Include(s => s.Slot)
            .ThenInclude(slot => slot.Session)
        .Include(s => s.ProgramStudent)
            .ThenInclude(ps => ps.Student)
        .Include(s => s.ProgramStudent.TherapyProgram)
        .Include(s => s.Slot.Therapist)
        .ToListAsync();

            var scheduleVMs = schedules.Select(s => new ScheduleVM
            {
                Schedule = s,
                StudentName = s.ProgramStudent.Student.Name,
                ProgramName = s.ProgramStudent.TherapyProgram.Name,
                SessionName = s.Slot.Session.Name,
                Price = (s.Slot.StartTime.DayOfWeek == DayOfWeek.Saturday || s.Slot.StartTime.DayOfWeek == DayOfWeek.Friday) ?
                s.ProgramStudent.TherapyProgram.WeekendPrice : s.ProgramStudent.TherapyProgram.WeekdayPrice
            }).ToList();

            return View(scheduleVMs);
        }

        public async Task<IActionResult> Create(int programStudentId, int sessionId)
        {
            var programStudent = await _context.ProgramStudents
                .Include(ps => ps.Student)
                .Include(ps => ps.TherapyProgram)
                .FirstOrDefaultAsync(ps => ps.Id == programStudentId);

            if (programStudent == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
            {
                return NotFound();
            }

            var slots = await _context.Slots
        .Where(s => s.SessionId == sessionId)
        .Include(s => s.Therapist)
        .ToListAsync();

            var scheduledSlotIds = await _context.Schedules
                .Where(s => s.Slot.SessionId == sessionId)
                .Select(s => s.SlotId)
                .ToListAsync();
            var slotList = slots
                .Where(s => !scheduledSlotIds.Contains(s.Id))
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"<strong>Date: </strong> {s.StartTime:yyyy-MM-dd} <br/> " +
                   $"<strong>Time: </strong> {s.StartTime:hh:mm tt} - {s.EndTime:hh:mm tt} <br/> " +
                   $"<strong>Therapist</strong>: {s.Therapist.Name} <br/> " +
                   $"<strong>Price</strong>: {(s.StartTime.DayOfWeek == DayOfWeek.Saturday || s.StartTime.DayOfWeek == DayOfWeek.Friday ? programStudent.TherapyProgram.WeekendPrice : programStudent.TherapyProgram.WeekdayPrice):C}"
                })
                .ToList();

            if (slotList.Count == 0)
            {
                ViewData["NoSlotsMessage"] = "No slots available.";
            }

            var viewModel = new ScheduleVM
            {
                Schedule = new Schedule
                {
                    ProgramStudentId = programStudentId
                },
                StudentName = programStudent.Student.Name,
                ProgramName = programStudent.TherapyProgram.Name,
                SessionName = session.Name,
                SlotList = slotList
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var slot = await _context.Slots
                    .Include(s => s.Therapist)
                    .FirstOrDefaultAsync(s => s.Id == viewModel.SelectedSlotId);

                if (slot == null)
                {
                    return NotFound();
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

                if (parent == null)
                {
                    return NotFound("Parent not found.");
                }

                var programStudent = await _context.ProgramStudents
                    .Include(ps => ps.TherapyProgram)
                    .FirstOrDefaultAsync(ps => ps.Id == viewModel.Schedule.ProgramStudentId);

                if (programStudent == null)
                {
                    return NotFound();
                }

                var schedule = new Schedule
                {
                    ProgramStudentId = viewModel.Schedule.ProgramStudentId,
                    SlotId = viewModel.SelectedSlotId
                };
                _context.Add(schedule);
                await _context.SaveChangesAsync();

                var billing = new Billing
                {
                    Date = slot.StartTime,
                    TherapyProgramId = programStudent.ProgramId,
                    StudentId = programStudent.StudentId,
                    ParentId = parent.ParentId,
                    Amount = IsWeekend(slot.StartTime) ? programStudent.TherapyProgram.WeekendPrice : programStudent.TherapyProgram.WeekdayPrice,
                    PaymentReceipt = string.Empty 
                };

                _context.Billings.Add(billing);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == viewModel.Schedule.Slot.SessionId);

            if (session == null)
            {
                return NotFound();
            }

            viewModel.SlotList = await _context.Slots
                .Where(s => s.SessionId == session.Id)
                .Include(s => s.Therapist)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.StartTime:yyyy-MM-dd HH:mm}  -  {s.EndTime:HH:mm} (Therapist: {s.Therapist.Name})"
                })
                .ToListAsync();

            return View(viewModel);
        }

        private bool IsWeekend(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Edit(int id, int sessionId)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var programStudent = await _context.ProgramStudents
                .Include(ps => ps.Student)
                .Include(ps => ps.TherapyProgram)
                .FirstOrDefaultAsync(ps => ps.Id == schedule.ProgramStudentId);

            if (programStudent == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
               .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
            {
                return NotFound();
            }

            var slots = await _context.Slots
       .Where(s => s.SessionId == sessionId)
       .Include(s => s.Therapist)
       .ToListAsync();

            var scheduledSlotIds = await _context.Schedules
                .Where(s => s.Slot.SessionId == sessionId)
                .Select(s => s.SlotId)
                .ToListAsync();
            var slotList = slots
                .Where(s => !scheduledSlotIds.Contains(s.Id))
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"<strong>Date: </strong> {s.StartTime:yyyy-MM-dd} <br/> " +
                   $"<strong>Time: </strong> {s.StartTime:hh:mm tt} - {s.EndTime:hh:mm tt} <br/> " +
                   $"<strong>Therapist</strong>: {s.Therapist.Name} <br/> " +
                   $"<strong>Price</strong>: {(s.StartTime.DayOfWeek == DayOfWeek.Saturday || s.StartTime.DayOfWeek == DayOfWeek.Sunday ? programStudent.TherapyProgram.WeekendPrice : programStudent.TherapyProgram.WeekdayPrice):C}"
                })
                .ToList();

            if (slotList.Count == 0)
            {
                ViewData["NoSlotsMessage"] = "No slots available.";
            }

            var viewModel = new ScheduleVM
            {
                Schedule = schedule,
                StudentName = programStudent.Student.Name,
                ProgramName = programStudent.TherapyProgram.Name,
                SessionName = session.Name,
                SlotList = slotList
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ScheduleVM viewModel)
        {
            if (id != viewModel.Schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var schedule = await _context.Schedules
                        .Include(s => s.Slot)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (schedule == null)
                    {
                        return NotFound();
                    }

                    var newSlot = await _context.Slots
                        .Include(s => s.Therapist)
                        .FirstOrDefaultAsync(s => s.Id == viewModel.SelectedSlotId);

                    if (newSlot == null)
                    {
                        return NotFound();
                    }

                    schedule.SlotId = viewModel.SelectedSlotId;
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(viewModel.Schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var programStudent = await _context.ProgramStudents
                .Include(ps => ps.Student)
                .Include(ps => ps.TherapyProgram)
                .FirstOrDefaultAsync(ps => ps.Id == viewModel.Schedule.ProgramStudentId);

            if (programStudent != null)
            {
                viewModel.StudentName = programStudent.Student.Name;
                viewModel.ProgramName = programStudent.TherapyProgram.Name;
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == viewModel.Schedule.Slot.SessionId);

            if (session != null)
            {
                viewModel.SessionName = session.Name;
            }

            viewModel.SlotList = await _context.Slots
                .Where(s => s.SessionId == viewModel.Schedule.Slot.SessionId)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.StartTime:yyyy-MM-dd HH:mm} - {s.EndTime:HH:mm}"
                })
                .ToListAsync();

            return View(viewModel);
        }


    }
}
