namespace TherapiCareTest.Models
{
    public class ParentHomeViewModel
    {
        public List<TherapyProgram> RegisteredPrograms { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Announcement> Announcements { get; set; }
        public List<Student> Students { get; set; }
        public string ParentName { get; set; }
    }
}
