using Focus.Incident.Domain.Incident.Common.Interface;
using Focus.Incident.Domain.Incident.Interfaces;
using Focus.Incident.Domain.Incident.Models;
using Focus.Incident.Domain.Incident.Service;
using Focus.Incident.Infrastructure.DB.EntityModels;
using Focus.Incident.Infrastructure.DB.Repositories;
using Focus.Incident.Job.Schedules;
using Focus.Incident.Job.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Focus.Incident.Job.StartUp
{
    public static partial class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IncidentImportService>();
            services.AddScoped<CMDBIncidentResult>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IIncidentSNRepository,IncidentSNRepository>(x =>
            //{
            //    return new IncidentSNRepository(configuration.GetConnectionString("CMDB_ADO"));
            //});
            services.AddScoped<IIncidentSFRepository, IncidentSFRepository>(x =>
            {
                return new IncidentSFRepository(configuration.GetConnectionString("CMDB_ADO"));
            });


            services.AddScoped<IncidentImportSchedule, IncidentImportSchedule>();
            return services;
        }

        public static IServiceCollection AddSchedulers(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var cmdbDatasync in configuration.GetSection("Schedules:IncidentImport").GetChildren())
            {
                var schedule = cmdbDatasync.GetValue<string>("CronScheduleValue");

                services.AddSingleton<IScheduledTask>(sp =>
                {
                    var scheduledService = sp.GetService<IncidentImportSchedule>();
                    scheduledService.Schedule = schedule;
                    return scheduledService;
                });
            }

            services.AddScheduler((sender, args) =>
            {
                Console.Write(args.Exception.Message);
                args.SetObserved();
            });

            return services;
        } 
    }
}
