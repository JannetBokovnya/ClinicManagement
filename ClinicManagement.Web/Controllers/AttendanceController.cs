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
    public class AttendanceController : Controller
    {
        private readonly IClinicService<Patient> _clinicPatientService;
        private readonly IClinicService<Attendance> _attendanceService;

        public AttendanceController(IClinicService<Patient> clinicPatientService, IClinicService<Attendance> attendanceService)
        {
            _clinicPatientService = clinicPatientService;
            _attendanceService = attendanceService;

        }

        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            var viewModel = new AttendanceFormViewModel
            {
                Patient = id,
                Heading = "Add Attendance"
            };
            return View("AttendanceForm", viewModel);
        }

        [HttpPost]
        public ActionResult Create(AttendanceFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("AttendanceForm", viewModel);

            var attendance = new Attendance
            {
                Id = viewModel.Id,
                ClinicRemarks = viewModel.ClinicRemarks,
                Diagnosis = viewModel.Diagnosis,
                SecondDiagnosis = viewModel.SecondDiagnosis,
                ThirdDiagnosis = viewModel.ThirdDiagnosis,
                Therapy = viewModel.Therapy,
                Date = DateTime.Now,
                PatientId= viewModel.Patient,
               
            };
            _attendanceService.SaveOrUpdate(attendance, 0);
            return RedirectToAction("Details", "Patient", new { id = viewModel.Patient });

        }
    }
}