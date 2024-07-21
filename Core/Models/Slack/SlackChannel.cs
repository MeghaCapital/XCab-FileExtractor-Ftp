using Core.Helpers;

namespace Core.Models.Slack
{
    public enum SlackChannel
    {
        [StringValue("#xcab-errors")]
        GeneralErrors = 1,
        [StringValue("#webservice-logs")]
        WebServiceLogs = 2,
        [StringValue("#webservice-errors")]
        WebServiceErrors = 3,
        [StringValue("#webclient-logs")]
        WebClientLogs = 4,
        [StringValue("#webclient-errors")]
        WebClientErrors = 5,
        [StringValue("#errors-soap")]
        ErrorsSoap = 6,
        [StringValue("#icab-logs")]
        XmlRequestSoap = 7
    }
}
