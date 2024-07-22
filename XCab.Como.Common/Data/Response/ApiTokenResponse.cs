namespace xcab.como.common.Data.Response
{
    public class ApiTokenResponse
    {
        public Payload Payload;
        public RawPayload RawPayload;
        public string StatusCode { get; set; }
    }
    public class Payload
    {
        public string Id { get; set; }
        public string ApiToken { get; set; }
        public string IssuedAtUtc { get; set; }
    }
    public class RawPayload
    {
        public string Id { get; set; }
        public string ApiToken { get; set; }
        public string IssuedAtUtc { get; set; }
    }
}
