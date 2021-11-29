using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.ViewModel
{
    public class DoctorDetailViewModel
    {
        public Doctor Doctor { get; set; }
        public IEnumerable<Appointment> UpcomingAppointments { get; set; } //назначенный прием  на ближайшее время
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}