using System.ComponentModel;
using TherapiCareTest.Data.Enum;

namespace TherapiCareTest.ViewModel
{
    public class StudentViewModel
    {
        [DisplayName("Student Id")]
        public int Id { get; set; }

        [DisplayName("Student Name")]
        public string Name { get; set; }

        [DisplayName("Student's Ic")]
        public string Ic { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }

        [DisplayName("Gender")]
        public Gender? Gender { get; set; }

        [DisplayName("Program Id")]
        public int programId { get; set; }

        [DisplayName("Program Name")]
        public string programName { get; set; }

        [DisplayName("Parent Id")]
        public string parentId { get; set; }

        [DisplayName("Parent Name")]
        public string parentName { get; set; }

        [DisplayName("Status Report")]
        public string status { get; set; } = "Pending";

    }
}

