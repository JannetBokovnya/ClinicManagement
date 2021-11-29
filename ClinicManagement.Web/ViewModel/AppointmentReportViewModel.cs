using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.ViewModel
{
    public class AppointmentReportViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }
    }
}