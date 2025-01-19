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
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_CustomerService)]
    public class CustServiceTherapyProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustServiceTherapyProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<TherapyProgram> objTherapyProgramList = _unitOfWork.TherapyProgram.GetAll().ToList();
            return View(objTherapyProgramList);
        }
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TherapyProgram? therapyProgramFromDb = _unitOfWork.TherapyProgram.Get(u => u.Id == id);

            if (therapyProgramFromDb == null)
            {
                return NotFound();
            }
            return View(therapyProgramFromDb);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TherapyProgram obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TherapyProgram.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Program created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TherapyProgram? therapyProgramFromDb = _unitOfWork.TherapyProgram.Get(u => u.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (therapyProgramFromDb == null)
            {
                return NotFound();
            }
            return View(therapyProgramFromDb);
        }
        [HttpPost]
        public IActionResult Edit(TherapyProgram obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TherapyProgram.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Program updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    TherapyProgram? therapyProgramFromDb = _unitOfWork.TherapyProgram.Get(u => u.Id == id);

        //    if (therapyProgramFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(therapyProgramFromDb);
        //}


        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    TherapyProgram? obj = _unitOfWork.TherapyProgram.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.TherapyProgram.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Program deleted successfully";
        //    return RedirectToAction("Index");
        //}
        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.TherapyProgram.Get(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.TherapyProgram.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Program deleted successfully" });
        }
        #endregion  
    }
}
