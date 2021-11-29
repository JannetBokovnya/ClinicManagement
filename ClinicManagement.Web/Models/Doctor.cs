using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClinicManagement.Web.Models
{
    //доктор
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public string PhysicianId { get; set; }
        public ApplicationUser Physician { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public Doctor()
        {
            Appointments = new Collection<Appointment>();
        }

        public string FullNameDoctor
        {
            get
            {
                string fullName =FirstName + " " + LastName;
                return fullName;
            }

        }
    }
}