using System;

namespace Data.Entities.ConsolidatedReferences
{
    public class XCabClientReferences
    {
        public int Id { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public DateTime JobDate { get; set; }
        public long PrimaryJobId { get; set; }
    }
}