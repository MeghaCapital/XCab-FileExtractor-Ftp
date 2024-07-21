namespace Core.Helpers
{
	public class IkeaHelper
	{
		public static List<string> ikeaAccounts = new List<string> { "KMIKC", "KMIKCC", "KMIKR", "KMIKS", "KSIKR", "KPIKP" };

		public static string GetIkeaEmailAddress(int stateId)
		{
			var recipient = stateId switch
			{
				1 => "IkeaVIC@kingstransport.com.au",
				2 => "IkeaNSW@kingstransport.com.au",
				5 => "IkeaWA@kingstransport.com.au",
				_ => ""
			};
			return recipient;
		}

		public static string GetIkeaEmailAddressGroups(int stateId)
		{
			var recipient = stateId switch
			{
				1 => "ikea.melbourne@kingsgroup.com.au",
				2 => "ikea.sydney@kingsgroup.com.au",
				5 => "ikea.perth@kingsgroup.com.au",
				_ => ""
			};
			return recipient;
		}

		public static bool IsAnIkeaAccount(string accountCode)
		{
			return ikeaAccounts.Contains(accountCode);
		}

		public static bool IsReturnServiceCode(string serviceCode)
		{
			return !string.IsNullOrWhiteSpace(serviceCode) && ((serviceCode.ToUpper().StartsWith("X") || serviceCode.ToUpper().StartsWith("R")));
		}

		public static bool IsValidDeliveryServiceCode(string serviceCode)
		{
			return !string.IsNullOrWhiteSpace(serviceCode) && serviceCode.ToUpper().StartsWith("I") && !serviceCode.ToUpper().Equals("ICAN");
		}
	}
}