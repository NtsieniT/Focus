using Focus.Incident.Domain.Application.Models;
using Focus.Incident.Domain.AssignmentGroup.Models;
using Focus.Incident.Domain.Person;
using Focus.Incident.Domain.Person.Models;
using Focus.Incident.Domain.PrimaryBusinessLine.Models;
using Focus.Incident.Domain.Report.Models;
using Microsoft.EntityFrameworkCore;

namespace Focus.Incident.Infrastructure.DB.EntityModels
{
    public class ApplicationDbContext : DbContext
    {
      
        public virtual DbSet<Domain.Incident.Models.Incident> Incident { get; set; }
        public virtual DbSet<PrimaryBusinessLine> PrimaryBusinessLine { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<AssignmentGroup> AssignmentGroup { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        public virtual DbSet<ApplicationCount> ApplicationCount { get; set; }
        public virtual DbSet<AssignmentGroupCount> AssignmentGroupCount { get; set; }
        public virtual DbSet<BusinessLineCount> BusinessLineCount { get; set; }       
        public virtual DbSet<PersonCount> PersonCount { get; set; }
        public virtual DbSet<SummaryCounters> SummaryCount { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // seed data correctly when EF Core 2.1. comes out
            //builder.Entity<Role>().HasData(new Role { Id = 1, Name = Constants.Role_Administrator });
        }
    }
}
