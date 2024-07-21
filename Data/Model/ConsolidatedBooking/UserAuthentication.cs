namespace Data.Model.ConsolidatedBooking
{
    /// <summary>
    ///     Description for providing User Credentials
    /// </summary>
    public class UserAuthentication
    {
        /// <summary>
        ///     Username to be provided with every request
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Shared Key to be provided with every request
        /// </summary>
        public string SharedKey { get; set; }
    }
}