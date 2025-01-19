using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
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

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

            if (admin == null)
            {
                return RedirectToAction("Upsert");
            }

            var model = new AdminVM
            {
                AdminId = admin.AdminId,
                Name = admin.Name,
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

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

            var model = new AdminVM();

            if (admin != null)
            {
                model = new AdminVM
                {
                    AdminId = admin.AdminId,
                    Name = admin.Name,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
            }
            else
            {
                model = new AdminVM
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
        public async Task<IActionResult> Upsert(AdminVM model)
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

                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

                if (admin == null)
                {
                    admin = new Models.Admin
                    {
                        AdminId = model.AdminId,
                        Name = model.Name,
                        ApplicationUserId = user.Id
                    };
                    _context.Admins.Add(admin);
                }
                else
                {
                    admin.Name = model.Name;
                    _context.Admins.Update(admin);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
