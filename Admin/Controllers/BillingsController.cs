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
using TherapiCareTest.Data.Enum;
using TherapiCareTest.Models;
using Rotativa.AspNetCore;
using TherapiCareTest.Services;


namespace TherapiCareTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BillingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public BillingsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Admin/Billings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Billings.Include(b => b.Parent).Include(b => b.Student).Include(b => b.TherapyProgram);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Billings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                .Include(b => b.Parent)
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        [HttpGet]
        public IActionResult Filter(int? selectedMonth, PaymentStatus? paymentStatus)
        {
            IQueryable<Billing> query = _context.Billings
                .Include(b => b.Parent)
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram);

            if (selectedMonth.HasValue)
            {
                query = query.Where(b => b.Date.Month == selectedMonth.Value);
            }
            if (paymentStatus.HasValue)
            {
                query = query.Where(b => b.PaymentStatus == paymentStatus.Value);
            }

            var filteredBillings = query.ToList();
            decimal totalAmount = filteredBillings.Sum(b => Convert.ToDecimal(b.Amount));

            ViewData["TotalAmount"] = totalAmount;
            ViewData["SelectedMonth"] = selectedMonth;
            ViewData["SelectedPaymentStatus"] = paymentStatus;
            return PartialView("_BillingTablePartial", filteredBillings);
        }

        public IActionResult GeneratePdf(int? selectedMonth, PaymentStatus? paymentStatus)
        {
            // Fetch billings data with related entities included
            IQueryable<Billing> billingsQuery = _context.Billings
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .Include(b => b.Parent);

            // Filter by selected month if provided
            if (selectedMonth.HasValue)
            {
                billingsQuery = billingsQuery.Where(b => b.Date.Month == selectedMonth.Value);
            }

            // Filter by payment status if provided
            if (paymentStatus.HasValue)
            {
                billingsQuery = billingsQuery.Where(b => b.PaymentStatus == paymentStatus.Value);
            }

            var billings = billingsQuery.ToList();

            return new ViewAsPdf("BillingPdf", billings)
            {
                FileName = "billing_report.pdf", // Set the file name for the PDF download
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        // GET: Admin/Billings/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Name");
            return View();
        }

        // POST: Admin/Billings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Date,PaymentReceipt,StudentId,PaymentStatus,TherapyProgramId,ParentId")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name", billing.ParentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", billing.StudentId);
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Name", billing.TherapyProgramId);
            return View(billing);
        }

        // GET: Admin/Billings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "Name", billing.ParentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", billing.StudentId);
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Name", billing.TherapyProgramId);
            return View(billing);
        }

        // POST: Admin/Billings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentStatus,Amount,Date,PaymentReceipt,StudentId,TherapyProgramId,ParentId")] Billing billing)
        {
            if (id != billing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBilling = await _context.Billings
                        .Include(b => b.Parent)
                        .Include(b => b.Student)
                        .Include(b => b.TherapyProgram)
                        .FirstOrDefaultAsync(b => b.Id == id);

                    if (existingBilling == null)
                    {
                        return NotFound();
                    }

                    // Only update the payment status
                    existingBilling.PaymentStatus = billing.PaymentStatus;

                    _context.Update(existingBilling);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingExists(billing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Re-populate the required fields before returning the view
            var billingToEdit = await _context.Billings
                .Include(b => b.Parent)
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (billingToEdit == null)
            {
                return NotFound();
            }

            ViewBag.StudentId = new SelectList(_context.Students, "Id", "Name", billingToEdit.StudentId);
            ViewBag.TherapyProgramId = new SelectList(_context.TherapyPrograms, "Id", "Name", billingToEdit.TherapyProgramId);
            ViewBag.ParentId = new SelectList(_context.Parents, "ParentId", "Name", billingToEdit.ParentId);

            return View(billingToEdit);
        }





        // GET: Admin/Billings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                .Include(b => b.Parent)
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // POST: Admin/Billings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billing = await _context.Billings.FindAsync(id);
            if (billing != null)
            {
                _context.Billings.Remove(billing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GeneratePdfDetail(int id)
        {
            var billing = _context.Billings
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .Include(b => b.Parent)
                .FirstOrDefault(b => b.Id == id); // Fetch the specific billing item by ID

            if (billing == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("BillingPdfDetails", billing)
            {
                FileName = "billing_reportDetails.pdf", // Set the file name for the PDF download
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendPaymentReminder(int billingId)
        {
            var billing = await _context.Billings
                .Include(b => b.Parent)
                .ThenInclude(p => p.ApplicationUser)
                .Include(b => b.TherapyProgram)
                .FirstOrDefaultAsync(b => b.Id == billingId);

            if (billing == null || billing.Parent == null || billing.Parent.ApplicationUser == null)
            {
                return NotFound();
            }

            var parentEmail = billing.Parent.ApplicationUser.Email;
            var subject = "Payment Reminder";
            var message = $"Dear {billing.Parent.Name},<br><br>" +
                          $"This is a reminder that your payment for the therapy program {billing.TherapyProgram.Name} is due.<br>" +
                          $"Amount Due: {billing.Amount}<br>" +
                          $"Due Date: {billing.Date.ToShortDateString()}<br><br>" +
                          "Please make the payment at your earliest convenience.<br><br>" +
                          "Thank you,<br>TherapiCare";

            await _emailService.SendEmailAsync(parentEmail, subject, message);

            TempData["SuccessMessage"] = "Payment reminder sent successfully.";
            return RedirectToAction(nameof(Index));
        }

        private bool BillingExists(int id)
        {
            return _context.Billings.Any(e => e.Id == id);
        }
    }
}
