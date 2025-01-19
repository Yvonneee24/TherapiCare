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

namespace TherapiCareTest.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class TherapyProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TherapyProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<TherapyProgram> objTherapyProgramList = _unitOfWork.TherapyProgram.GetAll().ToList();
            return View(objTherapyProgramList);
        }

    }
}
