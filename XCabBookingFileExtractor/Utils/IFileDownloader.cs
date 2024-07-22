using Core;
using System.Collections.Generic;

namespace XCabBookingFileExtractor.Utils
{
	public interface IFileDownloader
	{
		void DownloadFtpCsvFiles(List<LoginDetails> lstLoginDetail);

		ICollection<string> DownloadFtpCsvFiles(LoginDetails login);
	}
}