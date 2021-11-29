using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.Common
{
    using ClinicManagement.Web.EntityConfigurations;
    using ClinicManagement.Web.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ClinicContext : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'ClinicaContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ClinicManagement.Web.Common.ClinicaContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ClinicaContext' 
        // connection string in the application configuration file.
        public ClinicContext()
            : base("name=ClinicDBContext", throwIfV1Schema: false)
        {
        }


        public static ClinicContext Create()
        {
            return new ClinicContext();
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AdministrationModel> Administrations { get; set; }



        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Configurations.Add(new PatientConfiguration());
        //    //base.OnModelCreating(modelBuilder);
        //}
    }
}