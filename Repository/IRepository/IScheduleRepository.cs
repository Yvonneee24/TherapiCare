using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        void Update(Schedule Schedule);
    }
}
