using System.Linq.Expressions;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class TherapyProgramRepository : Repository<TherapyProgram>, ITherapyProgramRepository
    {

        private readonly ApplicationDbContext _context;

        public TherapyProgramRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(TherapyProgram therapyProgram)
        {
            _context.TherapyPrograms.Update(therapyProgram);
        }
    }
}
