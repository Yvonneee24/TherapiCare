using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Therapi.Utility;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;
using TherapiCareTest.ViewModel;

namespace TherapiCareTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AdminListController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }


        public async Task<IActionResult> ListOfStudent()
        {
            var students = _context.Students.ToList(); 
            return View(students);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students
                .Include(s => s.Parent)
                .Include(s => s.ProgramStudents)
                .ThenInclude(ps => ps.TherapyProgram)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> GenerateStudentPdf()
        {
            var students = await (from ps in _context.ProgramStudents
                                  join s in _context.Students on ps.StudentId equals s.Id
                                  join t in _context.TherapyPrograms on ps.ProgramId equals t.Id
                                  join p in _context.Parents on s.ParentId equals p.ParentId
                                  select new StudentViewModel
                                  {
                                      Id = s.Id,
                                      Name = s.Name,
                                      DOB = s.DOB,
                                      Gender = s.Gender,
                                      programId = ps.ProgramId,
                                      programName = t.Name,
                                      parentId = s.ParentId,
                                      parentName = p.Name
                                  }).ToListAsync();
            return new ViewAsPdf("StudentListPdf", students)
            {
                FileName = "StudentList.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        public async Task<IActionResult> ListOfParent()
        {
            var parents = await _context.Parents
                                    .Include(p => p.Students)
                                    .ToListAsync();
            return View(parents);

        }

        public async Task<IActionResult> GenerateParentPdf()
        {
            var students = await (from ps in _context.ProgramStudents
                                  join s in _context.Students on ps.StudentId equals s.Id
                                  join t in _context.TherapyPrograms on ps.ProgramId equals t.Id
                                  join p in _context.Parents on s.ParentId equals p.ParentId
                                  select new ParentViewModel
                                  {
                                      studentId = s.Id,
                                      childrenName = s.Name,
                                      childrenGender = s.Gender,
                                      programId = ps.ProgramId,
                                      programName = t.Name,
                                      parentId = s.ParentId,
                                      parentName = p.Name
                                  }).ToListAsync();
            return new ViewAsPdf("ParentListPdf", students)
            {
                FileName = "ParentList.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
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
