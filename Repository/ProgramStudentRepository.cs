using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class ProgramStudentRepository : Repository<ProgramStudent>, IProgramStudentRepository
    {

        private readonly ApplicationDbContext _context;

        public ProgramStudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ProgramStudent programStudent)
        {
            _context.ProgramStudents.Update(programStudent);
        }
    }
}
