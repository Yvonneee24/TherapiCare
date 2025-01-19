using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ITherapyProgramRepository TherapyProgram { get; private set; }
        public ISlotRepository Slot { get; private set; }
        public IScheduleRepository Schedule { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IProgramStudentRepository ProgramStudent { get; private set; }
        public IStudentRepository Student { get; private set; }
        public IParentRepository Parent { get; private set; }
        public ISessionRepository Session { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            TherapyProgram = new TherapyProgramRepository(context);
            Slot = new SlotRepository(context);
            Schedule = new ScheduleRepository(context);
            ApplicationUser = new ApplicationUserRepository(context);
            ProgramStudent = new ProgramStudentRepository(context);
            Student = new StudentRepository(context);
            Parent = new ParentRepository(context);
            Session = new SessionRepository(context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
