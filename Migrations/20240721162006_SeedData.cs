using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TherapiCareTest.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e361ef9-52b6-4a04-8a18-66f902851ea0", "AQAAAAIAAYagAAAAEDhGYI7AvJxLJmIuVc99VSwSWhm72CU8rL1fNzKg2hsXzNNW8C0z0ZjzhFSpPL4FuA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "custService-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6b38cd14-1c2d-4fce-8232-00e4461f9dfa", "AQAAAAIAAYagAAAAEHrqo2DR9knHaUwC5NwJk+xj/QyyBLjcQSjsHTq7QFtXqPKnMD7pWvbORIW9aI1QAQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "parent-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae86ef7b-cf7b-49b2-837f-1a42e0806abd", "AQAAAAIAAYagAAAAEJCOR38jE1JcoHlNRoMetPxhkxcGFOvqcqCrvmoxdxSpVtCBgrR3KNVPwuCTTM7Y2A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "therapist-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f12420d-9a36-486b-baa1-9c6aef10f520", "AQAAAAIAAYagAAAAEOPBLZpIOCbTXeqRsSTKp0U+YIFHQrih9ABLzL6J0IdIyJv37iq74jeh5Wxw9J7BEA==" });

            migrationBuilder.InsertData(
                table: "Parents",
                columns: new[] { "ParentId", "Address", "ApplicationUserId", "City", "HouseholdIncome", "Name", "Occupation", "Postcode", "State" },
                values: new object[] { "950524057482", "123, Jalan Abc, Taman Abc", "parent-user-id", "Seremban", "<RM5000", "Kent", "Doctor", "70200", "Negeri Sembilan" });

            migrationBuilder.InsertData(
                table: "Therapists",
                columns: new[] { "TherapistId", "ApplicationUserId", "Name" },
                values: new object[] { "456", "therapist-user-id", "Soo" });

            migrationBuilder.InsertData(
                table: "TherapyPrograms",
                columns: new[] { "Id", "Description", "Name", "Objective", "WeekdayPrice", "WeekendPrice" },
                values: new object[,]
                {
                    { 1, "Description of Program 1", "Program 1", "Objective of Program 1", 100.00m, 150.00m },
                    { 2, "Description of Program 2", "Program 2", "Objective of Program 2", 120.00m, 170.00m }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "Description", "Name", "ProgramId" },
                values: new object[,]
                {
                    { 1, "Description of Session 1", "Session 1", 1 },
                    { 2, "Description of Session 1", "Session 1", 2 },
                    { 3, "Description of Session 2", "Session 2", 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "BirthPlace", "DOB", "DeadlineDiagnoseByDoctor", "DiagnosisCondition", "Gender", "Name", "Nasionality", "OccupationalTherapy", "Others", "ParentId", "Passport", "Pediatricians", "Race", "RecommendedByHospital", "Religion", "SpeechTherapy" },
                values: new object[,]
                {
                    { 1, 4, "Johor", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Condition A", 0, "Yvonne", "Malaysia", true, "None", "950524057482", "A1234567", "Dr. Smith", "Chinese", "NY General Hospital", "Buddha", false },
                    { 2, 5, "Johor", new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Condition B", 1, "Jane", "Malaysia", false, "None", "950524057482", "B7654321", "Dr. Lee", "Chinese", "Toronto General Hospital", "Buddha", true }
                });

            migrationBuilder.InsertData(
                table: "ProgramStudents",
                columns: new[] { "Id", "Date", "ProgramId", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 21, 16, 20, 3, 457, DateTimeKind.Utc).AddTicks(4018), 1, 1 },
                    { 2, new DateTime(2024, 7, 21, 16, 20, 3, 457, DateTimeKind.Utc).AddTicks(4029), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "EndTime", "SessionId", "StartTime", "TherapistId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 8, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "456" },
                    { 2, new DateTime(2024, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), "456" },
                    { 3, new DateTime(2024, 8, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 8, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "456" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "ProgramStudentId", "SlotId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProgramStudents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProgramStudents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Therapists",
                keyColumn: "TherapistId",
                keyValue: "456");

            migrationBuilder.DeleteData(
                table: "Parents",
                keyColumn: "ParentId",
                keyValue: "950524057482");

            migrationBuilder.DeleteData(
                table: "TherapyPrograms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TherapyPrograms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2a728445-fa62-4e53-ae63-59c96b1bb928", "AQAAAAIAAYagAAAAEJ5HaAFq2Ane9/BaNBQX624uvbOshAzhRBP0a5XLpg1eJFDf4lksWGZ0VLBodGmuKA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "custService-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fdc1cb5-ae6b-4305-8904-40115a00efaf", "AQAAAAIAAYagAAAAEOCWaziQhZkAsXdb1VwJCWVJL5Prt2tHy6oGEchPOJp4fDeawCjo1NrDp0dBRSn9hw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "parent-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e294a647-0c24-4ae6-94a2-18fc4cf66e38", "AQAAAAIAAYagAAAAEObniKfSsxdo/DUmBM4dp6aBLvQsNKQjambKazZ5SuKhatdwrWJgYiTUlVUJmpqi3A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "therapist-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eb52fb12-2ad3-460b-b99a-57afe460aded", "AQAAAAIAAYagAAAAEFRETc9/zNG6vNd/a/MhBtTO1mOAYCl9lBmZie9mmqEfyZrEjbKOAzigmEDrroxIxQ==" });
        }
    }
}
