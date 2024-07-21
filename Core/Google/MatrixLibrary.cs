using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Google
{
    public class MatrixLibrary
    {
        public class RootObject
        {
            public List<string> DestinationAddresses { get; set; }
            public List<string> OriginAddresses { get; set; }
            public List<Row> Rows { get; set; }
            public string Status { get; set; }
        }

        #region Google Distance Matrix API Key
        private const string MatrixApiUrl = "https://maps.googleapis.com/maps/api/distancematrix";
        private const string DistanceApiUrl = "https://maps.googleapis.com/maps/api/directions";
        private const string DistanceApiUrlClientId = "https://maps.googleapis.com/maps/api/directions/json?";
        private const string DistanceApiUrlClientIdToSign = "/maps/api/directions/json?";
        private const string MatrixApiKey = "AIzaSyCQaXx-uwo-KzhaqDz-thct9enMZcPA5dM";
        private const string MatrixClientId = "gme-capitaltransport";
        private const string DistanceApiSignature = " AIzaSyBlQ8vjN_xyraB_XH19CWQvc3YIJkmosaQ";
        public const string GoogleCryptoKey = "34tTrzg3hYqAe4cSIiUA-jsiuAk=";
        private readonly bool _useClientId = true;
        private const string DistanceApiKey = " AIzaSyBlQ8vjN_xyraB_XH19CWQvc3YIJkmosaQ";
        private const string ResultType = "json";
        #endregion

        private const string Space = " ";
        private const string Separator = "|";

        public static string Sign(string url, string keyString)
        {
            Uri uri = null;
            var signature = "";
            try
            {
                uri = new Uri(url);
                var encoding = new ASCIIEncoding();
                // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
                var usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
                var privateKeyBytes = Convert.FromBase64String(usablePrivateKey);
                var encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);
                // compute the hash
                var algorithm = new HMACSHA1(privateKeyBytes);
                var hash = algorithm.ComputeHash(encodedPathAndQueryBytes);
                // convert the bytes to string and make url-safe by replacing '+' and '/' characters
                signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");
            }
            catch (Exception)
            {
                //swallow
            }
            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }

        [Obsolete]
        private List<Booking> GetWaypointOrdering(List<Booking> lstWaypoints, Booking startBooking, Booking endBooking,
            string stateName)
        {
            if (lstWaypoints == null) throw new ArgumentNullException(nameof(lstWaypoints));
            var orderedBookings = new List<Booking>();
            var origBookings = lstWaypoints;
            var origin = lstWaypoints[0].FromDetail2.Replace("&", Space) + Space + lstWaypoints[0].FromSuburb + Space +
                         stateName + " Australia";
            //  lstWaypoints.RemoveAt(0);
            //lstWaypoints.RemoveAt(lstWaypoints.Count-1);
            var waypoints = lstWaypoints.Aggregate("",
                (current, booking) =>
                    current + booking.ToDetail2.Replace("&", Space) + Space + booking.ToSuburb + Space + stateName +
                    " Australia" + Separator);
            var destination = endBooking.ToDetail2.Replace("&", " ") + Space +
                              lstWaypoints[lstWaypoints.Count - 1].ToSuburb + Space +
                              stateName + " Australia";
            var apiUrl = "";
            if (!_useClientId)
                apiUrl = DistanceApiUrl + "/" + ResultType
                         + "?origin=" + origin
                         + "&destination=" + destination
                         + "&waypoints=optimize:true|" + waypoints
                         + "&key=" + DistanceApiKey;
            else
            {
                var urlForSign = DistanceApiUrlClientId + "origin=" + origin
                                 + "&destination=" + destination
                                 + "&client=" + MatrixClientId
                                 + "&waypoints=optimize:true|" + waypoints;
                var signature = Sign(urlForSign.Replace(" ", "+"), GoogleCryptoKey);
                apiUrl = signature;
            }
            return GetOrderedBookingsFromGoogleApi(lstWaypoints, apiUrl);
        }

        private static List<Booking> GetOrderedBookingsFromGoogleApi(IReadOnlyList<Booking> bookings, string apiUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(apiUrl);
            var orderedBookings = new List<Booking>();
            request.UserAgent = "Mozilla/4.0";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                if (string.IsNullOrEmpty(result)) return orderedBookings;
                var rootObj = JsonConvert.DeserializeObject<Core.Google.RootObject>(result);
                if (rootObj.status.ToUpper() != "OK") return orderedBookings;
                if (rootObj.routes.Count > 0)
                    orderedBookings.AddRange(from long order in rootObj.routes[0].waypoint_order
                                             select bookings[(int)order]);
                return orderedBookings;
            }
        }

        private static string GetUrlEncoding(string inputUrl)
        {
            return WebUtility.UrlDecode(inputUrl);
        }
        public List<Booking> GetWaypointOrdereBookings(List<Booking> lstBookings, Booking startBooking,
            Booking endBooking)
        {
            var orderedBookings = new List<Booking>();
            var choppedList = false;
            List<Booking> extraBookings = null;
            try
            {
                if ((lstBookings == null) || !lstBookings.Any() || (startBooking == null) || (endBooking == null))
                    return orderedBookings;

                //Google waypoint limit of 23: http://stackoverflow.com/questions/8779886/exceed-23-waypoint-per-request-limit-on-google-directions-api-business-work-lev

                if (lstBookings.Count > 23) //maximum number of bookings that the api can handle? check
                {
                    extraBookings = lstBookings.Skip(23).Take(lstBookings.Count - 23).ToList();
                    lstBookings = lstBookings.Take(23).ToList();
                    choppedList = true;
                }
                var origin = "";
                //we need an origin - assumption is that the first booking is the start, unfortunately we do not have
                // a defined way of detecting the start booking - there were ideas to use driver's home location
                // but has not been standradised/discussed further
                var stateName = "";
                switch (lstBookings[0].StateId)
                {
                    case "1":
                        stateName = "VIC";
                        break;
                    case "2":
                        stateName = "NSW";
                        break;
                    case "3":
                        stateName = "QLD";
                        break;
                    case "4":
                        stateName = "SA";
                        break;
                    case "5":
                        stateName = "WA";
                        break;
                    case "6":
                        stateName = "NAT";
                        break;
                    case "7":
                        stateName = "ACT";
                        break;
                }

                origin = startBooking.FromDetail2.Replace("&", " ") + " " + startBooking.FromSuburb + " " +
                         stateName +
                         " Australia";
                var waypoints = lstBookings.Aggregate("",
                    (current, booking) =>
                        current + booking.ToDetail2.Replace("&", " ") + " " + booking.ToSuburb + " " + stateName +
                        " Australia" + "|");
                //String destination = lstBookings.Aggregate("", (current, booking) => current + (booking.ToDetail2.Replace("&", space) + space + booking.ToSuburb + space + stateName + " Australia" + separator));

                var destination = endBooking.ToDetail2.Replace("&", " ") + " " + endBooking.ToSuburb + " " +
                                  stateName + " Australia";
                var apiUrl = "";
                if (!_useClientId)
                    apiUrl = DistanceApiUrl + "/" + ResultType
                             + "?origin=" + origin
                             + "&destination=" + destination
                             + "&waypoints=optimize:true|" + waypoints
                             + "&key=" + DistanceApiKey;
                else
                {
                    var urlForSign = DistanceApiUrlClientId + "origin=" + origin
                                     + "&destination=" + destination
                                     + "&client=" + MatrixClientId
                                     + "&waypoints=optimize:true|" + waypoints;
                    //urlForSign = Uri.EscapeDataString(urlForSign);
                    var signature = Sign(urlForSign.Replace(" ", "+"), GoogleCryptoKey);
                    apiUrl = signature;
                }
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.UserAgent = "Mozilla/4.0";
                //request.Headers.Add("user-agent",
                //    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                var response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (string.IsNullOrEmpty(result)) return orderedBookings;
                    var rootObj = JsonConvert.DeserializeObject<Core.Google.RootObject>(result);
                    if (rootObj.status.ToUpper() != "OK") return orderedBookings;
                    if (rootObj.routes.Count > 0)
                        orderedBookings.AddRange(from long order in rootObj.routes[0].waypoint_order
                                                 select lstBookings[(int)order]);
                    // orderedBookings.Add(endBooking);
                    // orderedBookings.Insert(0, startBooking);
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while using Matrix Library in method GetWayPointOrderBookings, Exception Details:" +
                    e.Message, "EdifactExtractor");
            }
            if (choppedList)
                orderedBookings.AddRange(extraBookings);
            return orderedBookings;
        }

        public Booking GetDistanceMatrixEndPoint(List<Booking> lstBookings)
        {
            Booking matrixEndBooking = null;
            try
            {
                if ((lstBookings == null) || !lstBookings.Any())
                    return null;

                var origin = "";
                const string space = " ";
                const string separator = "|";
                //we need an origin - assuming 
                var stateName = "";
                switch (lstBookings[0].StateId)
                {
                    case "1":
                        stateName = "VIC";
                        break;
                    case "2":
                        stateName = "NSW";
                        break;
                    case "3":
                        stateName = "QLD";
                        break;
                    case "4":
                        stateName = "SA";
                        break;
                    case "5":
                        stateName = "WA";
                        break;
                    case "6":
                        stateName = "NAT";
                        break;
                    case "7":
                        stateName = "ACT";
                        break;
                    default:
                        stateName = "";
                        break;
                }

                //create origin location
                origin = lstBookings[0].FromDetail2.Replace("&", space) + space + lstBookings[0].FromSuburb + space +
                         stateName +
                         " Australia";
                //create destination location
                var destination = lstBookings.Aggregate("",
                    (current, booking) =>
                        current + booking.ToDetail2.Replace("&", space) + space + booking.ToSuburb + space +
                        stateName + " Australia" + separator);
                //create API URL
                var apiUrl = MatrixApiUrl + "/" + ResultType + "?origins=" + origin + "&destinations=" + destination +
                             "&key=" + MatrixApiKey;
                //create HTTP Web Request
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                //Create Resonse Objct
                var response = (HttpWebResponse)request.GetResponse();
                //setup objects to parse the API Result
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (string.IsNullOrEmpty(result)) return null;
                    //parse the results into Google Object
                    var rootObj = JsonConvert.DeserializeObject<RootObject>(result);
                    //iterate through the root elemnts and find the location that is fartheset
                    double maxDistance = 0;
                    var wayPointIndex = -1;
                    foreach (var rows in rootObj.Rows)
                    {
                        var index = 0;
                        foreach (var element in rows.Elements)
                        {
                            if ((element.distance != null) && (element.distance.Value > maxDistance))
                            {
                                maxDistance = element.distance.Value;
                                wayPointIndex = index;
                            }
                            index++;
                        }
                    }
                    if ((wayPointIndex != -1) && (maxDistance != 0))
                        matrixEndBooking = lstBookings[wayPointIndex];
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while using Matrix Library in method GetDistanceMatrixEndPoint, Exception Details:" +
                    e.Message, "EdifactExtractor");
            }
            return matrixEndBooking;
        }

        public Booking GetDistanceMatrixEndPoint(List<Booking> lstBookings, Booking originBooking)
        {
            Booking matrixEndBooking = null;
            try
            {
                if ((lstBookings == null) || !lstBookings.Any())
                    return null;

                var origin = "";
                const string space = " ";
                const string separator = "|";
                //we need an origin - assuming 
                var stateName = "";
                switch (lstBookings[0].StateId)
                {
                    case "1":
                        stateName = "VIC";
                        break;
                    case "2":
                        stateName = "NSW";
                        break;
                    case "3":
                        stateName = "QLD";
                        break;
                    case "4":
                        stateName = "SA";
                        break;
                    case "5":
                        stateName = "WA";
                        break;
                    case "6":
                        stateName = "NAT";
                        break;
                    case "7":
                        stateName = "ACT";
                        break;
                    default:
                        stateName = "";
                        break;
                }

                //create origin location
                origin = originBooking.FromDetail2.Replace("&", space) + space + lstBookings[0].FromSuburb + space +
                         stateName +
                         " Australia";
                //create destination location
                var destination = lstBookings.Aggregate("",
                    (current, booking) =>
                        current + booking.ToDetail2.Replace("&", space) + space + booking.ToSuburb + space +
                        stateName + " Australia" + separator);
                //create API URL
                var apiUrl = MatrixApiUrl + "/" + ResultType + "?origins=" + origin + "&destinations=" + destination +
                             "&key=" + MatrixApiKey;
                //create HTTP Web Request
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                //Create Resonse Objct
                var response = (HttpWebResponse)request.GetResponse();
                //setup objects to parse the API Result
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (string.IsNullOrEmpty(result)) return null;
                    //parse the results into Google Object
                    var rootObj = JsonConvert.DeserializeObject<RootObject>(result);
                    //iterate through the root elemnts and find the location that is fartheset
                    double maxDistance = 0;
                    var wayPointIndex = -1;
                    foreach (var rows in rootObj.Rows)
                    {
                        var index = 0;
                        foreach (var element in rows.Elements)
                        {
                            if (element.distance.Value > maxDistance)
                            {
                                maxDistance = element.distance.Value;
                                wayPointIndex = index;
                            }
                            index++;
                        }
                    }
                    if ((wayPointIndex != -1) && (maxDistance != 0))
                        matrixEndBooking = lstBookings[wayPointIndex];
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while using Matrix Library in method GetDistanceMatrixEndPoint, Exception Details:" +
                    e.Message, "EdifactExtractor");
            }
            return matrixEndBooking;
        }

    }
}
