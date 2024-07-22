using Data.Api.Bookings.Tms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XCabService.RequestValidationService
{
    public interface IRequestValidationProvider
    {
        bool IsRequestValid(Data.Api.Bookings.BookingModel.OnlineBooking onlineBooking, bool IsPostcodeOptional = false);
        bool IsBasfRequestValid(XElement manifest, bool IsPostcodeOptional = false);
        bool IsRequestValid(string accountCode, string serviceCode, string fromSuburb, string fromPostCode, string toSuburb, string toPostCode, string toAddressLine1, string fromAddressLine);
    }
}
