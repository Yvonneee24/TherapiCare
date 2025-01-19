using System.Linq.Expressions;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {

        private readonly ApplicationDbContext _context;

        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
        }
    }
}
