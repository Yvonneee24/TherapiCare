using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface IProgramStudentRepository : IRepository<ProgramStudent>
    {
        void Update(ProgramStudent ProgramStudent);
    }
}
