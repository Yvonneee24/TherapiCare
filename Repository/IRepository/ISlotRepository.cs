using TherapiCareTest.Models;

namespace TherapiCareTest.Repository.IRepository
{
    public interface ISlotRepository : IRepository<Slot>
    {
        void Update(Slot slot);
    }
}
