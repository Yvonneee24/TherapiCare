using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class ParentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ParentsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
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

            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            if (parent == null)
            {
                return RedirectToAction("Upsert");
            }

            var model = new ParentVM
            {
                ParentId = parent.ParentId,
                Name = parent.Name,
                City = parent.City,
                Postcode = parent.Postcode,
                State = parent.State,
                Address = parent.Address,
                Occupation = parent.Occupation,
                HouseholdIncome = parent.HouseholdIncome, 
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

            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            var model = new ParentVM();

            if (parent != null)
            {
                model = new ParentVM
                {
                    ParentId = parent.ParentId,
                    Name = parent.Name,
                    City = parent.City,
                    Postcode = parent.Postcode,
                    State = parent.State,
                    Address = parent.Address,
                    Occupation = parent.Occupation,
                    HouseholdIncome = parent.HouseholdIncome,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
            }
            else
            {
                model = new ParentVM
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
        public async Task<IActionResult> Upsert(ParentVM model)
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

                var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);

                if (parent == null)
                {
                    parent = new Parent
                    {
                        ParentId = model.ParentId,
                        Name = model.Name,
                        City = model.City,
                        Postcode = model.Postcode,
                        State = model.State,
                        Address = model.Address,
                        Occupation = model.Occupation,
                        HouseholdIncome = model.HouseholdIncome, 
                        ApplicationUserId = user.Id
                    };
                    _context.Parents.Add(parent);
                }
                else
                {
                    parent.Name = model.Name;
                    parent.City = model.City;
                    parent.Postcode = model.Postcode;
                    parent.State = model.State;
                    parent.Address = model.Address;
                    parent.Occupation = model.Occupation;
                    parent.HouseholdIncome = model.HouseholdIncome;
                    _context.Parents.Update(parent);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
