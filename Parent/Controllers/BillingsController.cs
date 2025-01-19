using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Data;
using TherapiCareTest.Data.Enum;
using TherapiCareTest.Models;
using System.IO;
using TherapiCareTest.ViewModels;
using Rotativa.AspNetCore;

namespace TherapiCareTest.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles ="Parent")]
    public class BillingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillingsController(ApplicationDbContext context, IWebHostEnvironment webHost, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHost = webHost; 
            _userManager = userManager;
        }

        // GET: Parent/Billings
        public async Task<IActionResult> Index()
        {
            // Get current user's id
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            // Query parent record to get ParentId
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUser.Id);

            if (parent == null)
            {
                return NotFound("Parent record not found.");
            }
            // Query billings where ParentId matches userId
            var parentBillings = await _context.Billings
                .Include(b => b.Parent)
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .Where(b => b.ParentId == parent.ParentId)
                .ToListAsync();

            // Check for pending bills and set ViewData
            var pendingBills = parentBillings.Any(b => b.PaymentStatus == TherapiCareTest.Data.Enum.PaymentStatus.PENDING);
            ViewData["HasPendingBills"] = pendingBills;

            

            return View(parentBillings);
        }

        // GET: Parent/Billing/Filter
        [HttpGet]
        public async Task<IActionResult> Filter(int? selectedMonth, PaymentStatus? paymentStatus)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            IQueryable<Billing> query = _context.Billings
                .Include(b => b.Parent)
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .Where(b => b.ParentId == parent.ParentId);

            if (selectedMonth.HasValue)
            {
                query = query.Where(b => b.Date.Month == selectedMonth.Value);
            }

            if (paymentStatus.HasValue)
            {
                query = query.Where(b => b.PaymentStatus == paymentStatus.Value);
            }

            var filteredBillings = await query.ToListAsync();
            decimal totalAmount = (decimal)filteredBillings.Sum(b => b.Amount);

            ViewData["TotalAmount"] = totalAmount;
            return PartialView("_BillingTablePartial", filteredBillings);
        }


        // GET: Parent/Billings/Details/5
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

        // GET: Parent/Billing/UploadReceipt/5
        public async Task<IActionResult> UploadReceipt(int billingId)
        {
            var billing = await _context.Billings
                .Include(b => b.TherapyProgram)
                .Include(b => b.Student)
                .FirstOrDefaultAsync(b => b.Id == billingId);

            if (billing == null)
            {
                return NotFound("Billing record not found.");
            }

            var viewModel = new BillingUploadVM
            {
                BillingId = billing.Id,
                ProgramName = billing.TherapyProgram.Name,
                ProgramPrice = billing.Amount,
                StudentName = billing.Student.Name
            };

            return View(viewModel);
        }

        // POST: Parent/Billing/UploadReceipt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadReceipt(BillingUploadVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var billing = await _context.Billings.FindAsync(viewModel.BillingId);
                if (billing == null)
                {
                    return NotFound("Billing record not found.");
                }

                if (viewModel.PaymentReceiptFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.PaymentReceiptFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.PaymentReceiptFile.CopyToAsync(fileStream);
                    }

                    billing.PaymentReceipt = uniqueFileName;
                    _context.Update(billing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewModel);
        }

        

        // GET: Parent/Billings/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "BirthPlace");
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Description");
            return View();
        }

        // POST: Parent/Billings/Create
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
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", billing.ParentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "BirthPlace", billing.StudentId);
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Description", billing.TherapyProgramId);
            return View(billing);
        }

        // GET: Parent/Billings/Edit/5
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
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", billing.ParentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "BirthPlace", billing.StudentId);
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Description", billing.TherapyProgramId);
            return View(billing);
        }

        // POST: Parent/Billings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Date,PaymentReceipt,StudentId,PaymentStatus,TherapyProgramId,ParentId")] Billing billing)
        {
            if (id != billing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billing);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", billing.ParentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "BirthPlace", billing.StudentId);
            ViewData["TherapyProgramId"] = new SelectList(_context.TherapyPrograms, "Id", "Description", billing.TherapyProgramId);
            return View(billing);
        }

        // GET: Parent/Billings/Delete/5
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

        // POST: Parent/Billings/Delete/5
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

        public IActionResult GeneratePdfDetailPaid(int id)
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

            return new ViewAsPdf("BillingPdfDetailsPaid", billing)
            {
                FileName = "billing_reportDetailsPaid.pdf", // Set the file name for the PDF download
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        public async Task<IActionResult> GeneratePdf(int? selectedMonth, PaymentStatus? paymentStatus)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            // Fetch billings data with related entities included
            IQueryable<Billing> billingsQuery = _context.Billings
                .Include(b => b.Student)
                .Include(b => b.TherapyProgram)
                .Include(b => b.Parent)
                .Where(b => b.ParentId == parent.ParentId);

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

        private bool BillingExists(int id)
        {
            return _context.Billings.Any(e => e.Id == id);
        }
    }
}
