using Data.Repository.SecondaryRepositories.Interfaces;

namespace Data.Repository.SecondaryRepositories
{
    public static class Nt12Context
    {
        public static IAppVehicleGroupRepository VehicleGroups { get; }
        public static IAppVehicleChecklistImageRepository ChecklistImages { get; }
        public static IAppVehicleChecklistRepository Checklists { get; }
        public static IChecklistResponseRepository ChecklistResponses { get; }

        static Nt12Context()
        {
            VehicleGroups = new AppVehicleGroupRepository();
            ChecklistImages = new AppVehicleChecklistImageRepository();
            Checklists = new AppVehicleChecklistRepository();
            ChecklistResponses = new ChecklistResponseRepository<ESchema>();
        }
    }
}
