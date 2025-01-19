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

namespace TherapiCareTest.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class CustServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustService/CustServices
        public IActionResult Index()
        {
            var parents = _context.Parents.ToList();
            return View(parents);
        }


    }
}
