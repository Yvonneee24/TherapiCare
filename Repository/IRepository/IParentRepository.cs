using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface IParentRepository : IRepository<Parent>
    {
        void Update(Parent Parent);
    }
}
