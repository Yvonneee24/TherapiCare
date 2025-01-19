using System;

namespace TherapiCareTest.ViewModels
{
    public class CustomerServiceVM
    {
        public int SlotId { get; set; }
        public int SessionId { get; set; }
        public string TherapyProgramName { get; set; }
        public string SessionName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TherapistName { get; set; }
    }
}
