using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
	public class HashHelpers
	{
		public static byte[] GetHash(string inputString)
		{
			HashAlgorithm algorithm = SHA1.Create();
			return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
		}

		public static string GetHashString(string inputString)
		{
			var sb = new StringBuilder();
			foreach (var b in GetHash(inputString))
				sb.Append(b.ToString("X2"));

			return sb.ToString();
		}

		public static string CalculateMD5Hash(string input)
		{
			// step 1, calculate MD5 hash from input
			System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("x2"));
			}
			return sb.ToString();
		}
	}
}
