namespace Data.Entities.ExternalClientIntegration
{
    public class ExternalClientIntegration
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string SecurityKey { get; set; }
        public string LiveApiUrl { get; set; }
        public string TestApiUrl { get; set; }
        public bool UseLiveApi { get; set; }
        public bool Active { get; set; }
        public string LivePodApiUrl { get; set; }
        public string TestPodApiUrl { get; set; }
        public string LivePocApiUrl { get; set; }
        public string TestPocApiUrl { get; set; }
		public string BasicAuthUsername { get; set; }
		public string BasicAuthPassword { get; set; }
	}
}
