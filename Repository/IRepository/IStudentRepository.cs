using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        void Update(Student Student);
    }
}
