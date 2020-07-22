﻿// <auto-generated />
using System;
using Focus.Incident.Infrastructure.DB.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Focus.Incident.Infrastructure.DB.Migrations.staging.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Focus.Incident.Domain.Application.Models.Application", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("Focus.Incident.Domain.AssignmentGroup.Models.AssignmentGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AssignmentGroup");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Incident.Models.Incident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ApplicationId");

                    b.Property<long?>("Assigned_toId");

                    b.Property<long?>("AssignmentGroupId");

                    b.Property<string>("Business_Area");

                    b.Property<float?>("Business_Impact");

                    b.Property<string>("CMDB_CI_Company");

                    b.Property<string>("Caller_ID");

                    b.Property<string>("Caused_By");

                    b.Property<string>("Classification");

                    b.Property<DateTime?>("Closed");

                    b.Property<string>("Company");

                    b.Property<string>("Country_Impacted");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("Last_Updated");

                    b.Property<bool>("Major_Incident");

                    b.Property<string>("Number");

                    b.Property<long?>("PrimaryBusinessLineId");

                    b.Property<string>("Priority");

                    b.Property<string>("Problem_ID");

                    b.Property<string>("Resolution_Description");

                    b.Property<DateTime?>("Resolved");

                    b.Property<string>("Severity");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("State");

                    b.Property<string>("Vendor");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Assigned_toId");

                    b.HasIndex("AssignmentGroupId");

                    b.HasIndex("PrimaryBusinessLineId");

                    b.ToTable("Incident");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Person.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Focus.Incident.Domain.PrimaryBusinessLine.Models.PrimaryBusinessLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PrimaryBusinessLine");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Report.Models.ApplicationCount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationName");

                    b.Property<int>("Counter");

                    b.HasKey("Id");

                    b.ToTable("ApplicationCount");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Report.Models.AssignmentGroupCount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignmentGroup");

                    b.Property<int>("Counter");

                    b.HasKey("Id");

                    b.ToTable("AssignmentGroupCount");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Report.Models.BusinessLineCount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessLine");

                    b.Property<int>("Counter");

                    b.HasKey("Id");

                    b.ToTable("BusinessLineCount");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Report.Models.PersonCount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Counter");

                    b.Property<string>("personName");

                    b.HasKey("Id");

                    b.ToTable("PersonCount");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Report.Models.SummaryCounters", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IncidentCounter");

                    b.Property<int>("NetcoolCounter");

                    b.Property<int>("RequestCounter");

                    b.Property<int>("SOICounter");

                    b.Property<int>("TotalIncidents");

                    b.HasKey("Id");

                    b.ToTable("SummaryCount");
                });

            modelBuilder.Entity("Focus.Incident.Domain.Incident.Models.Incident", b =>
                {
                    b.HasOne("Focus.Incident.Domain.Application.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("Focus.Incident.Domain.Person.Models.Person", "Assigned_to")
                        .WithMany()
                        .HasForeignKey("Assigned_toId");

                    b.HasOne("Focus.Incident.Domain.AssignmentGroup.Models.AssignmentGroup", "AssignmentGroup")
                        .WithMany()
                        .HasForeignKey("AssignmentGroupId");

                    b.HasOne("Focus.Incident.Domain.PrimaryBusinessLine.Models.PrimaryBusinessLine", "PrimaryBusinessLine")
                        .WithMany()
                        .HasForeignKey("PrimaryBusinessLineId");
                });
#pragma warning restore 612, 618
        }
    }
}
