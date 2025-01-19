using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]
    public class TherapistsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TherapistsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            { 
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var therapist = await _context.Therapists.FirstOrDefaultAsync(t => t.ApplicationUserId == user.Id);

            if (therapist == null)
            {
                return RedirectToAction("Upsert");
            }

            var model = new TherapistVM
            {
                TherapistId = therapist.TherapistId,
                Name = therapist.Name,
                AppUserId = user.Id,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return View(model);
        }

        public async Task<IActionResult> Upsert()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var therapist = await _context.Therapists.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

            var model = new TherapistVM();

            if (therapist != null)
            {
                model = new TherapistVM
                {
                    TherapistId = therapist.TherapistId,
                    Name = therapist.Name,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
            }
            else
            {
                model = new TherapistVM
                {
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TherapistVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User); 

                if (user == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var emailResult = await _userManager.UpdateAsync(user);
                if (!emailResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update email or phone number.");
                    return View(model);
                }

                var therapist = await _context.Therapists.FirstOrDefaultAsync(t => t.ApplicationUserId == user.Id);

                if (therapist == null)
                {
                    therapist = new Models.Therapist
                    {
                        TherapistId = model.TherapistId,
                        Name = model.Name,
                        ApplicationUserId = user.Id
                    };
                    _context.Therapists.Add(therapist);
                }
                else
                {
                    therapist.Name = model.Name;
                    _context.Therapists.Update(therapist);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
