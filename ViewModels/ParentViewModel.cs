using System.ComponentModel;
using TherapiCareTest.Data.Enum;

namespace TherapiCareTest.ViewModel
{
    public class ParentViewModel
    {
        [DisplayName("Student Id")]
        public int studentId { get; set; }

        [DisplayName("Children Name")]
        public string childrenName { get; set; }

        [DisplayName("Gender")]
        public Gender? childrenGender { get; set; }

        [DisplayName("Program Id")]
        public int programId { get; set; }

        [DisplayName("Program Name")]
        public string programName { get; set; }

        [DisplayName("Parent Id")]
        public string parentId { get; set; }

        [DisplayName("Parent Name")]
        public string parentName { get; set; }
    }
}
