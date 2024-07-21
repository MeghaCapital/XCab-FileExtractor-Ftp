namespace Data.Entities.Tplus
{
    public class TplusMultiLegModel : TplusJobEntity
    {
        public string BaseJobNumber { get; set; }
        public string TotalLegs { get; set; }
        public string LegNumber { get; set; }
        public string Driver { get; set; }
        public string SuburbName { get; set; }
        public string UserName { get; set; }
    }
}
