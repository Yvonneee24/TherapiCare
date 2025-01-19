namespace TherapiCareTest.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITherapyProgramRepository TherapyProgram { get; }
        IScheduleRepository Schedule { get; }
        ISlotRepository Slot { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IProgramStudentRepository ProgramStudent { get; }
        IStudentRepository Student { get; }
        IParentRepository Parent { get; }
        ISessionRepository Session { get; }
        void Save();
    }
}
