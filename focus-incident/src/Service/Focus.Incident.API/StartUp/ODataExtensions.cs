using Focus.Incident.Domain.Application.Models;
using Focus.Incident.Domain.AssignmentGroup.Models;
using Focus.Incident.Domain.Person.Models;
using Focus.Incident.Domain.PrimaryBusinessLine.Models;
using Focus.Incident.Domain.PrimaryBusinessLine.Services;
using Focus.Incident.Domain.Report.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Focus.Incident.API.StartUp
{
    public static partial class ODataExtensions
    {
        public static IApplicationBuilder AddCustomOData(this IApplicationBuilder app)
        {
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Domain.Incident.Models.Incident>("Incident").EntityType.HasKey(x => x.Id);
            builder.EntitySet<BusinessLineCount>("PrimaryBusinessLines").EntityType.HasKey(x => x.Id);
            builder.EntitySet<PersonCount>("Person").EntityType.HasKey(x => x.Id);
            builder.EntitySet<ApplicationCount>("Application").EntityType.HasKey(x => x.Id);
            builder.EntitySet<AssignmentGroupCount>("AssignmentGroup").EntityType.HasKey(x => x.Id);
            builder.EntitySet<SummaryCounters>("Summary").EntityType.HasKey(x => x.Id);


            
            

            app.UseMvc(routeBuilder =>
            {
                // work-around for #1179
                routeBuilder.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());
                // Work-around for #1175
                routeBuilder.EnableDependencyInjection();
            });

            return app;
        }

        }
    }
