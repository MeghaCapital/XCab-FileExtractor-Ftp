namespace XCabService.IkeaService;

public class IkeaPinrouteModifier
{
    public string Reference { get; set; }
    public string DriverNumber { get; set; }
    public TimeOnly RouteStart { get; set; }
    public TimeOnly Eta { get; set; }
    public string RouteName { get; set; }
    public string Sequence { get; set; }
}