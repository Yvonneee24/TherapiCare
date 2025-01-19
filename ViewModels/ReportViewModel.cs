using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapiCareTest.Models
{
    public class ReportViewModel
    {
        [Key]
        [DisplayName("Report Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Report Title")]
        public string Title { get; set; }

        [ForeignKey("Student")]
        [DisplayName("Staff Id")]
        public int staffId { get; set; }

        [DisplayName("Student's Name")]
        public string studentName { get; set; }

        [DisplayName("Student's Id")]
        public int studentId { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Number of Sessions Attended")]
        public int numberOfSessionsAttended { get; set; }

        [DisplayName("Treatment Provided")]
        public string treatment { get; set; }

        [DisplayName("Status Report")]
        public string reportStatus { get; set; } = "Pending";

        // Subjective assessment fields
        [DisplayName("Enter by Himself/Herself")]
        public int enterByHimself { get; set; }

        [DisplayName("With Prompting")]
        public int enterWithPrompting { get; set; }

        [DisplayName("Difficulties Separate with Parents")]
        public int difficultiesSeparateWithParents { get; set; }

        [DisplayName("With Crying and Refuse")]
        public int withCryingRefuse { get; set; }

        [DisplayName("Greeting with Prompt")]
        public int greetingWithPrompt { get; set; }

        [DisplayName("Greeting by Himself/Herself")]
        public int greetingByHimself { get; set; }

        [DisplayName("Mute")]
        public int mute { get; set; }

        [DisplayName("Refuse to Enter")]
        public int refuseToEnter { get; set; }

        [DisplayName("Progress Notes")]
        public string subjectiveAssessmentNotes { get; set; }

        // Objective assessment fields
        [DisplayName("Range of Motion (Upper / Lower Extreme)")]
        public int rangeOfMotion { get; set; }

        [DisplayName("Muscle Tone")]
        public int muscleTone { get; set; }

        [DisplayName("Muscle Strength")]
        public int muscleStrength { get; set; }

        [DisplayName("Muscle Endurance")]
        public int muscleEndurance { get; set; }

        [DisplayName("Joint Mobility")]
        public int jointMobility { get; set; }

        [DisplayName("Trunk Control & Balance")]
        public int trunkControlBalance { get; set; }

        [DisplayName("Standing")]
        public int grossMotorFunctionStanding { get; set; }

        [DisplayName("Crawling")]
        public int grossMotorFunctionCrawling { get; set; }

        [DisplayName("Walking")]
        public int grossMotorFunctionWalking { get; set; }

        [DisplayName("Jumping")]
        public int grossMotorFunctionJumping { get; set; }

        [DisplayName("Broad Jump")]
        public int grossMotorFunctionBroadJump { get; set; }

        [DisplayName("Kick Ball")]
        public int grossMotorFunctionKickBall { get; set; }

        [DisplayName("Throw Ball")]
        public int grossMotorFunctionThrowBall { get; set; }

        [DisplayName("Grasp & Release")]
        public int fineMotorFunctionGraspRelease { get; set; }

        [DisplayName("Reaching")]
        public int fineMotorFunctionReaching { get; set; }

        [DisplayName("Put block in a cup")]
        public int fineMotorFunctionPutBlock { get; set; }

        [DisplayName("Scribbles")]
        public int fineMotorFunctionScribbles { get; set; }

        [DisplayName("Tower of ___ cubes")]
        public int fineMotorFunctionCubes { get; set; }

        [DisplayName("Mature pencil grasping")]
        public int fineMotorFunctionMature { get; set; }

        [DisplayName("Immature pencil group")]
        public int fineMotorFunctionImmature { get; set; }

        [DisplayName("Imitate vehicle line")]
        public int fineMotorFunctionImitate { get; set; }

        [DisplayName("Copying")]
        public int fineMotorFunctionCopying { get; set; }

        [DisplayName("Progress Notes")]
        public string objectiveAssessmentNotes { get; set; }

        [DisplayName("Tactile")]
        public int tactile { get; set; }

        [DisplayName("Auditory")]
        public int auditory { get; set; }

        [DisplayName("Visual")]
        public int visual { get; set; }

        [DisplayName("Olfactory")]
        public int olfactory { get; set; }

        [DisplayName("Gustatory")]
        public int gustatory { get; set; }

        [DisplayName("Vestibular")]
        public int vestibular { get; set; }

        [DisplayName("Proprioception")]
        public int proprioception { get; set; }

        [DisplayName("Alphabet")]
        public int alphabet { get; set; }

        [DisplayName("Numbers")]
        public int numbers { get; set; }

        [DisplayName("Shapes")]
        public int shapes { get; set; }

        [DisplayName("Colors")]
        public int colors { get; set; }

        [DisplayName("Memory Function")]
        public int memoryFunction { get; set; }

        [DisplayName("Attention")]
        public int attention { get; set; }

        [DisplayName("Concentration")]
        public int concentration { get; set; }

        [DisplayName("Problem Solving")]
        public int problemSolving { get; set; }

        [DisplayName("Writing Skill")]
        public int writingSkill { get; set; }

        [DisplayName("Cognitive Regulation Skill Progress Note")]
        public string cognitiveProgressNote { get; set; }

        [DisplayName("Independent")]
        public int independent { get; set; }

        [DisplayName("With Supervision")]
        public int withSupervision { get; set; }

        [DisplayName("Maximum Assitance")]
        public int maximumAssistance { get; set; }

        [DisplayName("Toilry Trained")]
        public int toiletTrained { get; set; }

        [DisplayName("Money Management")]
        public int moneyManagement { get; set; }

        [DisplayName("Time Concept")]
        public int timeConcept { get; set; }

        [DisplayName("Folding Clothes")]
        public int foldingClothes { get; set; }

        [DisplayName("Hanging Clothes")]
        public int hangingClothes { get; set; }

        [DisplayName("Sweep Floor")]
        public int sweepFloor { get; set; }

        [DisplayName("Making Drinks")]
        public int makingDrinks { get; set; }

        [DisplayName("prepareSimpleFood")]
        public int prepareSimpleFood { get; set; }

        [DisplayName("Use Phone")]
        public int usePhone { get; set; }

        [DisplayName("Occupation Performance Remark")]
        public string occupationRemark { get; set; }

        [DisplayName("Tempered Tantrum")]
        public int temperedTantrum { get; set; }

        [DisplayName("Manipulative")]
        public int manipulative { get; set; }

        [DisplayName("Easily Distracted")]
        public int easilyDistracted { get; set; }

        [DisplayName("Passive")]
        public int passive { get; set; }

        [DisplayName("Cooperative")]
        public int cooperative { get; set; }

        [DisplayName("Isolation")]
        public int isolation { get; set; }

        [DisplayName("Reluctant")]
        public int reluctant { get; set; }

        [DisplayName("Repetitive Prompting")]
        public int repatitivePrompting { get; set; }

        [DisplayName("Verbal Prompting")]
        public int verbalPrompting { get; set; }

        [DisplayName("Physical Prompting")]
        public int physicalPrompting { get; set; }

        [DisplayName("Person")]
        public int eyeContactPerson { get; set; }

        [DisplayName("Object")]
        public int eyeContactObject { get; set; }

        [DisplayName("Initiate / Answer Question")]
        public int initiateAnswerQuestion { get; set; }

        [DisplayName("Verbal Respond")]
        public int verbalRespond { get; set; }

        [DisplayName("Voice Clarify")]
        public int voiceClarify { get; set; }

        [DisplayName("Facial Expression")]
        public int facialExpression { get; set; }

        [DisplayName("Body Language")]
        public int bodyLanguage { get; set; }

        [DisplayName("Taking Turn")]
        public int takingTurn { get; set; }

        [DisplayName("Sharing")]
        public int sharing { get; set; }

        [DisplayName("Stay in Grouping")]
        public int stayInGrouping { get; set; }

        [DisplayName("Academic Performance")]
        public int academicPerformance { get; set; }

        [DisplayName("A - Analysis Problem, Short Term, Long Term Goal")]
        public string analysisProblem { get; set; }

        [DisplayName("P - Tx Done, Tx Plan")]
        public string txPlan { get; set; }

        [DisplayName("Therapist Incharge")]
        public string therapistIncharge { get; set; }
    }
}
