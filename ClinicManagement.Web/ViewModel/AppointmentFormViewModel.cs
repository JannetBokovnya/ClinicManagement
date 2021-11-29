using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Web.ViewModel
{
    public class AppointmentFormViewModel
    {
        public int IdAppointment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        [Required]
        public string Detail { get; set; }

        [Required]
        public bool Status { get; set; }


        [Required]
        public int Patient { get; set; }
        public IEnumerable<Patient> Patients { get; set; }

        [Required]
        public int Doctor { get; set; }

        public string TimeBlockHelper { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }
        public string Heading { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }


        public DateTime GetStartDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }


        //public string Action
        //{
        //    get
        //    {
        //        Expression<Func<AppointmentsController, ActionResult>> update = (c => c.Update(this));
        //        Expression<Func<AppointmentsController, ActionResult>> create = (c => c.Create(this));
        //        var action = (Id != 0) ? update : create;
        //        return (action.Body as MethodCallExpression).Method.Name;
        //    }
        //}

    }
}
