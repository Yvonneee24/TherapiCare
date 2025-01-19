using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface ISessionRepository : IRepository<Session>
    {
        void Update(Session session);
    }
}
