using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {

        private readonly ApplicationDbContext _context;

        public ParentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Parent parent)
        {
            _context.Parents.Update(parent);
        }
    }
}
