using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class CollectionsExtension
    {
        public static IEnumerable<IGrouping<string, Booking>> GetUniqueBookings(List<Booking> bookings)
        {
            var groupedBookings = bookings.GroupBy(x => x.ToSuburb);
            return groupedBookings;
        }
    }
}
