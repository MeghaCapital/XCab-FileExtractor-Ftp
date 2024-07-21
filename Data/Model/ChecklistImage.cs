namespace Data.Model
{
    public class ChecklistImage
    {
        public int BookingId { get; set; }

        public int ChecklistId { get; set; }

        public byte[] Image { get; set; }
    }

    public enum checkListSubJobFilter
    {
        All = 1,
        PickUp = 2,
        Delivery = 3
    }
}
