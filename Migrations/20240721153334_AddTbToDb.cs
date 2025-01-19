using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TherapiCareTest.Migrations
{
    /// <inheritdoc />
    public partial class AddTbToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    a_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    a_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    a_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    a_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    IsViewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.a_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TherapyPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekdayPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeekendPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapyPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerServices",
                columns: table => new
                {
                    CustServiceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    reportStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServices", x => x.CustServiceId);
                    table.ForeignKey(
                        name: "FK_CustomerServices_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseholdIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ParentId);
                    table.ForeignKey(
                        name: "FK_Parents_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Therapists",
                columns: table => new
                {
                    TherapistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapists", x => x.TherapistId);
                    table.ForeignKey(
                        name: "FK_Therapists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_TherapyPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "TherapyPrograms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nasionality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Pediatricians = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendedByHospital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeadlineDiagnoseByDoctor = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiagnosisCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationalTherapy = table.Column<bool>(type: "bit", nullable: false),
                    SpeechTherapy = table.Column<bool>(type: "bit", nullable: false),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "ParentId");
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TherapistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "TherapistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentReceipt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    TherapyProgramId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billings_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "ParentId");
                    table.ForeignKey(
                        name: "FK_Billings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Billings_TherapyPrograms_TherapyProgramId",
                        column: x => x.TherapyProgramId,
                        principalTable: "TherapyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramStudents_TherapyPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "TherapyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    staffId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    numberOfSessionsAttended = table.Column<int>(type: "int", nullable: false),
                    treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reportStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    enterByHimself = table.Column<int>(type: "int", nullable: false),
                    enterWithPrompting = table.Column<int>(type: "int", nullable: false),
                    difficultiesSeparateWithParents = table.Column<int>(type: "int", nullable: false),
                    withCryingRefuse = table.Column<int>(type: "int", nullable: false),
                    greetingWithPrompt = table.Column<int>(type: "int", nullable: false),
                    greetingByHimself = table.Column<int>(type: "int", nullable: false),
                    mute = table.Column<int>(type: "int", nullable: false),
                    refuseToEnter = table.Column<int>(type: "int", nullable: false),
                    subjectiveAssessmentNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rangeOfMotion = table.Column<int>(type: "int", nullable: false),
                    muscleTone = table.Column<int>(type: "int", nullable: false),
                    muscleStrength = table.Column<int>(type: "int", nullable: false),
                    muscleEndurance = table.Column<int>(type: "int", nullable: false),
                    jointMobility = table.Column<int>(type: "int", nullable: false),
                    trunkControlBalance = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionStanding = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionCrawling = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionWalking = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionJumping = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionBroadJump = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionKickBall = table.Column<int>(type: "int", nullable: false),
                    grossMotorFunctionThrowBall = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionGraspRelease = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionReaching = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionPutBlock = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionScribbles = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionCubes = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionMature = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionImmature = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionImitate = table.Column<int>(type: "int", nullable: false),
                    fineMotorFunctionCopying = table.Column<int>(type: "int", nullable: false),
                    objectiveAssessmentNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tactile = table.Column<int>(type: "int", nullable: false),
                    auditory = table.Column<int>(type: "int", nullable: false),
                    visual = table.Column<int>(type: "int", nullable: false),
                    olfactory = table.Column<int>(type: "int", nullable: false),
                    gustatory = table.Column<int>(type: "int", nullable: false),
                    vestibular = table.Column<int>(type: "int", nullable: false),
                    proprioception = table.Column<int>(type: "int", nullable: false),
                    alphabet = table.Column<int>(type: "int", nullable: false),
                    numbers = table.Column<int>(type: "int", nullable: false),
                    shapes = table.Column<int>(type: "int", nullable: false),
                    colors = table.Column<int>(type: "int", nullable: false),
                    memoryFunction = table.Column<int>(type: "int", nullable: false),
                    attention = table.Column<int>(type: "int", nullable: false),
                    concentration = table.Column<int>(type: "int", nullable: false),
                    problemSolving = table.Column<int>(type: "int", nullable: false),
                    writingSkill = table.Column<int>(type: "int", nullable: false),
                    cognitiveProgressNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    independent = table.Column<int>(type: "int", nullable: false),
                    withSupervision = table.Column<int>(type: "int", nullable: false),
                    maximumAssistance = table.Column<int>(type: "int", nullable: false),
                    toiletTrained = table.Column<int>(type: "int", nullable: false),
                    moneyManagement = table.Column<int>(type: "int", nullable: false),
                    timeConcept = table.Column<int>(type: "int", nullable: false),
                    foldingClothes = table.Column<int>(type: "int", nullable: false),
                    hangingClothes = table.Column<int>(type: "int", nullable: false),
                    sweepFloor = table.Column<int>(type: "int", nullable: false),
                    makingDrinks = table.Column<int>(type: "int", nullable: false),
                    prepareSimpleFood = table.Column<int>(type: "int", nullable: false),
                    usePhone = table.Column<int>(type: "int", nullable: false),
                    occupationRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    temperedTantrum = table.Column<int>(type: "int", nullable: false),
                    manipulative = table.Column<int>(type: "int", nullable: false),
                    easilyDistracted = table.Column<int>(type: "int", nullable: false),
                    passive = table.Column<int>(type: "int", nullable: false),
                    cooperative = table.Column<int>(type: "int", nullable: false),
                    isolation = table.Column<int>(type: "int", nullable: false),
                    reluctant = table.Column<int>(type: "int", nullable: false),
                    repatitivePrompting = table.Column<int>(type: "int", nullable: false),
                    verbalPrompting = table.Column<int>(type: "int", nullable: false),
                    physicalPrompting = table.Column<int>(type: "int", nullable: false),
                    eyeContactPerson = table.Column<int>(type: "int", nullable: false),
                    eyeContactObject = table.Column<int>(type: "int", nullable: false),
                    initiateAnswerQuestion = table.Column<int>(type: "int", nullable: false),
                    verbalRespond = table.Column<int>(type: "int", nullable: false),
                    voiceClarify = table.Column<int>(type: "int", nullable: false),
                    facialExpression = table.Column<int>(type: "int", nullable: false),
                    bodyLanguage = table.Column<int>(type: "int", nullable: false),
                    takingTurn = table.Column<int>(type: "int", nullable: false),
                    sharing = table.Column<int>(type: "int", nullable: false),
                    stayInGrouping = table.Column<int>(type: "int", nullable: false),
                    academicPerformance = table.Column<int>(type: "int", nullable: false),
                    analysisProblem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    txPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    therapistIncharge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapistId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "TherapistId");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlotId = table.Column<int>(type: "int", nullable: false),
                    ProgramStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_ProgramStudents_ProgramStudentId",
                        column: x => x.ProgramStudentId,
                        principalTable: "ProgramStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin-role-id", null, "Admin", "ADMIN" },
                    { "customer-role-id", null, "Customer", "CUSTOMER" },
                    { "custService-role-id", null, "CustomerService", "CUSTOMERSERVICE" },
                    { "parent-role-id", null, "Parent", "PARENT" },
                    { "therapist-role-id", null, "Therapist", "THERAPIST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin-user-id", 0, "2a728445-fa62-4e53-ae63-59c96b1bb928", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEJ5HaAFq2Ane9/BaNBQX624uvbOshAzhRBP0a5XLpg1eJFDf4lksWGZ0VLBodGmuKA==", "0126983514", false, "", false, "admin@gmail.com" },
                    { "custService-user-id", 0, "1fdc1cb5-ae6b-4305-8904-40115a00efaf", "customerservice@gmail.com", true, false, null, "CUSTOMERSERVICE@GMAIL.COM", "CUSTOMERSERVICE@GMAIL.COM", "AQAAAAIAAYagAAAAEOCWaziQhZkAsXdb1VwJCWVJL5Prt2tHy6oGEchPOJp4fDeawCjo1NrDp0dBRSn9hw==", "0123456789", false, "", false, "customerservice@gmail.com" },
                    { "parent-user-id", 0, "e294a647-0c24-4ae6-94a2-18fc4cf66e38", "parent@gmail.com", true, false, null, "PARENT@GMAIL.COM", "PARENT@GMAIL.COM", "AQAAAAIAAYagAAAAEObniKfSsxdo/DUmBM4dp6aBLvQsNKQjambKazZ5SuKhatdwrWJgYiTUlVUJmpqi3A==", "0198745632", false, "", false, "parent@gmail.com" },
                    { "therapist-user-id", 0, "eb52fb12-2ad3-460b-b99a-57afe460aded", "therapist@gmail.com", true, false, null, "THERAPIST@GMAIL.COM", "THERAPIST@GMAIL.COM", "AQAAAAIAAYagAAAAEFRETc9/zNG6vNd/a/MhBtTO1mOAYCl9lBmZie9mmqEfyZrEjbKOAzigmEDrroxIxQ==", "0184521367", false, "", false, "therapist@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "admin-role-id", "admin-user-id" },
                    { "custService-role-id", "custService-user-id" },
                    { "parent-role-id", "parent-user-id" },
                    { "therapist-role-id", "therapist-user-id" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ApplicationUserId",
                table: "Admins",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_ParentId",
                table: "Billings",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_StudentId",
                table: "Billings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_TherapyProgramId",
                table: "Billings",
                column: "TherapyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_ApplicationUserId",
                table: "CustomerServices",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ApplicationUserId",
                table: "Parents",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramStudents_ProgramId",
                table: "ProgramStudents",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramStudents_StudentId",
                table: "ProgramStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ApplicationUserId",
                table: "Reports",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_studentId",
                table: "Reports",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TherapistId",
                table: "Reports",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProgramStudentId",
                table: "Schedules",
                column: "ProgramStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SlotId",
                table: "Schedules",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ProgramId",
                table: "Sessions",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_SessionId",
                table: "Slots",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_TherapistId",
                table: "Slots",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentId",
                table: "Students",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapists_ApplicationUserId",
                table: "Therapists",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "CustomerServices");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProgramStudents");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Therapists");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "TherapyPrograms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
