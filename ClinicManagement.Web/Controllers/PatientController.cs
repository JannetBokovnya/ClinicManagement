using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Services;
using ClinicManagement.Web.ViewModel;

namespace ClinicManagement.Web.Controllers
{
    
    public class PatientController : Controller
    {
        private readonly IClinicService<Patient> _patientService;
        private readonly IClinicService<City> _cityService;
        private readonly IAppointmentService _appointmentService;
        private readonly IClinicService<Attendance> _attendanceService;
       
        public PatientController(IClinicService<Patient> patientService, IClinicService<City> cityService, IAppointmentService appointmentService, IClinicService<Attendance> attendanceService)
        {
            _patientService = patientService;
            _cityService = cityService;
            _appointmentService = appointmentService;
            _attendanceService = attendanceService;
           
        }
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPatients()
        {
            return Json(new { data = _patientService.GetAllWithInclude().ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var viewModel = new PatientDetailViewModel()
            {
                Patient = _patientService.Get(id),
                Appointments = _appointmentService.GetAppointmentWithPatient(id),
                 Attendances = _attendanceService.Find(t => (t.PatientId == id))
                
            //CountAppointments = Appointments.CountAppointments(id),
            //CountAttendance = Attandences.CountAttendances(id)
        };
            return View("Details", viewModel);
        }


        public ActionResult Create()
        {
            var viewModel = new PatientFormViewModel
            {
                Cities = _cityService.GetAll(),
                Heading = "Новый пациет"
            };
            return View("PatientForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var patient = _patientService.Get(id);

            var viewModel = new PatientFormViewModel
            {
                Heading = "Редактировать пациента",
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName=patient.LastName,
                Phone = patient.Phone,
                Date = patient.DateTime,
                //Date = patient.DateTime.ToString("d MMM yyyy"),
                BirthDate = patient.BirthDate.ToString("dd/MM/yyyy"),
                Address = patient.Address,
                Height = patient.Height,
                Weight = patient.Weight,
                Sex = patient.Sex,
                Token=patient.Token,
                City = patient.CityId,
                Cities = _cityService.GetAll()
            };
            return View("PatientForm", viewModel);
        }

    }
}