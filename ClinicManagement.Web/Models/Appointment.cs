using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.Models
{
    //запись на прием к доктору
    public class Appointment
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public string TimeBlockHelper { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int CompareTo(Appointment other)
        {
            return this.Date.Date.Add(this.Time.TimeOfDay).CompareTo(other.Date.Date.Add(other.Time.TimeOfDay));
        }
        public string FullNamePatient
        {
            get
            {
                string fullName = Patient.FirstName + " " + Patient.LastName;
                return fullName;
            }

        }

        public string FullNameDoctor
        {
            get
            {
                string fullName = Doctor.FirstName + " " + Doctor.LastName;
                return fullName;
            }

        }
    }


   
}