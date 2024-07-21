using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Model
{
    public enum ApiErrorType
    {
        ReferenceLimitExceeded = 1,
        AccountCodeNotProvided = 2,
        StateNotProvided = 3,
        ReferenceNotProvided = 4,
        OtherError = 5,
    }
}
