using TherapiCareTest.Models;

namespace TherapiCareTest.ViewModels
{
    public class SessionSlotVM
    {
        public string SessionName { get; set; }
        public int SessionId { get; set; }
        public IEnumerable<Slot> Slots { get; set; }
    }
}
