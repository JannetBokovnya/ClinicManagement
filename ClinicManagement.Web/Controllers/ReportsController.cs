using ClinicManagement.Web.Models;
using ClinicManagement.Web.Services;
using ClinicManagement.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicManagement.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClinicService<Doctor> _clinicDoctorService;
        private readonly IClinicService<Attendance> _attendanceService;
        // GET: Reports

        public ReportsController(IAppointmentService appointmentService, IClinicService<Doctor> clinicDoctorService, IClinicService<Attendance> attendanceService)
        {
            _appointmentService = appointmentService;
            _clinicDoctorService = clinicDoctorService;
            _attendanceService = attendanceService;
        }
        public ActionResult Index()
        {
            return View();
        }
        //====================Daily appointment==============//

        public ActionResult DaillyAppointments()
        {
            var daily = _appointmentService.GetAppointments();
            return View(daily);
        }

        public ActionResult Dailly(DateTime? getDate, DateTime? getDate2)
        {
            var dailyAppointments = _appointmentService.GetDaillyAppointments(getDate, getDate2);
            return PartialView("_DaillyAppointments", dailyAppointments);
        }

        //====================End Daily appointment==============//

        // ===============Appointment ========================//
        public ActionResult Appointments()
        {
            var appointments = _appointmentService.GetAppointments();
            var viewModel = new AppointmentReportViewModel()
            {
                Appointments = appointments,
                Doctors = _clinicDoctorService.GetAllWithInclude()
            };
 
            return View(viewModel);
        }

        public ActionResult Appointment(AppointmentSearchVM viewModel)
        {
            var filter = _appointmentService.FilterAppointments(viewModel);
            var viewModelFiltr = new AppointmentReportViewModel()
            {
                Appointments = filter,
                Doctors = _clinicDoctorService.GetAllWithInclude()
            };
            return PartialView("_Appointments", viewModelFiltr);
        }

        //===============End Appointment===================//

        //======================Attandance ========================//
        public ActionResult Attandences()
        {
            var attandences = _attendanceService.GetAll();
            return View(attandences);
        }
        public ActionResult PatientAttandence(string token = null)
        {
            var patientAttandences = _attendanceService.GetAll();

            if (!string.IsNullOrWhiteSpace(token))
            {
                patientAttandences = patientAttandences.Where(p => p.Patient.Token.Contains(token));
            }
            return View("_AttandencePartial", patientAttandences);
        }

        // ===============Appointment ========================//
    }
}