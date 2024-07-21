using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class DateTimeHelpers
    {
        public static string GetUtcDateTimeForDriverBasedOnBusinessUnit(int driverNumber, DateTime datetime)
        {
            var timeZoneForJob = driverNumber >= 0 && driverNumber <= 1999
                ? TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                : (driverNumber >= 2000 && driverNumber <= 3999
                    ? TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                    : (driverNumber >= 4000 && driverNumber <= 5999
                        ? TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time")
                        : (driverNumber >= 6000 && driverNumber <= 7999
                            ? TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time")
                            : TimeZoneInfo.FindSystemTimeZoneById("Cen. Australia Standard Time"))));
            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(datetime,
                    timeZoneForJob);
            return utcDateTime.ToString("o");
        }
        public static DateTime GetLocalDateTimeFromUtc(int stateId, DateTime dateTime)
        {
            string timeZone = null;
            DateTime localDateTime = DateTime.MinValue;

            try
            {
                switch (stateId)
                {
                    case 1:
                    case 2:
                    case 3:
                        timeZone = "AUS Eastern Standard Time";
                        break;
                    case 4:
                        timeZone = "Cen. Australia Standard Time";
                        break;
                    case 5:
                        timeZone = "W. Australia Standard Time";
                        break;
                    case 9:
                        timeZone = "AUS Central Standard Time";
                        break;
                }

                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);

                localDateTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);

                if (stateId == 1 || stateId == 2 || stateId == 4)
                {
                    if (timeZoneInfo.IsDaylightSavingTime(localDateTime))
                    {
                        localDateTime.AddHours(1);
                    }
                }

                return localDateTime;
            }
            catch (Exception)
            {
                // Log errors
            }

            return localDateTime;

        }

        public static DayOfWeek IsAWeekDay(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime dateResult = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DayOfWeek dayOfWeek = dateResult.DayOfWeek;
                return dayOfWeek;
            }
            return DayOfWeek.Sunday;
        }
    }
}
