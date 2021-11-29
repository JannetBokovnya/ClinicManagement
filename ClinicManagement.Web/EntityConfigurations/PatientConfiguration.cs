using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace ClinicManagement.Web.EntityConfigurations
{
    public class PatientConfiguration: EntityTypeConfiguration<Patient>
    {
        public PatientConfiguration()
        {
            Property(p => p.CityId).IsRequired();
            Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            Property(p => p.LastName).IsRequired().HasMaxLength(255);
            Property(p => p.Phone).IsRequired().HasMaxLength(255);
            Property(p => p.Address).IsRequired().HasMaxLength(255);
            Property(p => p.BirthDate).IsRequired();
            Property(p => p.Token).IsRequired();
            HasMany(p => p.Appointments)
                .WithRequired(a => a.Patient)
                .WillCascadeOnDelete(false);
        }
    }
}