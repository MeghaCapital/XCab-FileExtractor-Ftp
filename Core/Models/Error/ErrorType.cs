using Core.Helpers;

namespace Core.Models.Error
{
    public enum ErrorType
    {
        [StringValue("General Error")]
        General = 1,
        [StringValue("Fatal Error")]
        Fatal = 2

    }
}
