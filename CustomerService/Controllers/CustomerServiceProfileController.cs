using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_CustomerService)]
    public class CustomerServiceProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CustomerServiceProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
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

            var customerservice = await _context.CustomerServices.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

            if (customerservice == null)
            {
                return RedirectToAction("Upsert");
            }

            var model = new CustomerServiceProfileVM
            {
                CustServiceId = customerservice.CustServiceId,
                Name = customerservice.Name,
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

            var customerservice = await _context.CustomerServices.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

            var model = new CustomerServiceProfileVM();

            if (customerservice != null)
            {
                model = new CustomerServiceProfileVM
                {
                    CustServiceId = customerservice.CustServiceId,
                    Name = customerservice.Name,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
            }
            else
            {
                model = new CustomerServiceProfileVM
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
        public async Task<IActionResult> Upsert(CustomerServiceProfileVM model)
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

                var customerservice = await _context.CustomerServices.FirstOrDefaultAsync(a => a.ApplicationUserId == user.Id);

                if (customerservice == null)
                {
                    customerservice = new Models.CustomerService
                    {
                        CustServiceId = model.CustServiceId,
                        Name = model.Name,
                        ApplicationUserId = user.Id
                    };
                    _context.CustomerServices.Add(customerservice);
                }
                else
                {
                    customerservice.Name = model.Name;
                    _context.CustomerServices.Update(customerservice);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
