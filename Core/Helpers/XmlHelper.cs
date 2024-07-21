using System.Xml.Serialization;

namespace Core.Helpers
{
	public static class XmlHelper
	{
		public static string DesrializeRequest<T>(T manifest)
		{
			try
			{
				var stringWriter = new StringWriter();
				var serializer = new XmlSerializer(typeof(T));
				serializer.Serialize(stringWriter, manifest);
				return stringWriter.ToString();
			}
			catch
			{
				throw;
			}
		}
	}
}
