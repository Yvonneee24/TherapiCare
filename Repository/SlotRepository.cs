using System.Linq.Expressions;
using TherapiCareTest.Data;
using TherapiCareTest.Models;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class SlotRepository : Repository<Slot>, ISlotRepository
    {

        private readonly ApplicationDbContext _context;

        public SlotRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Slot slot)
        {
            _context.Slots.Update(slot);
        }
    }
}
