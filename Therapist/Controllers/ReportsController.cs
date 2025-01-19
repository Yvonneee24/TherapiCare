using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using TherapiCareTest.Data;
using TherapiCareTest.Models;

namespace TherapiCareTest.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    public class ReportsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ReportsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public async Task<IActionResult> Create(int studentId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null && await _userManager.IsInRoleAsync(user, "Therapist"))
            {
                var therapist = await _context.Therapists.FirstOrDefaultAsync(t => t.ApplicationUserId == user.Id);
                if (therapist != null)
                {
                    var model = new Report
                    {
                        staffId = therapist.TherapistId,
                        studentId = student.Id,
                        studentName = student.Name,
                        therapistIncharge = therapist.Name
                    };

                    ViewBag.StudentId = student.Id;
                    ViewBag.StudentName = student.Name;
                    return View(model);
                }
            }

            return NotFound("Therapist not found or user is not authorized as a therapist.");
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int studentId, [Bind("Id,Title,staffId,studentName,studentId,Date,numberOfSessionsAttended,treatment,reportStatus,enterByHimself,enterWithPrompting,difficultiesSeparateWithParents,withCryingRefuse,greetingWithPrompt,greetingByHimself,mute,refuseToEnter,subjectiveAssessmentNotes,rangeOfMotion,muscleTone,muscleStrength,muscleEndurance,jointMobility,trunkControlBalance,grossMotorFunctionStanding,grossMotorFunctionCrawling,grossMotorFunctionWalking,grossMotorFunctionJumping,grossMotorFunctionBroadJump,grossMotorFunctionKickBall,grossMotorFunctionThrowBall,fineMotorFunctionGraspRelease,fineMotorFunctionReaching,fineMotorFunctionPutBlock,fineMotorFunctionScribbles,fineMotorFunctionCubes,fineMotorFunctionMature,fineMotorFunctionImmature,fineMotorFunctionImitate,fineMotorFunctionCopying,objectiveAssessmentNotes,tactile,auditory,visual,olfactory,gustatory,vestibular,proprioception,alphabet,numbers,shapes,colors,memoryFunction,attention,concentration,problemSolving,writingSkill,cognitiveProgressNote,independent,withSupervision,maximumAssistance,toiletTrained,moneyManagement,timeConcept,foldingClothes,hangingClothes,sweepFloor,makingDrinks,prepareSimpleFood,usePhone,occupationRemark,temperedTantrum,manipulative,easilyDistracted,passive,cooperative,isolation,reluctant,repatitivePrompting,verbalPrompting,physicalPrompting,eyeContactPerson,eyeContactObject,initiateAnswerQuestion,verbalRespond,voiceClarify,facialExpression,bodyLanguage,takingTurn,sharing,stayInGrouping,academicPerformance,analysisProblem,txPlan,therapistIncharge")] Report report)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListOfStudent", "TherapistList", new { area = "Therapist" });
            }

            ViewBag.StudentId = student.Id;
            ViewBag.StudentName = student.Name;
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,staffId,studentName,studentId,Date,numberOfSessionsAttended,treatment,reportStatus,enterByHimself,enterWithPrompting,difficultiesSeparateWithParents,withCryingRefuse,greetingWithPrompt,greetingByHimself,mute,refuseToEnter,subjectiveAssessmentNotes,rangeOfMotion,muscleTone,muscleStrength,muscleEndurance,jointMobility,trunkControlBalance,grossMotorFunctionStanding,grossMotorFunctionCrawling,grossMotorFunctionWalking,grossMotorFunctionJumping,grossMotorFunctionBroadJump,grossMotorFunctionKickBall,grossMotorFunctionThrowBall,fineMotorFunctionGraspRelease,fineMotorFunctionReaching,fineMotorFunctionPutBlock,fineMotorFunctionScribbles,fineMotorFunctionCubes,fineMotorFunctionMature,fineMotorFunctionImmature,fineMotorFunctionImitate,fineMotorFunctionCopying,objectiveAssessmentNotes,tactile,auditory,visual,olfactory,gustatory,vestibular,proprioception,alphabet,numbers,shapes,colors,memoryFunction,attention,concentration,problemSolving,writingSkill,cognitiveProgressNote,independent,withSupervision,maximumAssistance,toiletTrained,moneyManagement,timeConcept,foldingClothes,hangingClothes,sweepFloor,makingDrinks,prepareSimpleFood,usePhone,occupationRemark,temperedTantrum,manipulative,easilyDistracted,passive,cooperative,isolation,reluctant,repatitivePrompting,verbalPrompting,physicalPrompting,eyeContactPerson,eyeContactObject,initiateAnswerQuestion,verbalRespond,voiceClarify,facialExpression,bodyLanguage,takingTurn,sharing,stayInGrouping,academicPerformance,analysisProblem,txPlan,therapistIncharge")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }

        // View All Reports for a Student
        public async Task<IActionResult> ViewAllReportsForStudent(int id)
        {
            var reports = await _context.Reports
                                        .Where(r => r.studentId == id)
                                        .ToListAsync();

            if (reports == null || reports.Count == 0)
            {
                return NotFound();
            }

            return View(reports);
        }

        // Compile all updates for one month
        public async Task<IActionResult> CompiledUpdates(int studentId)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var reportsForMonth = await _context.Reports
                                                .Where(r => r.studentId == studentId && r.Date >= startDate && r.Date <= endDate)
                                                .ToListAsync();
            //var staffDict = await _context.Therapists.ToDictionaryAsync(s => s.TherapistId, s => s.ApplicationUser.UserName);
            var therapists = await _context.Therapists.ToListAsync();
            var staffDict = therapists.ToDictionary(t => t.TherapistId, t => t.Name);
            ViewBag.StaffDict = staffDict;

            return View(reportsForMonth);
        }

        // Generate PDF for all updates
        public async Task<IActionResult> CompiledUpdatesPdf(int studentId)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1).AddDays(1).AddSeconds(-1);

            Console.WriteLine($"Student ID: {studentId}");
            Console.WriteLine($"Start Date: {startDate}");
            Console.WriteLine($"End Date: {endDate}");

            var reportsForMonth = await _context.Reports
                                                .Where(r => r.studentId == studentId &&
                                                    r.Date >= startDate &&
                                                    r.Date <= endDate &&
                                                    r.reportStatus == "Approved")
                                                .ToListAsync();

            if (reportsForMonth.Count == 0)
            {
                Console.WriteLine("No reports found for the given student and date range.");
            }
            else
            {
                Console.WriteLine($"{reportsForMonth.Count} reports found for the given student and date range.");
                foreach (var report in reportsForMonth)
                {
                    Console.WriteLine($"Report Title: {report.Title}, Date: {report.Date}");
                }
            }

            //var therapists = await _context.Therapists.ToListAsync();
            //var staffDict = therapists.ToDictionary(t => t.TherapistId, t => t.Name);
            //ViewBag.StaffDict = staffDict;

            return new ViewAsPdf("CompiledUpdatesPdf", reportsForMonth)
            {
                FileName = "CompiledUpdates.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

    }
}
