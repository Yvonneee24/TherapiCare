using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository;
using TherapiCareTest.Repository.IRepository;
using TherapiCareTest.ViewModel;

namespace TherapiCareTest.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]
    public class TherapistListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public TherapistListController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        public async Task<IActionResult> ListOfStudent()
        {
            var students = await _context.Students.Include(s => s.Parent).ToListAsync();
            return View(students);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Student> studList = _unitOfWork.Student.GetAll().ToList();
            return Json(new { data = studList });
        }
        #endregion
    }
}
