namespace Data.Model
{
    public class VehicleChecklist
    {
        public int Id { get; set; }

        public int VehicleGroupId { get; set; }

        public string Question { get; set; }

        public string ShortQuestion { get; set; }

        public int PdaScreen { get; set; }
    }
}
