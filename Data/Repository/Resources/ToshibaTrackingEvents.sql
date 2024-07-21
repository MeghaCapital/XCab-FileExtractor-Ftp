SELECT TOP 200
									J.Ref1 Reference1,
									j.Ref1 Reference2,
									j.FROM_ADD_1 FromAddressLine1,
									j.FROM_ADD_2 FromAddressLine2,
									j.FROM_SUB FromSuburb,
									j.FROM_PC FromPostcode,
									j.TO_ADD_1 ToAddressLine1,
									j.TO_ADD_2 ToAddressLine2,
									j.TO_SUB ToSuburb,
									j.TO_PC ToPostcode,
									j.JobConsignment Consignment,
									j.JOB_NUMBER JobNumber,
									CAST(j.ALLOCATED AS DATE) JobDate,
									j.PICKUP_COMPLETE PickupComplete,
									j.DELIVERY_COMPLETE DeliveryComplete,
									j.CLIENT_CODE AccountCode,
									S.stAbbrev CapitalState, --Needed for control table
									s.stID StateId,
									j.JOB_DATE CreatedOn,
									j.DEL_POD_NAME PODSigName
								INTO #res
								FROM
									[DWH12].[TPlus].[dbo].[jobs] J
									INNER JOIN [Tracking].[trk].[RegisteredClients] RC ON RC.ClientCode = j.CLIENT_CODE AND RC.StateId = j.State
									LEFT OUTER JOIN [Tracking].[trk].[TriggeredJobs] T ON T.JobNumber = J.JOB_NUMBER AND t.JobDate = j.JOB_DATE AND T.StateId = j.state
									JOIN[DWH12].[TPlus].[dbo].[ts_states] S ON S.stID = j.State
								WHERE
									j.JOB_DATE >= CAST(dateadd(day, -1, getdate()) AS DATE)
									 AND T.JobNumber IS NULL
									AND j.SERVICE_CODE IN('CPOD')

								 INSERT INTO [Tracking].[trk].[TriggeredJobs] SELECT StateId, JobNumber, CreatedOn, getdate() FROM #res

								SELECT * FROM #res