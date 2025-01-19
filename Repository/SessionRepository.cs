using System.Linq.Expressions;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {

        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Session session)
        {
            _context.Sessions.Update(session);
        }
    }
}
