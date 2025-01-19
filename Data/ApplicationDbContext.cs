using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TherapiCareTest.Models;
using Therapi.Utility;
using TherapiCareTest.Data.Enum;

namespace TherapiCareTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<TherapyProgram> TherapyPrograms { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ProgramStudent> ProgramStudents { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Billing> Billings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string adminUserId = "admin-user-id";
            string therapistUserId = "therapist-user-id";
            string parentUserId = "parent-user-id";
            string custServiceUserId = "custService-user-id";

            string adminRoleId = "admin-role-id";
            string therapistRoleId = "therapist-role-id";
            string parentRoleId = "parent-role-id";
            string custServiceRoleId = "custService-role-id";
            string customerRoleId = "customer-role-id";

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = SD.Role_Admin, NormalizedName = SD.Role_Admin.ToUpper() },
                new IdentityRole { Id = therapistRoleId, Name = SD.Role_Therapist, NormalizedName = SD.Role_Therapist.ToUpper() },
                new IdentityRole { Id = parentRoleId, Name = SD.Role_Parent, NormalizedName = SD.Role_Parent.ToUpper() },
                new IdentityRole { Id = custServiceRoleId, Name = SD.Role_CustomerService, NormalizedName = SD.Role_CustomerService.ToUpper() },
                new IdentityRole { Id = customerRoleId, Name = SD.Role_Customer, NormalizedName = SD.Role_Customer.ToUpper() }
            );


            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin_123"),
                    SecurityStamp = string.Empty,
                    PhoneNumber = "0126983514"
                },
                new ApplicationUser
                {
                    Id = therapistUserId,
                    UserName = "therapist@gmail.com",
                    NormalizedUserName = "THERAPIST@GMAIL.COM",
                    Email = "therapist@gmail.com",
                    NormalizedEmail = "THERAPIST@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Therapist_123"),
                    SecurityStamp = string.Empty,
                    PhoneNumber = "0184521367"
                },
                new ApplicationUser
                {
                    Id = parentUserId,
                    UserName = "parent@gmail.com",
                    NormalizedUserName = "PARENT@GMAIL.COM",
                    Email = "parent@gmail.com",
                    NormalizedEmail = "PARENT@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Parent_123"),
                    SecurityStamp = string.Empty,
                    PhoneNumber = "0198745632"
                },
                new ApplicationUser
                {
                    Id = custServiceUserId,
                    UserName = "customerservice@gmail.com",
                    NormalizedUserName = "CUSTOMERSERVICE@GMAIL.COM",
                    Email = "customerservice@gmail.com",
                    NormalizedEmail = "CUSTOMERSERVICE@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc_12345"),
                    SecurityStamp = string.Empty,
                    PhoneNumber = "0123456789"
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
        new IdentityUserRole<string> { UserId = therapistUserId, RoleId = therapistRoleId },
        new IdentityUserRole<string> { UserId = parentUserId, RoleId = parentRoleId },
        new IdentityUserRole<string> { UserId = custServiceUserId, RoleId = custServiceRoleId }
    );
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Yvonne",
                    Passport = "A1234567",
                    Nasionality = "Malaysia",
                    Race = "Chinese",
                    BirthPlace = "Johor",
                    Religion = "Buddha",
                    DOB = new DateTime(2020, 5, 1),
                    Gender = Gender.MALE,
                    Age = 4,
                    ParentId = "950524057482",
                    Pediatricians = "Dr. Smith",
                    RecommendedByHospital = "NY General Hospital",
                    DeadlineDiagnoseByDoctor = new DateTime(2023, 12, 31),
                    DiagnosisCondition = "Condition A",
                    OccupationalTherapy = true,
                    SpeechTherapy = false,
                    Others = "None"
                },
                new Student
                {
                    Id = 2,
                    Name = "Jane",
                    Passport = "B7654321",
                    Nasionality = "Malaysia",
                    Race = "Chinese",
                    BirthPlace = "Johor",
                    Religion = "Buddha",
                    DOB = new DateTime(2019, 8, 15),
                    Gender = Gender.FEMALE,
                    Age = 5,
                    ParentId = "950524057482",
                    Pediatricians = "Dr. Lee",
                    RecommendedByHospital = "Toronto General Hospital",
                    DeadlineDiagnoseByDoctor = new DateTime(2024, 6, 30),
                    DiagnosisCondition = "Condition B",
                    OccupationalTherapy = false,
                    SpeechTherapy = true,
                    Others = "None"
                }
            );
            modelBuilder.Entity<Parent>().HasData(
                new Parent
                {
                    ParentId = "950524057482",
                    Name = "Kent",
                    City = "Seremban",
                    Postcode = "70200",
                    State = "Negeri Sembilan",
                    Address = "123, Jalan Abc, Taman Abc",
                    Occupation = "Doctor",
                    HouseholdIncome = "<RM5000",
                    ApplicationUserId = parentUserId
                }
            );
            modelBuilder.Entity<Therapist>().HasData(
                new Therapist
                {
                    TherapistId = "456",
                    Name = "Soo",
                    ApplicationUserId = therapistUserId
                }
            );

            modelBuilder.Entity<TherapyProgram>().HasData(
                new TherapyProgram
                {
                    Id = 1,
                    Name = "Program 1",
                    Description = "Description of Program 1",
                    Objective = "Objective of Program 1",
                    WeekdayPrice = 100.00m,
                    WeekendPrice = 150.00m
                },
                new TherapyProgram
                {
                    Id = 2,
                    Name = "Program 2",
                    Description = "Description of Program 2",
                    Objective = "Objective of Program 2",
                    WeekdayPrice = 120.00m,
                    WeekendPrice = 170.00m
                }
            );

            modelBuilder.Entity<Session>().HasData(
                new Session
                {
                    Id = 1,
                    Name = "Session 1",
                    Description = "Description of Session 1",
                    ProgramId = 1
                },
                new Session
                {
                    Id = 3,
                    Name = "Session 2",
                    Description = "Description of Session 2",
                    ProgramId = 1
                },
                new Session
                {
                    Id = 2,
                    Name = "Session 1",
                    Description = "Description of Session 1",
                    ProgramId = 2
                }
            );

            modelBuilder.Entity<Slot>().HasData(
                new Slot
                {
                    Id = 1,
                    StartTime = new DateTime(2024, 8, 1, 9, 0, 0),
                    EndTime = new DateTime(2024, 8, 1, 10, 0, 0),
                    TherapistId = "456",
                    SessionId = 1
                },
                new Slot
                {
                    Id = 3,
                    StartTime = new DateTime(2024, 8, 2, 9, 0, 0),
                    EndTime = new DateTime(2024, 8, 2, 10, 0, 0),
                    TherapistId = "456",
                    SessionId = 1
                },
                new Slot
                {
                    Id = 2,
                    StartTime = new DateTime(2024, 8, 3, 11, 0, 0),
                    EndTime = new DateTime(2024, 8, 3, 12, 0, 0),
                    TherapistId = "456",
                    SessionId = 2
                }
            );

            modelBuilder.Entity<ProgramStudent>().HasData(
                new ProgramStudent
                {
                    Id = 1,
                    StudentId = 1,
                    ProgramId = 1
                },
                new ProgramStudent
                {
                    Id = 2,
                    StudentId = 2,
                    ProgramId = 2
                }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    Id = 1,
                    SlotId = 1,
                    ProgramStudentId = 1
                },
                new Schedule
                {
                    Id = 2,
                    SlotId = 2,
                    ProgramStudentId = 2
                }
            );
        }
    }
}
