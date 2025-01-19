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

namespace TherapiCareTest.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_CustomerService)]
    public class CustomerServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerServices.ToListAsync());
        }

        // GET: CustomerServices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
               .FirstOrDefaultAsync(m => m.CustServiceId == id);
            if (customerService == null)
            {
                return NotFound();
            }

            return View(customerService);
        }

        // GET: CustomerServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,reportStatus")] Models.CustomerService customerService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerService);
        }

        // GET: CustomerServices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices.FindAsync(id);
            if (customerService == null)
            {
                return NotFound();
            }
            return View(customerService);
        }

        // POST: CustomerServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,reportStatus")] Models.CustomerService customerService)
        {
            if (id != customerService.CustServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerServiceExists(customerService.CustServiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerService);
        }

        // GET: CustomerServices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(m => m.CustServiceId == id);
            if (customerService == null)
            {
                return NotFound();
            }

            return View(customerService);
        }

        // POST: CustomerServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customerService = await _context.CustomerServices.FindAsync(id);
            if (customerService != null)
            {
                _context.CustomerServices.Remove(customerService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerServiceExists(string id)
        {
            return _context.CustomerServices.Any(e => e.CustServiceId == id);
        }

        // GET: CustomerServices/ApproveReport
        public async Task<IActionResult> ApproveReport()
        {
            var reports = await _context.Reports
                //.Where(r => r.reportStatus == "Pending")
                .ToListAsync();

            return View(reports);
        }

        // POST: CustomerServices/ApproveReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveReport(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report != null)
            {
                report.reportStatus = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ApproveReport", "CustomerServices");
        }

        // POST: CustomerServices/RejectReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectReport(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report != null)
            {
                report.reportStatus = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ApproveReport", "CustomerServices");
        }

    }
}
