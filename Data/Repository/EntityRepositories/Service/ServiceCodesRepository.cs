using Core;
using Core.Models.Slack;
using Dapper;
using Data.Model.ServiceCodes;
using Data.Repository.EntityRepositories.DeliverySuburb;
using Data.Repository.EntityRepositories.Service.Interface;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.Service
{
    /// <summary>
    ///Repository to hold Service code related elements
    /// </summary>
    public class ServiceCodesRepository : IServiceCodesRepository
    {
        /// <summary>
        /// Get a list of Service codes based on a criteria and given number of Pallets
        /// </summary>
        /// <param name="serviceCodeRequestModel"></param>
        /// <returns></returns>
        public ICollection<ServiceCodeFilter> GetServiceCodesPallets(ShipmentModel.ServiceQuoteRequest serviceCodeRequestModel)
        {
            try
            {
                //Retrieve data afor the give criteria
                ICollection<ServiceCodeFilter> ServiceCodes = null;
                var dbArgs = new DynamicParameters();
                var includeStandard = true;

                if (serviceCodeRequestModel.UrgencyLevel == UrgencyLevel.All || serviceCodeRequestModel.UrgencyLevel == UrgencyLevel.Standard || serviceCodeRequestModel.UrgencyLevel == UrgencyLevel.Cheapest)
                {
                    includeStandard = new XCabDeliverySuburbRepository().CheckStandardAllowedForZone((int)serviceCodeRequestModel.State, serviceCodeRequestModel.FromSuburb.ToUpper().Trim(), serviceCodeRequestModel.FromPostCode, serviceCodeRequestModel.ToSuburb.ToUpper(), serviceCodeRequestModel.ToPostCode);
                }

                dbArgs.Add("State", serviceCodeRequestModel.State);
                if (serviceCodeRequestModel.ServiceType > 0)
                    dbArgs.Add("ServiceType", serviceCodeRequestModel.ServiceType.ToString());
                if (serviceCodeRequestModel.UrgencyLevel > 0)
                    dbArgs.Add("ServiceSubType", serviceCodeRequestModel.UrgencyLevel.ToString());
                dbArgs.Add("NumPallets", serviceCodeRequestModel.NumberOfPallets);
                dbArgs.Add("TotalWeight", serviceCodeRequestModel.TotalWeightInKg);
                if (serviceCodeRequestModel.EnclosedVehicle == true)
                    dbArgs.Add("ItemEnclosed", serviceCodeRequestModel.EnclosedVehicle);
                if (serviceCodeRequestModel.WithCrane == true)
                    dbArgs.Add("WithCrane", serviceCodeRequestModel.WithCrane);
                if (serviceCodeRequestModel.WithTailgate == true)
                    dbArgs.Add("WithTailgate", serviceCodeRequestModel.WithTailgate);

                string sqlServiceCode = @"Select c2.Code ServiceCode, c2.Description ServiceCodeDesc ,c2.PickupEta  PickupEta
                                FROM [Quote].[VehicleServiceCodesMapping] a2
                                INNER JOIN [Quote].[Vehicles] b2 ON a2.VehicleID = b2.id
                                INNER JOIN [Quote].[ServiceCode] c2 ON a2.ServiceCodeID = c2.id 
								INNER JOIN [Quote].[ServiceType] d2 ON c2.ServiceType = d2.ServiceTypeCode
								INNER JOIN [Quote].[ServiceSubType] e2 ON c2.ServiceSubType = e2.ServiceSubTypeCode
                                WHERE a2.Active = 1 
                                AND a2.State = @State";

                if (!includeStandard)
                    sqlServiceCode += " AND e2.ServiceSubTypeCode <> 'STN' ";

                if (serviceCodeRequestModel.ServiceType > 0)
                    sqlServiceCode += " AND d2.APIMapType = @ServiceType ";

                if (serviceCodeRequestModel.UrgencyLevel > 0)
                    sqlServiceCode += " AND e2.APIMapType = @ServiceSubType ";

                if (serviceCodeRequestModel.EnclosedVehicle == true)
                    sqlServiceCode += " AND c2.ItemsEnclosed = @ItemEnclosed ";

                if (serviceCodeRequestModel.WithCrane == true)
                    sqlServiceCode += " AND c2.WithCrane = @WithCrane ";

                if (serviceCodeRequestModel.WithTailgate == true)
                    sqlServiceCode += " AND c2.WithTailgate = @WithTailgate ";

                sqlServiceCode += @" AND c2.FacilitatePallets = 1
                                AND b2.id in(Select top 3 b1.id							
                                FROM [Quote].[VehicleServiceCodesMapping] a1
                                INNER JOIN [Quote].[Vehicles] b1 ON a1.VehicleID = b1.id
                                INNER JOIN [Quote].[ServiceCode] c1 ON a1.ServiceCodeID = c1.id 
								INNER JOIN [Quote].[ServiceType] d1 ON c1.ServiceType = d1.ServiceTypeCode
								INNER JOIN [Quote].[ServiceSubType] e1 ON c1.ServiceSubType = e1.ServiceSubTypeCode
                                WHERE a1.Active = 1 
                                AND a1.State = @State";

                if (!includeStandard)
                    sqlServiceCode += " AND e1.ServiceSubTypeCode <> 'STN' ";

                if (serviceCodeRequestModel.ServiceType > 0)
                    sqlServiceCode += " AND d1.APIMapType = @ServiceType ";

                if (serviceCodeRequestModel.UrgencyLevel > 0)
                    sqlServiceCode += " AND e1.APIMapType = @ServiceSubType ";

                if (serviceCodeRequestModel.EnclosedVehicle == true)
                    sqlServiceCode += " AND c1.ItemsEnclosed = @ItemEnclosed ";

                if (serviceCodeRequestModel.WithCrane == true)
                    sqlServiceCode += " AND c1.WithCrane = @WithCrane ";

                if (serviceCodeRequestModel.WithTailgate == true)
                    sqlServiceCode += " AND c1.WithTailgate = @WithTailgate ";

                sqlServiceCode += @" AND b1.MaxPallets >= @NumPallets
                                AND c1.FacilitatePallets = 1
                                AND b1.TotalMaxWeight >= @TotalWeight                               
								GROUP BY b1.id,b1.MaxPallets,b1.Description						
                                ORDER by b1.MaxPallets) ORDER BY c2.id";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    ServiceCodes = connection.Query<ServiceCodeFilter>(sqlServiceCode, dbArgs).ToList();
                }

                return ServiceCodes;
            }
            catch (Exception e)
            {
                Logger.LogSlackErrorFromApp("RESTful Web Service: ServiceController",
                            "Error while retrieving details from database for Quote based on Service codes. Message:" + e.Message + ", Service request:" + serviceCodeRequestModel,
                            "RESTful Web Service:ServiceController", SlackChannel.WebServiceErrors);
                return null;
            }
        }

        /// <summary>
        /// Get a list of Service codes based on a criteria and given cubic
        /// </summary>
        /// <param name="serviceCodeRequestModel"></param>
        /// <returns></returns>
        public ICollection<ServiceCodeFilter> GetServiceCodesNonPallets(ShipmentModel.ServiceQuoteRequest serviceCodeRequestModel)
        {
            try
            {
                //Retrieve data afor the give criteria
                ICollection<ServiceCodeFilter> ServiceCodes = null;
                var dbArgs = new DynamicParameters();
                var includeStandard = true;

                if (serviceCodeRequestModel.UrgencyLevel == UrgencyLevel.All || serviceCodeRequestModel.UrgencyLevel == UrgencyLevel.Standard || serviceCodeRequestModel.UrgencyLevel == UrgencyLevel.Cheapest)
                {
                    includeStandard = new XCabDeliverySuburbRepository().CheckStandardAllowedForZone((int)serviceCodeRequestModel.State, serviceCodeRequestModel.FromSuburb.ToUpper().Trim(), serviceCodeRequestModel.FromPostCode, serviceCodeRequestModel.ToSuburb.ToUpper(), serviceCodeRequestModel.ToPostCode);
                }

                dbArgs.Add("State", serviceCodeRequestModel.State);
                if (serviceCodeRequestModel.ServiceType > 0)
                    dbArgs.Add("ServiceType", serviceCodeRequestModel.ServiceType.ToString());
                if (serviceCodeRequestModel.UrgencyLevel > 0)
                    dbArgs.Add("ServiceSubType", serviceCodeRequestModel.UrgencyLevel.ToString());
                if (serviceCodeRequestModel.TotalCubic > 0)
                    dbArgs.Add("TotalCubic", serviceCodeRequestModel.TotalCubic);
                dbArgs.Add("TotalWeight", serviceCodeRequestModel.TotalWeightInKg);
                dbArgs.Add("MaxLength", serviceCodeRequestModel.MaxLengthInCm);
                dbArgs.Add("MaxWidth", serviceCodeRequestModel.MaxWidthInCm);
                dbArgs.Add("MaxHeight", serviceCodeRequestModel.MaxHeightInCm);
                dbArgs.Add("MaxWeight", serviceCodeRequestModel.MaxWeightInKg);
                if (serviceCodeRequestModel.EnclosedVehicle == true)
                    dbArgs.Add("ItemEnclosed", serviceCodeRequestModel.EnclosedVehicle);
                if (serviceCodeRequestModel.WithCrane == true)
                    dbArgs.Add("WithCrane", serviceCodeRequestModel.WithCrane);
                if (serviceCodeRequestModel.WithTailgate == true)
                    dbArgs.Add("WithTailgate", serviceCodeRequestModel.WithTailgate);

                string sqlServiceCode = @"Select c4.Code ServiceCode, c4.Description ServiceCodeDesc ,c4.PickupEta  PickupEta
                                FROM [Quote].[VehicleServiceCodesMapping] a4
                                INNER JOIN [Quote].[Vehicles] b4 ON a4.VehicleID = b4.id
                                INNER JOIN [Quote].[ServiceCode] c4 ON a4.ServiceCodeID = c4.id 
								INNER JOIN [Quote].[ServiceType] d4 ON c4.ServiceType = d4.ServiceTypeCode
								INNER JOIN [Quote].[ServiceSubType] e4 ON c4.ServiceSubType = e4.ServiceSubTypeCode
                                WHERE a4.Active = 1 
                                AND a4.State = @State ";

                if (!includeStandard)
                    sqlServiceCode += " AND  e4.ServiceSubTypeCode <> 'STN' ";

                if (serviceCodeRequestModel.ServiceType > 0)
                    sqlServiceCode += " AND d4.APIMapType = @ServiceType ";

                if (serviceCodeRequestModel.UrgencyLevel > 0)
                    sqlServiceCode += " AND e4.APIMapType = @ServiceSubType ";

                if (serviceCodeRequestModel.EnclosedVehicle == true)
                    sqlServiceCode += " AND c4.ItemsEnclosed = @ItemEnclosed ";

                if (serviceCodeRequestModel.WithCrane == true)
                    sqlServiceCode += " AND c4.WithCrane = @WithCrane ";

                if (serviceCodeRequestModel.WithTailgate == true)
                    sqlServiceCode += " AND c4.WithTailgate = @WithTailgate ";
                /*Values 436 and 435 are assigned for null values of Max Length, Width and Height So that cubic of that value would be equal to the 
                  cubic of the container of a SEMI truck which is the largest vehicle
                  Values 1320, 240 and 260 are assigned for null values of Item Max Length, Width and Height which is the dimentions of the container of a SEMI truck 
                  which is the largest vehicle
                 */
                sqlServiceCode += @" AND b4.id in(Select top 3 f3.id FROM (Select b1.id
                                FROM [Quote].[VehicleServiceCodesMapping] a1
                                INNER JOIN [Quote].[Vehicles] b1 ON a1.VehicleID = b1.id
                                INNER JOIN [Quote].[ServiceCode] c1 ON a1.ServiceCodeID = c1.id 
								INNER JOIN [Quote].[ServiceType] d1 ON c1.ServiceType = d1.ServiceTypeCode
								INNER JOIN [Quote].[ServiceSubType] e1 ON c1.ServiceSubType = e1.ServiceSubTypeCode
                                WHERE a1.Active = 1 
                                AND a1.State = @State
                                AND (ISNULL(b1.TotalMaxLength,436)*ISNULL(b1.TotalMaxWidth,436)*ISNULL(b1.TotalMaxHeight,435) >= @TotalCubic)
                                AND (ISNULL(b1.TotalMaxLength,1320) >= @MaxLength  AND ISNULL(b1.TotalMaxWidth,240) >= @MaxWidth AND ISNULL(b1.TotalMaxHeight,260) >= @MaxHeight)
                                AND (ISNULL(b1.ItemMaxLength, 1320) > = @MaxLength AND ISNULL(b1.ItemMaxWidth, 240) > @MaxWidth AND ISNULL(b1.ItemMaxHeight, 260) >= @MaxHeight AND ISNULL(b1.ItemMaxWeight,22000 ) >= @MaxWeight ) 
                                AND b1.TotalMaxWeight >= @TotalWeight ";

                if (!includeStandard)
                    sqlServiceCode += " AND  e1.ServiceSubTypeCode <> 'STN' ";

                if (serviceCodeRequestModel.ServiceType > 0)
                    sqlServiceCode += " AND d1.APIMapType = @ServiceType ";

                if (serviceCodeRequestModel.UrgencyLevel > 0)
                    sqlServiceCode += " AND e1.APIMapType = @ServiceSubType ";

                if (serviceCodeRequestModel.EnclosedVehicle == true)
                    sqlServiceCode += " AND c1.ItemsEnclosed = @ItemEnclosed ";

                if (serviceCodeRequestModel.WithCrane == true)
                    sqlServiceCode += " AND c1.WithCrane = @WithCrane ";

                if (serviceCodeRequestModel.WithTailgate == true)
                    sqlServiceCode += " AND c1.WithTailgate = @WithTailgate ";

                sqlServiceCode += @" GROUP BY b1.id,b1.TotalMaxWeight,b1.Description";

                if (serviceCodeRequestModel.AllowOnRacks == true)
                {
                    sqlServiceCode += @" UNION
                                Select b2.id
                                FROM [Quote].[VehicleServiceCodesMapping] a2
                                INNER JOIN [Quote].[Vehicles] b2 ON a2.VehicleID = b2.id
                                INNER JOIN [Quote].[ServiceCode] c2 ON a2.ServiceCodeID = c2.id 
								INNER JOIN [Quote].[ServiceType] d2 ON c2.ServiceType = d2.ServiceTypeCode
								INNER JOIN [Quote].[ServiceSubType] e2 ON c2.ServiceSubType = e2.ServiceSubTypeCode
                                WHERE a2.Active = 1 
                                AND a2.State = @State";

                    if (!includeStandard)
                        sqlServiceCode += " AND  e2.ServiceSubTypeCode <> 'STN' ";

                    if (serviceCodeRequestModel.ServiceType > 0)
                        sqlServiceCode += " AND d2.APIMapType = @ServiceType ";

                    if (serviceCodeRequestModel.UrgencyLevel > 0)
                        sqlServiceCode += " AND e2.APIMapType = @ServiceSubType ";

                    if (serviceCodeRequestModel.EnclosedVehicle == true)
                        sqlServiceCode += " AND c2.ItemsEnclosed = @ItemEnclosed ";

                    if (serviceCodeRequestModel.WithCrane == true)
                        sqlServiceCode += " AND c2.WithCrane = @WithCrane ";

                    if (serviceCodeRequestModel.WithTailgate == true)
                        sqlServiceCode += " AND c2.WithTailgate = @WithTailgate ";

                    sqlServiceCode += @" AND b2.TotalMaxWeight >= @TotalWeight 	
                                     AND ((b2.RacksMaxLength*b2.RacksMaxWidth*b2.RacksMaxHeight) >= @TotalCubic)
                                     AND (b2.RacksMaxLength >= @MaxLength AND b2.RacksMinLength <= @MaxLength AND b2.RacksMaxWidth >= @MaxWidth AND b2.RacksMaxHeight >= @MaxHeight AND ISNULL(b2.RacksMaxWeight,b2.TotalMaxWeight) >= @MaxWeight) ";

                    sqlServiceCode += @" GROUP BY b2.id,b2.TotalMaxWeight,b2.Description";
                }

                sqlServiceCode += @") f3 ORDER BY f3.id ) ORDER BY e4.ServiceSubTypeCode,c4.id";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    ServiceCodes = connection.Query<ServiceCodeFilter>(sqlServiceCode, dbArgs).ToList();
                }

                return ServiceCodes;
            }
            catch (Exception e)
            {
                Logger.LogSlackErrorFromApp("RESTful Web Service: ServiceController",
                            "Error while retrieving details from database for Quote based on Service codes. Message:" + e.Message + ", Service request:" + serviceCodeRequestModel,
                            "RESTful Web Service:ServiceController", SlackChannel.WebServiceErrors);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <returns></returns>
        public string GetUrgencyTypeOfServiceCode(string serviceCode)
        {
            var urgencyType = string.Empty;

            try
            {
                string sqlServiceCode = @"SELECT TOP 1 ServiceSubType FROM [Quote].[ServiceCode] WHERE code = '" + serviceCode + "'";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    urgencyType = connection.ExecuteScalar<string>(sqlServiceCode);
                }

                return urgencyType;
            }
            catch (Exception e)
            {
                Logger.LogSlackErrorFromApp("RESTful Web Service: ServiceCodesRepository",
                            "Error while retrieving urgency type of the service code. Message:" + e.Message + ", Service request:",
                            "RESTful Web Service:ServiceCodesRepository", SlackChannel.WebServiceErrors);
                return urgencyType;
            }
        }

    }
}
