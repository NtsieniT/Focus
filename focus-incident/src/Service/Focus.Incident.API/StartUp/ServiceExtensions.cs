using Focus.Incident.Domain.Application.Services;
using Focus.Incident.Domain.AssignmentGroup.Services;
using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Incident.Interfaces;
using Focus.Incident.Domain.Incident.Service;
using Focus.Incident.Domain.Person.Services;
using Focus.Incident.Domain.PrimaryBusinessLine.Services;
using Focus.Incident.Domain.Report.Interfaces;
using Focus.Incident.Domain.Report.Models;
using Focus.Incident.Domain.Report.Services;
using Focus.Incident.Infrastructure.DB.EntityModels;
using Focus.Incident.Infrastructure.DB.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Focus.Incident.API.StartUp
{
    public static partial class Extensions
    {

        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));            
            services.AddScoped<IncidentImportService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IReportRepository<>), typeof(ReportRepository<>));
            //services.AddScoped(typeof(IIncidentRepository), typeof(IncidentService));
            services.AddScoped<IncidentService>();
            services.AddScoped<ReportService>();

            services.AddScoped<PrimaryBusinessLineService>();
            services.AddScoped<PersonService>();
            services.AddScoped<ApplicationService>();
            services.AddScoped<AssignmentGroupService>();
            services.AddScoped<SummaryService>();

            services.AddScoped<AssignmentGroupCount>();
            services.AddScoped<ApplicationCount>();
            services.AddScoped<PersonCount>();
            services.AddScoped<BusinessLineCount>();
            services.AddScoped<SummaryCounters>();
           
            
            return services;
        }
    }
}
