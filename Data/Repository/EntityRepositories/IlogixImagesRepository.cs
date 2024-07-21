using Data.Entities.Ilogix;
using Data.Repository.EntityRepositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Repository.EntityRepositories
{
	public class IlogixImagesRepository : IIlogixImagesRepository
	{
		public async Task<ICollection<PodImage>> GetPodImage(string jobnumber, string statePrefix, DateTime jobDate)
		{
			var PodImages = new List<PodImage>();
			if (jobDate != DateTime.MinValue && !string.IsNullOrWhiteSpace(jobnumber) && !string.IsNullOrWhiteSpace(statePrefix))
			{
				var year = jobDate.Year;
				var month = jobDate.Month;
				var day = jobDate.Day;
				//get the ilogix job number
				var ilogixJobNumber = day.ToString().PadLeft(2, '0') + month.ToString().PadLeft(2, '0') + year.ToString().Substring(2) +
									  statePrefix + jobnumber.PadLeft(8, '0');
				var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
				var _defaultTableName = "PodImages";
				var isLiveTable = false;
				year = Convert.ToInt32(year.ToString().Substring(2));
				var requestedDateTime = new DateTime(2000 + year, month, day);
				var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

				if (currentDateTime.Subtract(requestedDateTime).Days < 7)
				{
					isLiveTable = true;
				}
				if (!isLiveTable)
				{
					_defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
										Convert.ToString(month).PadLeft(2, '0');
				}

				var sql = "SELECT JobNumber, SubJobNumber, Image FROM "
					  + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumber + "'";

				var connectionString = DbSettings.Default.ILogixWebProAppConnectionString;
				using (var sqlConnection = new MySqlConnection(connectionString))
				{
					try
					{
						await sqlConnection.OpenAsync();
						using var cmd = new MySqlCommand(sql, sqlConnection);
						using var reader = await cmd.ExecuteReaderAsync();

						while (reader.Read())
						{
							var PodImage = new PodImage
							{
								JobNumber = reader["jobnumber"].ToString(),
								SubJobNumber = reader["subjobnumber"].ToString(),
								podImage = (byte[])reader["image"],
								TPLUS_JobNumber = jobnumber,
								JobDateTime = jobDate
							};
							PodImages.Add(PodImage);
						}
						if (!reader.IsClosed)
							reader.Close();
					}
					catch (Exception ex)
					{
						await Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPodImage, message: " + ex.Message, "ILogixImagesRepository");
					}
					finally
					{
						if (sqlConnection.State == ConnectionState.Open)
						{
							sqlConnection.Close();
						}
					}
				}
			}
			return PodImages;
		}

		public ICollection<PodImage> GetPodImage(string jobnumber, string subJobNumber, string statePrefix, DateTime jobDate)
		{
			var PodImages = new List<PodImage>();
			if (jobDate != DateTime.MinValue && !string.IsNullOrWhiteSpace(jobnumber) && !string.IsNullOrWhiteSpace(statePrefix) && !string.IsNullOrWhiteSpace(subJobNumber))
			{
				var year = jobDate.Year;
				var month = jobDate.Month;
				var day = jobDate.Day;
				//get the ilogix job number
				var ilogixJobNumber = day.ToString().PadLeft(2, '0') + month.ToString().PadLeft(2, '0') + year.ToString().Substring(2) +
									  statePrefix + jobnumber.PadLeft(8, '0');

				var sql = "";
				var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
				var _defaultTableName = "PodImages";
				var isLiveTable = false;
				year = Convert.ToInt32(year.ToString().Substring(2));
				var requestedDateTime = new DateTime(2000 + year, month, day);
				var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

				if (currentDateTime.Subtract(requestedDateTime).Days < 7)
				{
					isLiveTable = true;
				}
				if (!isLiveTable)
				{
					_defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
										Convert.ToString(month).PadLeft(2, '0');
				}

				sql = "SELECT JobNumber, SubJobNumber, Image, PODName FROM "
					  + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";

				var connectionString = DbSettings.Default.ILogixWebProAppConnectionString;
				using (var sqlConnection = new MySqlConnection(connectionString))
				{
					try
					{
						sqlConnection.Open();
						using var cmd = new MySqlCommand(sql, sqlConnection);
						using var reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							var PodImage = new PodImage
							{
								JobNumber = reader["jobnumber"].ToString(),
								SubJobNumber = reader["subjobnumber"].ToString(),
								podImage = (byte[])reader["image"],
								TPLUS_JobNumber = jobnumber,
								JobDateTime = jobDate,
								PODName = reader["PODName"].ToString()
							};
							PodImages.Add(PodImage);
						}
						if (!reader.IsClosed)
							reader.Close();
					}
					catch (Exception ex)
					{
						Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPodImage(base on the SubJobNumber), message: " + ex.Message, "ILogixImagesRepository");
					}
					finally
					{
						if (sqlConnection.State == ConnectionState.Open)
						{
							sqlConnection.Close();
						}
					}
				}
			}
			return PodImages;
		}

		public ICollection<PocImage> GetPocImage(string jobnumber, string statePrefix, DateTime jobDate)
		{
			var PocImages = new List<PocImage>();
			if (jobDate != DateTime.MinValue && !string.IsNullOrWhiteSpace(jobnumber) && !string.IsNullOrWhiteSpace(statePrefix))
			{
				var year = jobDate.Year;
				var month = jobDate.Month;
				var day = jobDate.Day;
				//get the ilogix job number
				var ilogixJobNumber = day.ToString().PadLeft(2, '0') + month.ToString().PadLeft(2, '0') + year.ToString().Substring(2) +
									  statePrefix + jobnumber.PadLeft(8, '0');

				var sql = "";
				var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
				var _defaultTableName = "Poc";
				var isLiveTable = false;
				year = Convert.ToInt32(year.ToString().Substring(2));
				var requestedDateTime = new DateTime(2000 + year, month, day);
				var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

				if (currentDateTime.Subtract(requestedDateTime).Days < 7)
				{
					isLiveTable = true;
				}
				if (!isLiveTable)
				{
					_defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
										Convert.ToString(month).PadLeft(2, '0');
				}

				sql = "SELECT JobNumber, SubJobNumber, Picture FROM "
					  + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumber + "'";

				var connectionString = DbSettings.Default.ILogixWebProAppConnectionString;
				using (var sqlConnection = new MySqlConnection(connectionString))
				{
					try
					{
						sqlConnection.Open();
						using var cmd = new MySqlCommand(sql, sqlConnection);
						using var reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							var PocImage = new PocImage
							{
								JobNumber = reader["jobnumber"].ToString(),
								SubJobNumber = reader["subjobnumber"].ToString(),
								pocImage = (byte[])reader["Picture"],
								TPLUS_JobNumber = jobnumber,
								JobDateTime = jobDate
							};
							PocImages.Add(PocImage);
						}
						if (!reader.IsClosed)
							reader.Close();
					}
					catch (Exception ex)
					{
						Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPocImage, message: " + ex.Message + ". Job number: " + ilogixJobNumber, "ILogixImagesRepository");
					}
					finally
					{
						if (sqlConnection.State == ConnectionState.Open)
						{
							sqlConnection.Close();
						}
					}
				}
			}
			return PocImages;

		}

		public ICollection<PocImage> GetPocImage(string jobnumber, string subJobNumber, string statePrefix, DateTime jobDate)
		{
			var imageFound = false;
			var PocImages = new List<PocImage>();
			if (jobDate != DateTime.MinValue && !string.IsNullOrWhiteSpace(jobnumber) && !string.IsNullOrWhiteSpace(statePrefix) && !string.IsNullOrWhiteSpace(subJobNumber))
			{
				var year = jobDate.Year;
				var month = jobDate.Month;
				var day = jobDate.Day;
				//get the ilogix job number
				var ilogixJobNumber = day.ToString().PadLeft(2, '0') + month.ToString().PadLeft(2, '0') + year.ToString().Substring(2) +
									  statePrefix + jobnumber.PadLeft(8, '0');

				var sql = "";
				var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
				var _defaultTableName = "Poc";
				var isLiveTable = false;
				year = Convert.ToInt32(year.ToString().Substring(2));
				var requestedDateTime = new DateTime(2000 + year, month, day);
				var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
				if (requestedDateTime > currentDateTime)
					return PocImages;
				if (currentDateTime.Subtract(requestedDateTime).Days < 7)
				{
					isLiveTable = true;
				}
				if (!isLiveTable)
				{
					_defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
										Convert.ToString(month).PadLeft(2, '0');
				}

				sql = "SELECT JobNumber, SubJobNumber, Picture FROM "
					  + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'"
					  + " UNION SELECT JobNumber, SubJobNumber, Picture FROM POC WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";

                var connectionString = DbSettings.Default.ILogixWebProAppConnectionString;
				using (var sqlConnection = new MySqlConnection(connectionString))
				{
					try
					{
						sqlConnection.Open();
						using var cmd = new MySqlCommand(sql, sqlConnection);
						using var reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							var PocImage = new PocImage
							{
								JobNumber = reader["jobnumber"].ToString(),
								SubJobNumber = reader["subjobnumber"].ToString(),
								pocImage = (byte[])reader["Picture"],
								TPLUS_JobNumber = jobnumber,
								JobDateTime = jobDate
							};
							imageFound = true;
							PocImages.Add(PocImage);
						}
						//close the reader if open
						if (!reader.IsClosed)
							reader.Close();
						if (!imageFound)
						{
							for (var i = 1; i <= 14; i++)
							{
								var requestedDateTimeFix = requestedDateTime.AddDays(i);
								if (requestedDateTimeFix > currentDateTime)
									break;
								var ilogixJobNumberFix = requestedDateTimeFix.Day.ToString().PadLeft(2, '0') + requestedDateTimeFix.Month.ToString().PadLeft(2, '0') + year.ToString() +
										 statePrefix + jobnumber.PadLeft(8, '0');
								if (currentDateTime.Subtract(requestedDateTimeFix).Days < 7)
								{
									isLiveTable = true;
									_defaultTableName = "Poc";
								}
								if (!isLiveTable)
								{
									// If defaultTableName is poc_2020_11 then because of above line it append one more 2020_11
									// and table name becomes – poc_2020_11_2020_11 which is invalid table name and it end up in Catch block. 
									if (!_defaultTableName.Contains(century))
										_defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
															Convert.ToString(month).PadLeft(2, '0');
									else
										_defaultTableName = "Poc" + century + Convert.ToString(year) + "_" +
															Convert.ToString(month).PadLeft(2, '0');
								}

								var sqlJobNumberFix = "SELECT JobNumber, SubJobNumber, Picture FROM "
									  + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumberFix + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";

								using var cmdJobNumberFix = new MySqlCommand(sqlJobNumberFix, sqlConnection);
								using var readerJobNumberFix = cmdJobNumberFix.ExecuteReader();
								while (readerJobNumberFix.Read())
								{
									var PocImage = new PocImage
									{
										JobNumber = readerJobNumberFix["jobnumber"].ToString(),
										SubJobNumber = readerJobNumberFix["subjobnumber"].ToString(),
										pocImage = (byte[])readerJobNumberFix["Picture"],
										TPLUS_JobNumber = jobnumber,
										JobDateTime = requestedDateTimeFix
									};
									imageFound = true;
									PocImages.Add(PocImage);
								}
								//close the reader if open
								if (!readerJobNumberFix.IsClosed)
									readerJobNumberFix.Close();
								if (imageFound)
									break;
							}
						}

						if (!imageFound)
						{
							// Most of the time last day of month POC stores into next month table. 
							// Eg - 301120A00254692 is stored in Poc_2020_12 table. So above code does not returns any image. 
							// Below code to fix that issue. 
							var newTableName = "Poc";
							var newYear = year;
							var newMonth = month;
							if (month == 12)
							{
								newMonth = 1;
								newYear = year + 1;
							}
							else
							{
								newMonth = month + 1;
							}
							newTableName = "Poc" + century + Convert.ToString(newYear) + "_" +
												Convert.ToString(newMonth).PadLeft(2, '0');
							if (IsTableExist(newTableName, sqlConnection))
							{
								sql = "SELECT JobNumber, SubJobNumber, Picture FROM "
									  + newTableName + " WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" +
									  subJobNumber.PadLeft(2, '0') + "'";

								using var newCommand = new MySqlCommand(sql, sqlConnection);
								using var newReader = newCommand.ExecuteReader();
								while (newReader.Read())
								{
									var PocImage = new PocImage
									{
										JobNumber = newReader["jobnumber"].ToString(),
										SubJobNumber = newReader["subjobnumber"].ToString(),
										pocImage = (byte[])newReader["Picture"],
										TPLUS_JobNumber = jobnumber,
										JobDateTime = jobDate
									};
									imageFound = true;
									PocImages.Add(PocImage);
								}

								//close the reader if open
								if (!newReader.IsClosed)
									newReader.Close();
							}

						}

						// This might be redudant code but just for fall back option 
						if (!imageFound)
						{
							sql = "SELECT JobNumber, SubJobNumber, Picture FROM Poc "
							   + "WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";

							using var command = new MySqlCommand(sql, sqlConnection);
							using var mySqlDataReader = cmd.ExecuteReader();

							while (mySqlDataReader.Read())
							{
								var PocImage = new PocImage
								{
									JobNumber = mySqlDataReader["jobnumber"].ToString(),
									SubJobNumber = mySqlDataReader["subjobnumber"].ToString(),
									pocImage = (byte[])mySqlDataReader["Picture"],
									TPLUS_JobNumber = jobnumber,
									JobDateTime = jobDate
								};
								imageFound = true;
								PocImages.Add(PocImage);
							}

							//close the reader if open
							if (!mySqlDataReader.IsClosed)
								mySqlDataReader.Close();
						}
					}
					catch (Exception ex)
					{
						Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPocImage(based on the SubJobNumber), message: " + ex.Message, "ILogixImagesRepository");
					}
					finally
					{
						if (sqlConnection.State == ConnectionState.Open)
						{
							sqlConnection.Close();
						}
					}
				}
			}
			return PocImages;
		}

		public ICollection<PodImage> GetPodImageV2(string iLogixJobNumber, string subJobNumber, bool useArchiveDatabase = false)
		{
			bool imageFound = false;
			var PodImages = new List<PodImage>();
			if (!string.IsNullOrWhiteSpace(iLogixJobNumber) && !string.IsNullOrWhiteSpace(subJobNumber))
			{
				//get day
				int day = int.Parse(iLogixJobNumber.Substring(0, 2));
				//get month
				int month = int.Parse(iLogixJobNumber.Substring(2, 2));
				//get year
				int year = int.Parse(iLogixJobNumber.Substring(4, 2));
				var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
				var _defaultTableName = "PodImages";
				var isLiveTable = false;
				//year = Convert.ToInt32(year.ToString().Substring(2));
				var requestedDateTime = new DateTime(2000 + year, month, day);
				var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

				if (currentDateTime.Subtract(requestedDateTime).Days < 7)
				{
					isLiveTable = true;
				}
				if (!isLiveTable)
				{
					_defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
										Convert.ToString(month).PadLeft(2, '0');
				}

				string sql = "SELECT JobNumber, SubJobNumber, Image, PODName FROM "
		  + _defaultTableName + " WHERE JobNumber='" + iLogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";

				var connectionString = DbSettings.Default.ILogixMysqlConnection;
				if (useArchiveDatabase)
				{
					var ILogixSettingsrepository = new ILogixSettingsrepository();
					var usingArchiveDatabase = ILogixSettingsrepository.IsUsingArchivalDatabase(requestedDateTime);
					if (usingArchiveDatabase)
					{
						connectionString = DbSettings.Default.ILogixArchivedDbConnectionString;
					}
				}

				using (var sqlConnection = new MySqlConnection(connectionString))
				{
					try
					{
						sqlConnection.Open();
						if (IsTableExist(_defaultTableName, sqlConnection))
						{
							using (var cmd = new MySqlCommand(sql, sqlConnection))
							{
								using (var reader = cmd.ExecuteReader())
								{
									while (reader.Read())
									{
										var PodImage = new PodImage
										{
											JobNumber = reader["jobnumber"].ToString(),
											SubJobNumber = reader["subjobnumber"].ToString(),
											podImage = (byte[])reader["image"],
											TPLUS_JobNumber = iLogixJobNumber,
											PODName = reader["PODName"].ToString()
										};
										PodImages.Add(PodImage);
										imageFound = true;
									}
									if (!reader.IsClosed)
										reader.Close();
								}
							}
						}
						if (!imageFound)
						{
							//check if this POD is in the monthly table instead
							try
							{
								if (isLiveTable)
								{
									var _century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
									_defaultTableName = "podimages" + _century + Convert.ToString(year) + "_" + Convert.ToString(month).PadLeft(2, '0');
									sql = "SELECT JobNumber, SubJobNumber, Image, PODName FROM "
											  + _defaultTableName + " WHERE JobNumber='" + iLogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";
									if (IsTableExist(_defaultTableName, sqlConnection))
									{
										using (var cmd = new MySqlCommand(sql, sqlConnection))
										{
											using (var reader = cmd.ExecuteReader())
											{
												while (reader.Read())
												{
													var PodImage = new PodImage
													{
														JobNumber = reader["jobnumber"].ToString(),
														SubJobNumber = reader["subjobnumber"].ToString(),
														podImage = (byte[])reader["image"],
														TPLUS_JobNumber = iLogixJobNumber,
														PODName = reader["PODName"].ToString()
													};
													PodImages.Add(PodImage);
													imageFound = true;
												}
												if (!reader.IsClosed)
													reader.Close();
											}
										}
									}
								}
							}
							catch (Exception ex)
							{
								Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPodImageV2, message: " + ex.Message, "ILogixImagesRepository");
							}
							if (!imageFound)
							{
								//This is a scenario for jobs completed nearing the end of the month, the pods are stored in the next months table
								//roll over to the next month table and do a check
								if (month == 12)
								{
									year++;
									month = 1;
								}
								else
								{
									month++;
								}
								try
								{
									var _century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
									_defaultTableName = "podimages" + _century + Convert.ToString(year) + "_" + Convert.ToString(month).PadLeft(2, '0');
									if (IsTableExist(_defaultTableName, sqlConnection))
									{
										sql = "SELECT JobNumber, SubJobNumber, Image, PODName FROM "
												+ _defaultTableName + " WHERE JobNumber='" + iLogixJobNumber + "' AND SubJobNumber='" + subJobNumber.PadLeft(2, '0') + "'";
										using (var cmd = new MySqlCommand(sql, sqlConnection))
										{
											using (var reader = cmd.ExecuteReader())
											{
												while (reader.Read())
												{
													var PodImage = new PodImage
													{
														JobNumber = reader["jobnumber"].ToString(),
														SubJobNumber = reader["subjobnumber"].ToString(),
														podImage = (byte[])reader["image"],
														TPLUS_JobNumber = iLogixJobNumber,
														PODName = reader["PODName"].ToString()
													};
													PodImages.Add(PodImage);
													imageFound = true;
												}
												if (!reader.IsClosed)
													reader.Close();
											}
										}
									}
								}
								catch (Exception ex)
								{
									Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPodImageV2, message: " + ex.Message + ". Job number: " + iLogixJobNumber, "ILogixImagesRepository");
								}
							}
						}
					}
					catch (Exception ex)
					{
						Core.Logger.Log("Exception Occurred in ILogixImagesRepository: GetPodImageV2, message: " + ex.Message + ". Job number: " + iLogixJobNumber, "ILogixImagesRepository");
					}
					finally
					{
						if (sqlConnection.State == ConnectionState.Open)
						{
							sqlConnection.Close();
						}
					}
				}
			}
			return PodImages;
		}

		public static bool IsTableExist(string tableName, MySqlConnection sqlConnection)
		{
			var isTableExist = false;
			if (sqlConnection.State == ConnectionState.Open)
			{
				if (!string.IsNullOrEmpty(tableName))
				{
					var sql = $"select TABLE_NAME from information_schema.tables where table_name='{tableName}'";
					try
					{
						var tableExistcmd = new MySqlCommand(sql, sqlConnection);
						var record = tableExistcmd.ExecuteScalar();

						if (record != null &&
							record.ToString() != String.Empty &&
							record.ToString().ToLower().Equals(tableName.ToLower()))
						{
							isTableExist = true;
						}
					}
					catch (Exception ex)
					{
						Core.Logger.Log("Exception Occurred in ILogixImagesRepository: IsTableExist, message: " + ex.Message, "ILogixImagesRepository");
					}
				}
			}

			return isTableExist;
		}
	}
}
