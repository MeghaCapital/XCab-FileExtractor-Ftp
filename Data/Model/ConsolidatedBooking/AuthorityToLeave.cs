namespace Data.Model.ConsolidatedBooking
{
    public class AuthorityToLeave
    {
        /// <summary>
        /// Authority to leave the goods
        /// </summary>
        public bool? IsGranted { get; set; }

        /// <summary>
        /// Instructions
        /// </summary>
        public string? Instructions { get; set; }
    }
}