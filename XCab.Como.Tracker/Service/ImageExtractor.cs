using Data.Model.Poc.V2;
using Data.Model.PodStreamer.V2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common;
using xcab.como.common.Service;
using xcab.como.common.Struct;
using xcab.como.tracker;
using xcab.como.tracker.Client;
using xcab.como.tracker.Data.Models;
using xcab.como.tracker.Data.Response;
using xcab.como.tracker.Service;
using XCab.Como.Tracker.Service.Utils;

namespace xcab.como.tracker.Service
{
	public class ImageExtractor : ComoDefaultProcessor, IImageExtractor
	{
		private static readonly string base64NotFound;
		private static readonly IImageClient imageClient;

		static ImageExtractor()
		{
			ImageExtractor.base64NotFound = "iVBORw0KGgoAAAANSUhEUgAAAZAAAACGCAIAAAB8JUupAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAABY/SURBVHhe7Z17cBZXGcb7D+KgFm/VGbR1LKKoU/jDOrbVjh219A/rDKWltDjoKNqaCTrVaeuNagGpFBVaYsvNjq2VUqcFCqVJIAmXhJSkJOSeEJKQBHKD5MsFcr+Az57Nd+Y9u9+3325o6sZ5fvMO8z27Z8/ue3Le5zv73bjmyh13MBgMxhSIK1euuXINg8FgTIWgYTEYjCkTNCwGgzFlwmlYhBASKkyDEkJpQggJEaZBCaE0IYSECNOghFCaEEJChGlQQihNCCEhwjQoIZQmhJAQYRqUEEoTQkiIMA1KCKUJISREmAYlhNKEEBIiTIMSQmlCCAkRpkEJoTQhhIQI06CEUJoQQkKEaVBCKE0IISHCNCghlCaEkBBhGpQQShNCSIgwDUoIpQkhJESYBiWE0oQQEiJMgxJCaUIICRGmQQmhNCGEhAjToIRQmhBCQoRpUEIoTQghIcI0KCGUJoSQEGEalBBKE0JIiDANSgilCSEkRJgGJYTShBASIkyDEkJpQggJEaZBCaE0IYSECNOghFCaEEJChGlQQihNCCEhwjQoIZQmhJAQYRqUEEoTQkiIMA1KCKUJISREmAYlhNKEEBIiTIMSQmlCCAkRpkEJoTQhhIQI06CEUJoQQkKEaVBCKE0IeQ+4LBjfRGJiGpQQShNCJpux3NzB9PS+t97qra0dGhoaGxsb30HcmAYlhNLEPxN7epzYUaHCTiEh462JyWhtra64S9/4RktLy8DAAIcrLtqdEDSsiTHW3DySkTF04MBgQwOeIUdHR/1MuLFIZPyotrbh4eEp97yKHFFs4ymkpyeIhoapmON7wFB1ta64jptuKiws7Orq4kDFRbsTgoY1AcZeeEEO2tCXvhQpLOzv7/eec9aSY+ZM+5DL06e3trZOuefV0T/9SWedMEavuw5rBwzL1MpxssFoDFRV6VFq++IXs7OzL1y4QMOKi5hUlnJo4g0m1ti8ecagXXNN75w5DQ0NfX19HsVpzUhxyPHjx9vb2yd7muJ6cAosAPHvVRoHehj7/OdlCgkjLy/vPchxaoG/Qn9lpR4iGNaRI0fOnz/PUYqLmFGWcmjiDSbW6PXXG4Omou7nP8eCAreH4+1cwDVk+7S0tObmZmvjpDGWlXX5M5/BuXDBXWlp8NOrqYp4iceLoRkzMjIyMCaTmuOUg4YVGDGpLOXQxBuUX8y6Hfjwh0vy87u7u+PNPIdhvfXWW+fOnZu8YkZhjPz+9/p0tStX4nSDg4MTXmc5Ej/88MNZTzyRvXp1zpo1MSPrhRe4wnJDwwpMdKysoGEFxVG3fddeqx9XJyWdPXs23iLL8qZoS8R7YFiDv/mNPt3JX/yioqKit7f33TKsN1NScnJyysvLq6qqTsWipqaGr2G5oWEFJjpWVtCwguKo2/wHH9SPscgqy8iI946P5U3RlojJNixcw8Cvf61PdyI5ubS09NKlS++WYR3YuhVWhRUl7jTdwKcGBgb4LqEbGlZgomNlBQ0rKI663fvssz033qhl46JF9fX1Md/+s7wp2gwRyLDQGya0xo/poJnDsEpKSi5evGgf7qcHB47EM7Zvr6urs+8x4zF+ZCLQUqVlEegom3HtiXfLeHuxMehVOdA9ALsT/EvDCkZ0rKygYQXFUbdpmzdX/fnPWo5Om1aUlhaJRNxOZG2JNkP4MSxM7tGmppGUlKHf/hb3dzCg3tdfv3TiBO7sPD78ZX9GbDA9vX/JEn26qoULT2/b1rVnz/iHpI4cgdfE6yEmaOw2LI83GRIis/OZmo11YEGB9VmwvDw7i/EdsRirq7M/OIZnkZGREXe3Y4WFwwcPDp46hfNq18ADHDX8u99h2Pt27OgrLMSJcLhPWxlP7S9/sf9wVie7dmHVafdAwwpGdKysoGEFxVG3B7dtw8ql/wtf0FvqFy5EJbsXWVZdRdsgvA3LmvGZmaO33y4PkdH/zW9GcnNx8+XoYayv78r73+9oHDO6k5I6Oztx1zZ+ZCIciV+NYXlnFy81zdhzz+nG7SkpuC2FEYzvM4F365a9d9554cIFx9sOY7t26QYtJ0/ilhldjaxZM/aJT+jtdlyePr1r82YsUbHf8Zd1MNrRMfq97zkORwx/6lORHTvQQ295ud5Iw0qMGENLOTTxxl231dXV57du1VuwyCr5z3/a29sdVWSVX7QNwsOwLvf3j91/v2wcM8be977zTz/d1dUF09ElJL/24R3NCxZUVVX5f1XLnfjEDMtPdjFTs4HEwke3rHz8cVxGvJf2h0+f1i0j8+YVFRXJVxitrrZt0w1OvPZa6/HjIzffrLe448Ly5XA9ZB1v0DD+3p9W61yyBGfRkoaVGDF6lnJo4o27bmtrazs6OvrEp0nbbr319OnTWCPIaW15U7QBIp5hjUUi7g+m9s+c2Tp3Lia3Yzui51vfwnTXJQSXHL3uOkebmHHyoYdOnjzp8TkMB+7EJ2BY8bLzk5oNrla++1mwYkVZWVlM28UW78+Uo8Hgli26Qe7atUOzZmk5NGMGxhxPP3qLHfV//COuKubKFHeX+ssMOiI33HDp4x+XW3rnzNGPaViJEUNnKYcm3sSs297e3vaMDL0RUfzii5iFcpFleZNoENOwUEIjK1bIZqXf/e4bW7bs3bv3wIEDhw4dysrKenvDhu7Pfla2OZecrBd06BCP33nnnfT09FMPPKDb5Cxf/uqrr6Kf/fv349Spqamok5qaGv8fdIiZeCDD8sjOT2o2KGyf735ii/erRWggDQsOZT/omTXr6C9/uWvXrrS0tIMHD+atWzf8gQ/oZrhCWGRPT4/DYtCbvBOE0x3/4Q/f3L4do52RkZH9r3+d/fa39V4dNKzEyBGjYQUlZt0ODAyg8i+KGdl6yy24VZSLLByn9yJiGtbIuXOXp0/XbY795Cco5vz8fDhLU1NTW1tba2trY2NjeUFBx1e/qpvhBqr66FEULSa9VYSDg1jxoX2HcIe8n/0sJycH6z6cFNi92a/IjJ87Ee7EsbRE4jhpTHTiGo/sEqame0PPk2RYdjTPm/fGiy9mZmaiW1xPS0tLQ0PDqVdfxZXoNiVPP93c3OxYZI0WFOgGiMxHH33zzTcLCwvr6+vRCQYc86F61SrZD4KGlRgxXJZyaOKNu25hWPZbSJGsLL0dUZaSgjrU09ryJrHXbVion+Hly3WDigUL9u3bV1xcjLsY+x0u69Sjo+gQRtNw6tTQRz+qG9euWIGqsM+FfjD78bj/8cd1AxQ2urJfn7b7AYGKxDpAJF5z993nk5L6HnsM9uEO3LX1NjXhGrSPeGenLidxamBSDavpppv27NyZm5sLl8EaCutHDBf+7erqks9GWLpWVVXJZyM8kMsrrBzhxSUlJXjmwOFIDf2gPeZD3XPPSc+iYSUmOlZW0LCCYhVWrDsjTDjMyG7xOVLcO5SXl9sLH/tAvQvhNizH6+WpGzZg9YF6lmVvM36uH/1IN2752tccH2RHG5+F7RNH4gmj+W9/Q7lqo/GZXcLU/OeFLYEMq+P661//97/hVlgN9Zu/vQG76dq5U7esvfNOLJ3ky3+jTU16L2Lfpk15eXk4l04f4HSQkUjk3Jo1uiUNKzFiYC3l0MQbR91qw8J0xLSOVFTI58+yNWv0vYPlTdHtCLdhDe/bp/fiqT41NdW+2XSXIsCB3UeO6PY9s2YdP34cxaDnPR78bw2r/NFHca+n37/zn513av7zwpZAhlVyzz0ZGRkxP/cL2Zebq1ueu/lmx2gPZ2frva1z5+JmUOYusTyrsFA3pmElJjpWVtCwguKoW21Y2IXZibkuVwfWIqugALc5mI44Tm9HOAzLKp516/Te0kWLjh07hgWIbuDAKqG+Pt1+dNq0Q4cO4Y5Dt/df2D5Bz/4Na2jGjJyUlFOnTuEicUbgPzs09kjNf17YEsiwyu69FzaEVWFM7xhsa9Mt0VVOTo7+Xjf6GXrpJb238q67Dh8+3Nraiicw+1iJlV1FhW5Mw0pMdKysoGEFxVG30rDsvV1VVaMf+pBuUJOcDGNCA2v6RjciHIaF+TqYlKT35iclOW463OC5euTTn9aHHH3pJdzL6CLBgZNqWLs3bMA6AlaCenOTnZ2N68fq0v6gZtDsPFLznxe2BDKs8vvuO3HiRLyvgiIR3RJdHT16FIZrt0Q/Q6tW6b228cn1lyThVREn0bGygoYVFEfdOgwL0xGLrI6VK3UD/bMzVpvoRoTbsIbFy7pHV62K9/EiDY4duu02fcjbTz2F2xl9JehwUg0r9fnni4qKYEkoNjco5s7OTgyFXYdBs/NIzX9e2BLUsAoKCuIZlvzzObrCv0M//rHe623HCa+KOImOlRU0rKA46tZhWAD7u9vahj/2Md2mOimpsbFR3uYgYhjW17+u92avXl1ZWWnfT9kN3DiqOnv9+traWmshoECHk2pYSByn6+/vx2acKyb6XHgcKDuP1NBVGA1r6VK9t/CnPy0uLr548WLM7BJeFXESHSsraFhBcdetw7AAZOcf/qDb2Iss3CPoLYgYt4T33KP3vvOrX/kxrJHPfU4fkrZzJ67kvTQsd+LxCJqdR2qOvOAO9q9QuLtKaA1o8G4Z1sAjj+i9FYsXx7skkPCqiJPoWFlBwwqKz7rFlo5//EM3g2cVireZEG7D6hPf6a2/447Er2GJL8pdmD3b8XvEVhX5K2yf+Ew8JkGz80gN19+3Y4fe69FVQmtAg3fLsOQldcyZk5+fjzvimP3QsAITHSsraFhB8Vm3mH9wh4G5c3XLqocf1o8RDsOy5nFqqt47Om1aXnq697uEw8nJun3t7bc7viiHBwPid2/wtF9UVNTT04MD7QZB8Zl4TAJl552ao6uW+fPjuQNaDj7zjG45eYZlXdKhQ3ovInfXLvmOrSThVREnYmAt5dDEG/91Ozw83PH667olFln6MSKGYXV2yu8tl69YUV9fj/sg7LLbSOTXXFD/+7Ztc3yTGQ8cKxFUo/eSzRv/ibsJlJ13alZXYoWCBsf37EHB65G0QbOR117TzRCTalgDAwOD4vvbZcnJZ86ccWfn56qIEzFclnJo4o3/usXsvHTpkvxKhwyHYQEYXNfGjboBSvH0pk3t7e3Y7pj31i8uiS/cVSxYkJaWhgqRn3jEg/6339ZtYJceHzLyg//EY+IzOz+pDXZ3S++rX7jQnfvIyy/L7y0iJs+wwMjISI/4ZAOyq3zllUgkIj+KZV1VSkrCqyJOxHBZyqGJN4HqFvO1IzNTN5bhNixMWdyyyd8CRDQ/9ljnyZN4rrbOq76SNrxv39gNN+gG/TNn7vnnP0+cOIHykL1Z1djWJsujbtmyxsZGFLbV0bFjQ/n5MAv/dWIddRWGlTA7/6mhZfeTT+pmiNYHHjhfXd2vftITCzT5mxDwDvvBpBoWHnS3tckvOfR/8pNNf/+79X7x8LA1dLW1o4sX670eV0WcRAfNChpWUKzJ57tuURJ9fX09ixbp9jrchgUwuXEXOfLBDzoaj1177dBtt42K31GyY2jGjMyVK7OysuBE7hsQ9Na7bJlsj54HbrnF/jnNS/fdhzqJeVMWk0CJx8Qju0Cp2e4gPzgyfsiNN8r3FhGlixbBEezHk2pYVleDgx1//atuo2Pw1luHv/xluaXs3ns9roo4EUNnKYcm3gStWzzlRyoq3NUFw2pqanIYFmZtb29vQ3p6d6wftHPEhdmz92zenJGRUV1djXtP94zHlu4jRxy/Z6Kj5a67Yv6uUzyu3rD8Z+edmu0ObW+8IX/UwRFYwhQuW7Z3797O+fPtLZNqWACPcezZ1at1M3fgqk7+4Af79+/3uCriRI4hDSsoKFv5NJ7x8svy8+VuUBW4C2vZvVtW19mvfAXV2Nra6jAsgC0wkZqamjMPPSR/N04GVh9ld9+9e/fuY8eOwTXQ3t0PwKlxl3T24MGYBpH7xBNX9YujiRKPScLsfKaGa4aR1eXmXpw929EDAi6QumFDenp6UVFRb/QVsZb5892/ODogfoOh7Pvf9xgQh2E5urLBErK9vf3M888PfuQjurGO5nnz0jZuzMzMtH7D4zvfsTe6r4o4kcNIwwoKJlZXUdGpLVvy1q078swzqKt4X3PV4JCLFy/WFhSUPPvs2089lb1+fVpamuP3xSUoUdxINjc3o03xunWnly6tWrKk8v77KxYvxioga+1aLBxwr1RcXNyi/qdSj7mOrnDquoqK0k2bih95xO7kRHLyga1bDx8+DEfw/myqxE68eutW/4nHJGZ2E0+tri4vPT3vySftHkqXLs1ISUlNTc3NzYUtRiKRjoIC+491eOdOdCvHfPy55JVXijZuzF279lBqKhZ08id6JMi0PSenLCXFaolDYv35cCA8CyetrKwsXb+++sEH7QHHVaVv2YI/el5eHlwenohFd8327e+sX4+r8pgJxMI0KCGUJt7Ys7ytra22thYlgcJD+SWcbaguVAKOOnPmzOnTpzFr7Z92i2cW2I69mNkwhYaGBhRSSUlJoQJVhx5wXuz16EFjGwT6wdWWlpZiEYEKQUWdO3cOBY+94+0SgRPhRgw3L/AI/4nHxJ3dxFLD2XENuCRcD45FD8iuoqICw4uVDnbBZfDHsq8ZI4+Njm6RPhZxjY2N6AFXAuOIZ8H2uXBtaIne0Ge8l//QA1Z/esDlVeGPbr/pgWOxqkI/3l0RC9OghFCaJARzy64EgGdUn0VrH4WCwVGYoJi1CecoekYznAJrDZgLyhjggf12mH+zsE+No1BIqE+A2sOV+O/BZmKJx0Nmd5Wp4Xrs8UFqeGJAanp45TXjgb1RgpY+/yi4KrtlvK40aGmfFAMe86rQAInbXV39SP6fYxqUEEoTn2DmeUxZDyZwlH0uTGtgPx7fEQT7QN3J+Nbg2P2Mi3cD9KavamI92wfqTsa3CuwG4yIW3nslCbvS2C2v5qqIhWlQQihNCCEhwjQoIZQmhJAQYRqUEEoTQkiIMA1KCKUJISREmAYlhNKEEBIiTIMSQmlCCAkRpkEJoTQhhIQI06CEUJoQQkKEaVBCKE0IISHCNCghlCaEkBBhGpQQShNCSIgwDUoIpQkhJESYBiWE0oQQEiJMgxJCaUIICRGmQQmhNCGEhAjToIRQmhBCQoRpUEIoTQghIcI0KCGUJoSQEGEalBBKE0JIiDANSgilCSEkRJgGJYTShBASIkyDEkJpQggJEaZBCaE0IYSECNOghFCaEEJChGlQQihNCCEhwjQoIZQmhJAQYRqUEEoTQkiIMA1KCKUJISREmAYlhNKEEBIiTIMSQmlCCAkRpkEJoTQhhIQI06CEUJoQQkKEaVBCKE0IISHCNCghlCaEkBBhGpQQShNCSIgwDUoIpQkhJESYBiWE0oQQEiJMgxJCaUIICRGmQQmhNCGEhAjToIRQmhBCQoRpUEIoTQghIcI0KCGUJoSQEGEalBBKE0JIiDANSgilCSEkRJgGJYTShBASIkyDEkJpQggJEaZBCaE0IYSECNOghFCaEEJChGlQQihNCCEhwjQoIZQmhJAQYRqUEEoTQkiIMA1KCKUJISREmAYlBIPBYIQ5aFgMBmPKBA2LwWBMmaBhMRiMKROWYRFCyJTgypX/AqOs2tmUNUaCAAAAAElFTkSuQmCC";
			ImageExtractor.imageClient = new ImageClient();
		}

		public ImageExtractor() : base()
		{

		}

		public async Task<IEnumerable<PocImageResponse>> GetPoc(int comoJobId, ELegType legType)
		{
			List<PocImageResponse> pocImageResponses = new List<PocImageResponse>();
			var pocImageResponse = new PocImageResponse();
			List<int> pocImageIds = new List<int>();
			var eventName = "Arrive";
			var operation = "ExtractPocImages";
			var query = "";

			try
			{
				if (comoJobId > 0)
				{
					if (legType == ELegType.PICKUP)
					{
						query = @"query " + operation + @" {
                                          jobs(position: 0, limit: 10, filters: {id: {_eq: " + comoJobId + @"}}) {
                                            subJobs {
                                            trackingEvents(filters: {trackingEventType: {name: {_eq: " + "\"" + eventName + "\"" + @"}}}) {
                                            id
                                            pocDocuments {
                                                           right {
                                                                id
													            documentType {
																	id
																	name
																}
                                                            }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        ";

						Client.Initialise(base.ApiToken);

						Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);

						AttestationRecordResponse pocExtractionResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
							operation,
							query
						)).Result;

						if ((pocExtractionResponse != null) && (pocExtractionResponse.jobs != null))
						{
							for (int i = 0; i < pocExtractionResponse.jobs.Count; i++)
							{
								var job = pocExtractionResponse.jobs[i];

								if ((job != null) && (job.subJobs != null))
								{
									for (int j = 0; j < job.subJobs.Count; j++)
									{
										var subJob = job.subJobs[j];

										if (subJob != null)
										{
											if ((subJob.trackingEvents != null) && (legType == ELegType.PICKUP))
											{
												for (int m = 0; m < subJob.trackingEvents.Count; m++)
												{
													var subJobTrackingEvent = subJob.trackingEvents[m];

													if (subJobTrackingEvent != null)
													{
														if ((subJobTrackingEvent.pocDocuments != null))
														{
															for (int n = 0; n < subJobTrackingEvent.pocDocuments.Count; n++)
															{
																var subJobTrackingEventPocDocument = subJobTrackingEvent.pocDocuments[n];

																if (subJobTrackingEventPocDocument != null)
																{
																	var imageId = subJobTrackingEventPocDocument.right.id;
																	pocImageIds.Add(imageId);
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}

						if (pocImageIds != null)
						{
							for (int i = 0; i < pocImageIds.Count; i++)
							{
								ImageExtractor.imageClient.Initialise(base.ApiToken);
								ImageExtractor.imageClient.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.InternalImageEndpoint + "?ids=" + string.Join(",", pocImageIds));
								byte[] response = Task.Run(async () => await ImageExtractor.imageClient.GetZipAsync(pocImageIds)).Result;
								pocImageResponse.Image = response;
								pocImageResponse.Type = "POC";

								pocImageResponses.Add(pocImageResponse);
							}
							return pocImageResponses;
						}
					}
					else if (legType == ELegType.DELIVERY)
					{
						query = @"query " + operation + @" {
										  jobs(position: 0, limit: 0, filters: {id: {_eq: " + comoJobId + @"}}) {
											subJobs {
											  subJobLegs {
												trackingEvents(filters: {trackingEventType: {name: {_eq: " + "\"" + eventName + "\"" + @"}}}) {
												  id
												  pocDocuments {
													right {
													  id
													  documentType {
														id
														name
													  }
													}
												  }
												}
											  }
											}
										  }
										}
										";

						Client.Initialise(base.ApiToken);

						Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);

						AttestationRecordResponse pocExtractionResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
							operation,
							query
						)).Result;

						List<ImageResponse> imageResponses = null;

						if ((pocExtractionResponse != null) && (pocExtractionResponse.jobs != null))
						{
							for (int i = 0; i < pocExtractionResponse.jobs.Count; i++)
							{
								var job = pocExtractionResponse.jobs[i];

								if ((job != null) && (job.subJobs != null))
								{
									imageResponses = new List<ImageResponse>();

									for (int j = 0; j < job.subJobs.Count; j++)
									{
										var subJob = job.subJobs[j];

										if (subJob != null)
										{
											if ((legType == ELegType.DELIVERY) && (subJob.subJobLegs != null))
											{
												for (int k = 0; k < subJob.subJobLegs.Count; k++)
												{
													var subJobLeg = subJob.subJobLegs[k];

													if (subJobLeg != null)
													{
														if (subJobLeg.trackingEvents != null)
														{
															for (int p = 0; p < subJobLeg.trackingEvents.Count; p++)
															{
																var subJobLegTrackingEvent = subJobLeg.trackingEvents[p];

																if (subJobLegTrackingEvent != null)
																{
																	if (subJobLegTrackingEvent.pocDocuments != null)
																	{
																		for (int q = 0; q < subJobLegTrackingEvent.pocDocuments.Count; q++)
																		{
																			var subJobLegTrackingEventPocDocument = subJobLegTrackingEvent.pocDocuments[q];

																			if (subJobLegTrackingEventPocDocument != null)
																			{
																				var imageId = subJobLegTrackingEventPocDocument.right.id;
																				pocImageIds.Add(imageId);
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (pocImageIds != null)
						{
							for (int i = 0; i < pocImageIds.Count; i++)
							{
								ImageExtractor.imageClient.Initialise(base.ApiToken);
								ImageExtractor.imageClient.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.InternalImageEndpoint + "?ids=" + string.Join(",", pocImageIds));
								byte[] response = Task.Run(async () => await ImageExtractor.imageClient.GetZipAsync(pocImageIds)).Result;
								pocImageResponse.Image = response;
								pocImageResponse.Type = "POC";

								pocImageResponses.Add(pocImageResponse);
							}
							return pocImageResponses;
						}
					}
				}
			}
			catch (Exception)
			{

			}

			return pocImageResponses;
		}

		public async Task<IEnumerable<PodImageResponse>> GetPod(int comoJobId, ELegType legType)
		{
			List<PodImageResponse> podImageResponses = new List<PodImageResponse>();
			var podImageResponse = new PodImageResponse();
			List<int> podImageIds = new List<int>();
			var eventName = "Complete";
			var operation = "ExtractPodImages";
			var query = "";
			var podName = "";

			try
			{
				if (comoJobId > 0)
				{
					if (legType == ELegType.PICKUP)
					{
						query = @"query " + operation + @" {
                                          jobs(position: 0, limit: 10, filters: {id: {_eq: " + comoJobId + @"}}) {
                                            subJobs {
                                            trackingEvents(filters: {trackingEventType: {name: {_eq: " + "\"" + eventName + "\"" + @"}}}) {
                                            id
                                            podDocuments {
                                                            right {
                                                                id
																name
																fileName
													            documentType {
																	id
																	name
																}
                                                            }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        ";

						Client.Initialise(base.ApiToken);

						Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);

						AttestationRecordResponse podExtractionResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
							operation,
							query
						)).Result;

						if ((podExtractionResponse != null) && (podExtractionResponse.jobs != null))
						{
							for (int i = 0; i < podExtractionResponse.jobs.Count; i++)
							{
								var job = podExtractionResponse.jobs[i];

								if ((job != null) && (job.subJobs != null))
								{
									for (int j = 0; j < job.subJobs.Count; j++)
									{
										var subJob = job.subJobs[j];

										if (subJob != null)
										{
											if ((subJob.trackingEvents != null) && (legType == ELegType.PICKUP))
											{
												for (int m = 0; m < subJob.trackingEvents.Count; m++)
												{
													var subJobTrackingEvent = subJob.trackingEvents[m];

													if (subJobTrackingEvent != null)
													{
														if ((subJobTrackingEvent.podDocuments != null))
														{
															for (int n = 0; n < subJobTrackingEvent.podDocuments.Count; n++)
															{
																var subJobTrackingEventPodDocument = subJobTrackingEvent.podDocuments[n];

																if (subJobTrackingEventPodDocument != null)
																{		
																	var imageId = subJobTrackingEventPodDocument.right.id;
																	podName = subJobTrackingEventPodDocument.right.fileName;
																	podImageIds.Add(imageId);
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}

						if(podImageIds != null)
						{
							for (int i = 0; i < podImageIds.Count; i++)
							{
								ImageExtractor.imageClient.Initialise(base.ApiToken);
								ImageExtractor.imageClient.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.InternalImageEndpoint + "?ids=" + string.Join(",", podImageIds));
								byte[] response = Task.Run(async () => await ImageExtractor.imageClient.GetZipAsync(podImageIds)).Result;
								podImageResponse.Image = response;
								podImageResponse.PodName = podName;

								podImageResponses.Add(podImageResponse);
							}
							return podImageResponses;
						}
					}
					else if (legType == ELegType.DELIVERY)
					{
						query = @"query " + operation + @" {
                                          jobs(position: 0, limit: 10, filters: {id: {_eq: " + comoJobId + @"}}) {
                                            subJobs {
											subJobLegs {
											trackingEvents(filters: {trackingEventType: {name: {_eq: " + "\"" + eventName + "\"" + @"}}}) {
													    id
														podDocuments {
														right {
															id
															name
															fileName
																documentType {
																id
																name
																}
															}
														}
													}
													}
												}
												}
											}";

						Client.Initialise(base.ApiToken);

						Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);

						AttestationRecordResponse podExtractionResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
							operation,
							query
						)).Result;

						List<ImageResponse> imageResponses = null;

						if ((podExtractionResponse != null) && (podExtractionResponse.jobs != null))
						{
							for (int i = 0; i < podExtractionResponse.jobs.Count; i++)
							{
								var job = podExtractionResponse.jobs[i];

								if ((job != null) && (job.subJobs != null))
								{
									imageResponses = new List<ImageResponse>();

									for (int j = 0; j < job.subJobs.Count; j++)
									{
										var subJob = job.subJobs[j];

										if (subJob != null)
										{		
											if ((legType == ELegType.DELIVERY) && (subJob.subJobLegs != null))
											{
												for (int k = 0; k < subJob.subJobLegs.Count; k++)
												{
													var subJobLeg = subJob.subJobLegs[k];

													if (subJobLeg != null)
													{
														if (subJobLeg.trackingEvents != null)
														{
															for (int p = 0; p < subJobLeg.trackingEvents.Count; p++)
															{
																var subJobLegTrackingEvent = subJobLeg.trackingEvents[p];

																if (subJobLegTrackingEvent != null)
																{
																	if (subJobLegTrackingEvent.podDocuments != null)
																	{
																		for (int q = 0; q < subJobLegTrackingEvent.podDocuments.Count; q++)
																		{
																			var subJobLegTrackingEventPocDocument = subJobLegTrackingEvent.podDocuments[q];

																			if (subJobLegTrackingEventPocDocument != null)
																			{															
																				var imageId = subJobLegTrackingEventPocDocument.right.id;
																				podName = subJobLegTrackingEventPocDocument.right.fileName;
																				podImageIds.Add(imageId);
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (podImageIds != null)
						{
							for (int i = 0; i < podImageIds.Count; i++)
							{
								ImageExtractor.imageClient.Initialise(base.ApiToken);
								ImageExtractor.imageClient.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.InternalImageEndpoint + "?ids=" + string.Join(",", podImageIds));
								byte[] response = Task.Run(async () => await ImageExtractor.imageClient.GetZipAsync(podImageIds)).Result;
								podImageResponse.Image = response;
								podImageResponse.PodName = podName;

								podImageResponses.Add(podImageResponse);
							}
							return podImageResponses;
						}
					}
				}
			}
			catch (Exception)
			{

			}

			return podImageResponses;
		}

		public IEnumerable<byte[]> AttestationRecord(long decodedJobNumber, ELegType legType, EDocumentType docType)
		{
			string jobNumber = JobEndec.Encode(decodedJobNumber, EEncoding.BASE26);
			int indexOfFirstNumber = jobNumber.IndexOfAny("0123456789".ToCharArray());

			string prefix = jobNumber.Substring(0, indexOfFirstNumber);
			int suffix = int.Parse(jobNumber.Substring(indexOfFirstNumber));

			string operation = "AttestationRecords";

			string query = @"query " + operation + @" {
                                jobs(position:0,limit:0,filters:{jobNumber : {prefix: {_eq : " + "\"" + prefix + "\"" + @"}, suffix: {_eq : " + suffix + @"}}}) {
                                subJobs {
                                            id
                                            billingDate
                                    hasDriver {
                                                id
                                                displayName
                                    }
                                            trackingEvents {
                                                id
                                                signatureName
                                    signatureSignedOn
                                    trackingEventType {
                                                    id
                                                    name
                                    }
                                                eventDateTimeUTC
                                                pocDocuments {
                                                    right {
                                                        id
                                                        name
                                                        dateCreated
                                        dateLastUpdated
                                                        documentType {
                                                            id
                                                            name


                                        }
                                                        fileContent
                                                        contentType
                                                    }
                                                }
                                                podDocuments {
                                                    right {
                                                        id
                                                        name
                                                        dateCreated
                                        dateLastUpdated
                                                        documentType {
                                                            id
                                                            name
                                        }
                                                        fileContent
                                                        contentType
                                                    }
                                                }
                                            }
                                            subJobLegs {
                                                id
                                                trackingEvents {
                                                    id
                                                    signatureName
                                        signatureSignedOn
                                        trackingEventType {
                                                        id
                                                        name
                                        }
                                                    eventDateTimeUTC
                                                    pocDocuments {
                                                        right {
                                                            id
                                                            name
                                                            dateCreated
                                            dateLastUpdated
                                                            documentType {
                                                                id
                                                                name
                                            }
                                                            fileContent
                                                            contentType
                                                        }
                                                    }
                                                    podDocuments {
                                                        right {
                                                            id
                                                            name
                                                            dateCreated
                                            dateLastUpdated
                                                            documentType {
                                                                id
                                                                name
                                            }
                                                            fileContent
                                                            contentType
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            ";

			Client.Initialise(base.ApiToken);

			Client.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);

			AttestationRecordResponse existingJobResponse = Task.Run(async () => await Client.GetAsync<AttestationRecordResponse>(
				operation,
				query
			)).Result;

			List<ImageResponse> imageResponses = null;

			if ((existingJobResponse != null) && (existingJobResponse.jobs != null))
			{
				for (int i = 0; i < existingJobResponse.jobs.Count; i++)
				{
					var job = existingJobResponse.jobs[i];

					if ((job != null) && (job.subJobs != null))
					{
						imageResponses = new List<ImageResponse>();

						for (int j = 0; j < job.subJobs.Count; j++)
						{
							var subJob = job.subJobs[j];

							if (subJob != null)
							{
								if ((subJob.trackingEvents != null) && (legType == ELegType.PICKUP))
								{
									for (int m = 0; m < subJob.trackingEvents.Count; m++)
									{
										var subJobTrackingEvent = subJob.trackingEvents[m];

										if (subJobTrackingEvent != null)
										{
											if ((subJobTrackingEvent.pocDocuments != null) && (docType == EDocumentType.POD))
											{
												for (int n = 0; n < subJobTrackingEvent.pocDocuments.Count; n++)
												{
													var subJobTrackingEventPodDocument = subJobTrackingEvent.pocDocuments[n];

													if (subJobTrackingEventPodDocument != null)
													{
														var subJobTrackingEventPodDocumentContentType = subJobTrackingEventPodDocument.right.contentType;

														if ((!string.IsNullOrWhiteSpace(subJobTrackingEventPodDocumentContentType)) && (string.Equals("image/png", subJobTrackingEventPodDocumentContentType)))
														{
															ImageResponse imageResponse = new ImageResponse();

															imageResponse.Id = subJobTrackingEventPodDocument.right.id;

															imageResponse.Content = Convert.FromBase64String(subJobTrackingEventPodDocument.right.fileContent ?? ImageExtractor.base64NotFound);

															imageResponse.Received = subJobTrackingEventPodDocument.right.dateCreated;

															var subJobTrackingEventPodDocumentType = subJobTrackingEventPodDocument.right.documentType;

															if (subJobTrackingEventPodDocumentType != null)
															{
																imageResponse.ImageTypeId = subJobTrackingEventPodDocumentType.id;
															}

															imageResponses.Add(imageResponse);
														}
													}
												}
											}

											if ((subJobTrackingEvent.pocDocuments != null) && (docType == EDocumentType.POC))
											{
												for (int n = 0; n < subJobTrackingEvent.pocDocuments.Count; n++)
												{
													var subJobTrackingEventPocDocument = subJobTrackingEvent.pocDocuments[n];

													if (subJobTrackingEventPocDocument != null)
													{
														var subJobTrackingEventPocDocumentContentType = subJobTrackingEventPocDocument.right.contentType;

														if ((!string.IsNullOrWhiteSpace(subJobTrackingEventPocDocumentContentType)) && (string.Equals("image/png", subJobTrackingEventPocDocumentContentType)))
														{
															ImageResponse imageResponse = new ImageResponse();

															imageResponse.Id = subJobTrackingEventPocDocument.right.id;

															imageResponse.Content = Convert.FromBase64String(subJobTrackingEventPocDocument.right.fileContent ?? ImageExtractor.base64NotFound);

															imageResponse.Received = subJobTrackingEventPocDocument.right.dateCreated;

															var subJobTrackingEventPocDocumentType = subJobTrackingEventPocDocument.right.documentType;

															if (subJobTrackingEventPocDocumentType != null)
															{
																imageResponse.ImageTypeId = subJobTrackingEventPocDocumentType.id;
															}

															imageResponses.Add(imageResponse);
														}
													}
												}
											}
										}
									}
								}

								if ((legType == ELegType.DELIVERY) && (subJob.subJobLegs != null))
								{
									for (int k = 0; k < subJob.subJobLegs.Count; k++)
									{
										var subJobLeg = subJob.subJobLegs[k];

										if (subJobLeg != null)
										{
											if (subJobLeg.trackingEvents != null)
											{
												for (int p = 0; p < subJobLeg.trackingEvents.Count; p++)
												{
													var subJobLegTrackingEvent = subJobLeg.trackingEvents[p];

													if (subJobLegTrackingEvent != null)
													{
														if ((subJobLegTrackingEvent.pocDocuments != null) && (docType == EDocumentType.POD))
														{
															for (int q = 0; q < subJobLegTrackingEvent.pocDocuments.Count; q++)
															{
																var subJobLegTrackingEventPodDocument = subJobLegTrackingEvent.pocDocuments[q];

																if (subJobLegTrackingEventPodDocument != null)
																{
																	var subJobLegTrackingEventPodDocumentContentType = subJobLegTrackingEventPodDocument.right.contentType;

																	if ((!string.IsNullOrWhiteSpace(subJobLegTrackingEventPodDocumentContentType)) && (string.Equals("image/png", subJobLegTrackingEventPodDocumentContentType)))
																	{
																		ImageResponse imageResponse = new ImageResponse();

																		imageResponse.Id = subJobLegTrackingEventPodDocument.right.id;

																		imageResponse.Received = subJobLegTrackingEventPodDocument.right.dateCreated;

																		imageResponse.Content = Convert.FromBase64String(subJobLegTrackingEventPodDocument.right.fileContent ?? ImageExtractor.base64NotFound);

																		var subJobLegTrackingEventPodDocumentType = subJobLegTrackingEventPodDocument.right.documentType;

																		if (subJobLegTrackingEventPodDocumentType != null)
																		{
																			imageResponse.ImageTypeId = subJobLegTrackingEventPodDocumentType.id;
																		}

																		imageResponses.Add(imageResponse);
																	}
																}
															}
														}

														if ((subJobLegTrackingEvent.pocDocuments != null) && (docType == EDocumentType.POC))
														{
															for (int q = 0; q < subJobLegTrackingEvent.pocDocuments.Count; q++)
															{
																var subJobLegTrackingEventPocDocument = subJobLegTrackingEvent.pocDocuments[q];

																if (subJobLegTrackingEventPocDocument != null)
																{
																	var subJobLegTrackingEventPocDocumentContentType = subJobLegTrackingEventPocDocument.right.contentType;

																	if ((!string.IsNullOrWhiteSpace(subJobLegTrackingEventPocDocumentContentType)) && (string.Equals("image/png", subJobLegTrackingEventPocDocumentContentType)))
																	{
																		ImageResponse imageResponse = new ImageResponse();

																		imageResponse.Id = subJobLegTrackingEventPocDocument.right.id;

																		imageResponse.Received = subJobLegTrackingEventPocDocument.right.dateCreated;

																		imageResponse.Content = Convert.FromBase64String(subJobLegTrackingEventPocDocument.right.fileContent ?? ImageExtractor.base64NotFound);

																		var subJobLegTrackingEventPocDocumentType = subJobLegTrackingEventPocDocument.right.documentType;

																		if (subJobLegTrackingEventPocDocumentType != null)
																		{
																			imageResponse.ImageTypeId = subJobLegTrackingEventPocDocumentType.id;
																		}

																		imageResponses.Add(imageResponse);
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			List<byte[]> images = new List<byte[]>();

			if (imageResponses != null)
			{
				List<int> documentIds = imageResponses.Select(c => c.Id).ToList();

				ImageExtractor.imageClient.Initialise(base.ApiToken);

				ImageExtractor.imageClient.UseEndpoint(ComoApiConstants.BaseComoUrl + ComoApiConstants.InternalImageEndpoint + "?ids=" + string.Join(",", documentIds));

				byte[] response = Task.Run(async () => await ImageExtractor.imageClient.GetZipAsync(documentIds)).Result;

				if (response != null)
				{
					if (documentIds.Count == 1)
					{
						images.Add(response);
					}
					else
					{
						images = GetImagesFromZip(response).ToList();
					}
				}

				//if (docType == EDocumentType.POD)
				//{
				//    imageResponses.Sort((x, y) =>
				//    {
				//        int ret = 0;

				//        if (x.Received.HasValue && y.Received.HasValue)
				//        {
				//            ret = x.Received.Value.CompareTo(y.Received.Value);
				//        }

				//        if (ret == 0) ret = x.Id.CompareTo(y.Id);
				//        return ret;
				//    });

				//    return imageResponses.LastOrDefault()?.Content;
				//}
				//else if (docType == EDocumentType.POC)
				//{
				//    //return IEnumerable<byte[]>
				//    return null;
				//}
			}

			return images;
		}

		private IEnumerable<byte[]> GetImagesFromZip(byte[] zipFile)
		{
			List<byte[]> images = null;

			try
			{
				images = new List<byte[]>();

				using (MemoryStream memoryStream = new MemoryStream(zipFile))
				{
					using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
					{
						for (int i = 0; i < zipArchive.Entries.Count; i++)
						{
							ZipArchiveEntry entry = zipArchive.Entries[i];
							byte[] image = GetImageFromStream(entry.Open(), entry.CompressedLength);
							if (!(image is null))
							{
								images.Add(image);
							}
						}
					}
				}
			}
			catch (Exception e)
			{

			}

			return images;
		}

		private byte[] GetImageFromStream(Stream stream, long compressedLength)
		{
			byte[] image = null;

			try
			{
				byte[] buffer = new byte[compressedLength];

				using (MemoryStream ms = new MemoryStream())
				{
					int read;
					while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
					{
						ms.Write(buffer, 0, read);
					}
					image = ms.ToArray();
				}
			}
			catch (Exception e)
			{

			}

			return image;
		}
	}
}