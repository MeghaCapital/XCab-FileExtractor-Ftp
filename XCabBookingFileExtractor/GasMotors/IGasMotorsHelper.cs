using Core;
using Data.Entities.GenericIntegration;
using System.Collections.Generic;

namespace XCabBookingFileExtractor.GasMotors
{
    public interface IGasMotorsHelper
    {
        List<Booking> ExtractBooking(string accountCode, int mappedStateId, List<GasMotorsCsvRow> csvRows, ICollection<XCabClientIntegration> defaultAddressDetails);
    }
}
