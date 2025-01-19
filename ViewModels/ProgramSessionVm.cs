using System.Collections.Generic;
using TherapiCareTest.Models;

namespace TherapiCareTest.ViewModels
{
    public class ProgramSessionsVM
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public int ProgramStudentId { get; set; }
    }
    
}
