using Core;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace XCabBookingFileExtractor.Utils.Sms
{
    public class SmsNotification
    {
        #region Constants
        private const string SmsApiBaseAddress = "http://p1-nt-app01:4200/";
        private const string SmsApiAddressSuffix = "sms/send";
        private const string WebApiKey = "SdiF8F9EBQUlV90B";
        #endregion

        public static async Task<bool> SendSmsNotitification(SMSContent smsContent)
        {
            bool isSmsSent = true;
            var client = new HttpClient
            {
                BaseAddress = new Uri(SmsApiBaseAddress)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(SmsApiAddressSuffix, smsContent);
                response.EnsureSuccessStatusCode();
                var statusCode = response.StatusCode;
                if (statusCode != HttpStatusCode.OK)
                {
                    Logger.Log(
                        $"Failed to call SMS portal api while sending sms to {smsContent.MobileNumber}.Status code:{statusCode}", "SendSmsNotitification");
                    isSmsSent = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(
                    $"Exception Occurred while moving sending sms Notification. Details are : {ex.Message}", "SendSmsNotitification");
                isSmsSent = false;
            }

            return isSmsSent;
        }

        public static bool IsSmsNotificationRequired { get; set; }
    }
}
