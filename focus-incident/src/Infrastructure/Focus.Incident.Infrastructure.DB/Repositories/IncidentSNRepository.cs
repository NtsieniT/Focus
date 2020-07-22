//using Focus.Incident.Domain.Incident.Interfaces;
//using Focus.Incident.Domain.Incident.Models.Queries;
//using Focus.Incident.Infrastructure.DB.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Text;
//using System.Threading.Tasks;
//using DomainModel = Focus.Incident.Domain.Incident.Models;

//namespace Focus.Incident.Infrastructure.DB.Repositories
//{
//    public class IncidentSNRepository : IIncidentSNRepository
//    {
//        private string ConnectionString;

//        public IncidentSNRepository(string connectionString)
//        {
//            this.ConnectionString = connectionString;
//        }
//        public async Task<IncidentQueryResult> ReadAsync(IncidentQuery query)
//        {
//            var result = new IncidentQueryResult();
//            var results = new List<DomainModel.Incident>();

//            #region SQL query String for service Now
//            string sqlSelectString = @"
//                    SELECT [Number]
//                        ,[Created]
//                        ,[Resolved]
//                        ,[Closed]
//	                    ,[Updated]
//	                    ,[Severity]
//                        ,[Priority]
//                        ,[Short_Description]
//                        ,[CMDB_CI]
//                        ,[CMDB_CI_Company]
//        	            ,[Country_Impacted]
//                        ,[ITMA_Business_Line]
//                        ,[Caused_by]
//	                    ,[Incident_State]
//	                    ,[Full_Business_Impact]
//                        ,[Assigned_To]
//                        ,[Assignment_Group]
//                        ,[Caller_ID]
//                        ,[Classification]
//                        ,[Resolution_Description]
//                        ,[Vendor]
//                        ,[Major_Incident]
//                        ,[Problem_ID]
//                        ,[ITMA_Company]
//                        ,[Caller_ID_Business_Area4]
//                    FROM
//                        [ServiceNow].[dbo].[tblSNIncident]
//Where
//                        ([ITMA_Company] = 'ABSA' or
//                        [ITMA_Company] = 'BARCLAYS BANK BOTSWANA' or
//                        [ITMA_Company] = 'BARCLAYS BANK GHANA' or
//                        [ITMA_Company] = 'BARCLAYS BANK KENYA' or
//                        [ITMA_Company] = 'BARCLAYS BANK MAURITIUS' or
//                        [ITMA_Company] = 'BARCLAYS BANK MOZAMBIQUE (BBM)' or
//                        [ITMA_Company] = 'BARCLAYS BANK SEYCHELLES' or
//                        [ITMA_Company] = 'BARCLAYS BANK TANZANIA' or
//                        [ITMA_Company] = 'BARCLAYS BANK UGANDA' or
//                        [ITMA_Company] = 'BARCLAYS BANK ZAMBIA' or
//                        [ITMA_Company] = 'NATIONAL BANK OF COMMERCE (NBC TANZANIA)' or
//                        [ITMA_Company] = 'BARCLAYS BANK ZIMBABWE' or
//                        [ITMA_Company] = 'BARCLAYS BANK EGYPT')
//                        and
//                        ([Incident_State] != 'Closed' and[Incident_State] != 'Resolved')
//                    ";
//            #endregion 
//            using (SqlConnection connection = new SqlConnection(ConnectionString))
//            {

//                Console.WriteLine("Opening sql connection");
//                try
//                {
//                    connection.Open();
//                }
//                catch
//                {
//                    Console.WriteLine("CRITICAL ERROR: Cannot open connection to SQL Server");
//                    Environment.Exit(1);
//                }
//                    Console.WriteLine("Starting query to obtain Incident data for Service Now");

//                using (SqlCommand command = new SqlCommand(sqlSelectString, connection))
//                {
//                    command.CommandTimeout = 480;
//                    //result.Total = (int?)command.ExecuteScalar();
//                    var reader = command.ExecuteReader();
//                    // map your reader
//                    while (reader.Read())
//                    {
//                        #region assign SQL fields to Incident Model
//                        var incident = new DomainModel.Incident();
//                        incident.Number = reader.SafeGetString(0);
//                        incident.Created = reader.SafeGetDate(1).Value;
//                        incident.Resolved = reader.SafeGetDate(2);
//                        incident.Closed = reader.SafeGetDate(3);
//                        incident.Last_Updated = reader.SafeGetDate(4);
//                        incident.Severity = reader.SafeGetString(5);
//                        incident.Priority = reader.SafeGetString(6);
//                        incident.ShortDescription = reader.SafeGetString(7);
//                        incident.CMDB_CI = reader.SafeGetString(8);
//                        incident.CMDB_CI_Company = reader.SafeGetString(9);
//                        incident.Country_Impacted = reader.SafeGetString(10);
//                        incident.Business_Line = reader.SafeGetString(11);
//                        incident.Caused_By = reader.SafeGetString(12);
//                        incident.State = reader.SafeGetString(13);
//                        incident.Business_Impact = reader.SafeGetDouble(14);
//                        incident.Assigned_to = reader.SafeGetString(15);
//                        incident.Assignment_Group = reader.SafeGetString(16);
//                        incident.Caller_ID = reader.SafeGetString(17);
//                        incident.Classification = reader.SafeGetString(18);
//                        incident.Resolution_Description = reader.SafeGetString(19);
//                        incident.Vendor = reader.SafeGetString(20);
//                        incident.Major_Incident = reader.GetBoolean(21);
//                        incident.Problem_ID = reader.SafeGetString(22);
//                        incident.Company = reader.SafeGetString(23);
//                        incident.Business_Area = reader.SafeGetString(24);
//                        #endregion assign SQL fields to Incident Model

//                        results.Add(incident);
//                    }

//                }
//            }


//            #region SQL query for Service First


//            #endregion

//            result.Results = results;
//            return result;

//        }
//    }
//}
