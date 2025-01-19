using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Therapi.Utility;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;
using TherapiCareTest.ViewModels;

namespace TherapiCareTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            if (applicationUser == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            var roles = _userManager.GetRolesAsync(applicationUser).Result;
            var roleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            var roleManagementVM = new RoleManagementVM
            {
                ApplicationUser = applicationUser,
                RoleList = roleList,
                SelectedRole = roles.FirstOrDefault() // Optionally, set the selected role in case you want to preselect the current role
            };

            return View(roleManagementVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM model)
        {
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == model.ApplicationUser.Id);
            if (user == null)
            {
                return NotFound(); 
            }

            var currentRoles = _userManager.GetRolesAsync(user).Result;
            var removeResult = _userManager.RemoveFromRolesAsync(user, currentRoles).Result;
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove current roles.");
            }

            var addResult = _userManager.AddToRoleAsync(user, model.SelectedRole).Result;
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add new role.");
            }

            model.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            _unitOfWork.Save();

            TempData["Success"] = "Role updated successfully.";
            return RedirectToAction("Index");
        }



        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> userList = _unitOfWork.ApplicationUser.GetAll().ToList();

            foreach (var user in userList)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Role = roles.FirstOrDefault();
            }

            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var userObj = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (userObj == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }

            if (userObj.LockoutEnd > DateTime.Now)
            {
                userObj.LockoutEnd = DateTime.Now;
            }
            else
            {
                userObj.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _unitOfWork.ApplicationUser.Update(userObj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation successful" });
        }

        #endregion
    }
}