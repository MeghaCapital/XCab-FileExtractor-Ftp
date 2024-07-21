namespace Data.Entities.Ftp;

public class XcabRemoteConfiguration
{
    public int LoginId { get; set; } = default;
    public string ProdHostname { get; set; } = default;    
    public string ProdRootPath { get; set; } = default;
    public string ProdUsername { get; set; } = default;
    public string ProdPassword { get; set; } = default;
    public string TestHostname { get; set; } = default;
    public string TestRootPath { get; set; } = default;
    public string TestUsername { get; set; } = default;
    public string TestPassword { get; set; } = default;
    public bool UseTest { get; set; } = default;
    public RemoteFtpSchema SchemaType { get; set; } = default;
    public RemoteFtpAction RemoteFtpActionType { get; set; } = default;
    public string SubFtpPath { get; set; } = default;
}

public enum RemoteFtpSchema
{
    BarcodeOneColManyRows = 1
}

public enum RemoteFtpAction
{
    Booking = 1
}