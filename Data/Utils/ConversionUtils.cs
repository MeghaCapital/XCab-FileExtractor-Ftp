using Core;
using Data.Entities.Remark;
using Data.Entities.Sundries;

namespace Data.Utils
{
    public static class ConversionUtils
    {
        public static XCabSundry GetXCabSundry(List<Sundry> sundries, int bookingId)
        {
            var xCabSundry = new XCabSundry();
            if (bookingId == 0)
                return null;
            xCabSundry.BookingId = bookingId;
            if (sundries.Count >= 1 && sundries[0] != null)
            {
                var sundry = sundries[0];
                if (sundry.Code != null)
                {
                    xCabSundry.Service1 = sundry.Code.ToString();
                    xCabSundry.Qty1 = sundry.Quantity;
                }
            }
            if (sundries.Count >= 2 && sundries[1] != null)
            {
                var sundry = sundries[1];
                if (sundry.Code != null)
                {
                    xCabSundry.Service2 = sundry.Code.ToString();
                    xCabSundry.Qty2 = sundry.Quantity;
                }
            }
            if (sundries.Count >= 3 && sundries[2] != null)
            {
                var sundry = sundries[2];
                if (sundry.Code != null)
                {
                    xCabSundry.Service3 = sundry.Code.ToString();
                    xCabSundry.Qty3 = sundry.Quantity;
                }
            }
            if (sundries.Count >= 4 && sundries[3] != null)
            {
                var sundry = sundries[3];
                if (sundry.Code != null)
                {
                    xCabSundry.Service4 = sundry.Code.ToString();
                    xCabSundry.Qty4 = sundry.Quantity;
                }
            }
            return xCabSundry;
        }
        public static List<string> GetXCabRemark(ICollection<Remark> remarks)
        {
            var listRemarks = new List<string>();
            foreach (var remark in remarks)
            {
                if (!string.IsNullOrEmpty(remark.RemarkText))
                {
                    listRemarks.Add(remark.RemarkText);
                }
            }
            return listRemarks;
        }
    }
}
