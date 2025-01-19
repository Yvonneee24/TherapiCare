using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface ITherapyProgramRepository : IRepository<TherapyProgram>
    {
        void Update(TherapyProgram therapyProgram);
    }
}
