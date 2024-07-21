using Data.Entities;
using Data.Model.ConsolidatedReferences;

namespace Data.Model
{
    public class XCabAsnBooking: XCabBooking
    {
        public List<xCabConsolidatedReferences> LstConsolidatedReferences { get; set; }
        public int NotificationCount { get; set; }
        public int ContactDetailsCount { get; set; }
    }
}
