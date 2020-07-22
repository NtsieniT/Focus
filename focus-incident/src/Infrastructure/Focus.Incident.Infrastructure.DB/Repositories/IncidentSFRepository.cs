using Focus.Incident.Domain.Incident.Interfaces;
using Focus.Incident.Domain.Incident.Models;
using Focus.Incident.Domain.Incident.Models.Queries;
using Focus.Incident.Infrastructure.DB.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DomainModel = Focus.Incident.Domain.Incident.Models;

namespace Focus.Incident.Infrastructure.DB.Repositories
{
    public class IncidentSFRepository : IIncidentSFRepository
    {

        private string ConnectionString;

        public IncidentSFRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public async Task<IncidentQueryResult> ReadAsync(IncidentQuery query)
        {
            var result = new IncidentQueryResult();
            var results = new List<CMDBIncidentResult>();


            #region SQL query String for service Firsts
            string sqlSFSelectString = @"
                           SELECT 
                                Number,
                                Created,
                                resolved,
                                Closed,
                                Updated,
                                Severity,
                                [Priority],
                                [Short Description],
                                ISNULL([IT Business Service],'No Application') as [IT Business Service],
                                [Impacted Country(s)],
                                ISNULL([Department Level 3],'No Primary Business Line') as [Department Level 3],
                                [Caused by Change],
                                [state],
                                ISNULL([Assigned To], 'No Assigned Person') as [Assigned To],
                                ISNULL([Assignment Group],'No Assignment Group') as [Assignment Group],
                                Contact,
                                [Incident Type],
                                [Description],
                                Vendor,
                                [Parent Incident],
                                Company, 
                                [Contact Business Area4] 
                          FROM ServiceFirst.dbo.vwSFIncidentSPDWAll
                                where
                                ([State] != 'Closed' AND [State] != 'Resolved')
                               
                            ";

            #endregion 

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                Console.WriteLine("Opening sql connection");
                try
                {
                    connection.Open();
                }
                catch
                {
                    Console.WriteLine("CRITICAL ERROR: Cannot open connection to SQL Server");
                    Environment.Exit(1);
                }
                Console.WriteLine("Starting query to obtain Incident data for Service First");

                using (SqlCommand command = new SqlCommand(sqlSFSelectString, connection))
                {
                    command.CommandTimeout = 480;
                    //result.Total = (int?)command.ExecuteScalar();
                    var reader = command.ExecuteReader();
                    // map your reader
                    while (reader.Read())
                    {
                        #region assign SQL fields to Incident Model
                        var incident = new CMDBIncidentResult();
                        incident.Number = reader.SafeGetString(0);
                        incident.Created = reader.GetDateTime(1);
                        incident.Resolved = reader.SafeGetDate(2);
                        incident.Closed = reader.SafeGetDate(3);
                        incident.Last_Updated = reader.SafeGetDate(4);
                        incident.Severity = reader.SafeGetString(5);
                        incident.Priority = reader.SafeGetString(6);
                        incident.ShortDescription = reader.SafeGetString(7);
                        incident.CMDB_CI = reader.SafeGetString(8);
                        //aNewIncident.CMDB_CI_Company = reader.SafeGetString(9);
                        incident.Country_Impacted = reader.SafeGetString(9);
                        incident.Business_Line = reader.SafeGetString(10);
                        incident.Caused_By = reader.SafeGetString(11);
                        incident.State = reader.SafeGetString(12);
                        //aNewIncident.Business_Impact = SafeGetDouble(reader, 14);
                        incident.Assigned_to = reader.SafeGetString(13);
                        incident.Assignment_Group = reader.SafeGetString(14);
                        incident.Caller_ID = reader.SafeGetString(15);
                        incident.Classification = reader.SafeGetString(16);
                        incident.Resolution_Description = reader.SafeGetString(17);
                        incident.Vendor = reader.SafeGetString(18);
                        //aNewIncident.Major_Incident = reader.GetBoolean(17);
                        //incident.Problem_ID = reader.SafeGetString(19);
                        incident.Company = reader.SafeGetString(20);
                        incident.Business_Area = reader.SafeGetString(21);
                        #endregion assign SQL fields to Incident Model

                        //if (!result.Any(x => x.Number == incident.Number))
                        //{
                        results.Add(incident);
                        //} 
                    }
                }
            }

            result.Results = results;
            return result;
        }
    }
}
