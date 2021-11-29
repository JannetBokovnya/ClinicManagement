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
    public class AppointmentsController : Controller
    {
        private readonly IClinicService<Doctor> _clinicDoctorService;
        private readonly IAppointmentService _appointmentService;
        
        public AppointmentsController(IClinicService<Doctor> clinicDoctorService, IAppointmentService appointmentService)
        {
            _clinicDoctorService = clinicDoctorService;
            _appointmentService = appointmentService;

        }
        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = _appointmentService.GetAppointments();
           
            return View(appointments);
        }

        public ActionResult Create(int id)
        {
            ViewBag.TimeBlockHelper = new SelectList(String.Empty);

            DateTime dnow = DateTime.Now;
            var viewModel = new AppointmentFormViewModel
            {
                Patient = id,
                IdAppointment = 0,
                Date = DateTime.Now,
                
                Doctors = _clinicDoctorService.Find(t => (t.IsAvailable == true)),

                Heading = "Новая запись к врачу"
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentFormViewModel viewModel)
        {

            if (viewModel.TimeBlockHelper == "DONT")
                ModelState.AddModelError("", "No Appointments Available for " + viewModel.Date.ToString());
            else
            {
                //Set Time
                viewModel.Time = DateTime.Parse(viewModel.TimeBlockHelper);

                //CheckWorkingHours
                DateTime start = Convert.ToDateTime(viewModel.Date).Add(viewModel.Time.TimeOfDay);
                DateTime end = (Convert.ToDateTime(viewModel.Date).Add(viewModel.Time.TimeOfDay)).AddMinutes(_appointmentService.GetFields(1));
                if (!(_appointmentService.IsInWorkingHours(start, end)))
                    ModelState.AddModelError("", "Часы работы доктора с " + _appointmentService.GetFields(2) + " по " + _appointmentService.GetFields(3));

                //Check Appointment Clash
                string check = _appointmentService.ValidateNoAppoinmentClash(viewModel);
                if (check != "")
                    ModelState.AddModelError("", check);
            }

            //Continue Normally

            if (ModelState.IsValid)
            {
                var appointment = new Appointment()
                {
                    //Date = viewModel.Date,
                    Date = Convert.ToDateTime(viewModel.Date),
                    Time = viewModel.Time,
                    TimeBlockHelper = viewModel.TimeBlockHelper,
                    Detail = viewModel.Detail,
                    Status = false,
                    PatientId = viewModel.Patient,
                    //Doctor = _clinicDoctorService.Get(viewModel.Doctor),
                    DoctorId=viewModel.Doctor,
                    Id = 0
                };

                _appointmentService.SaveOrUpdate(appointment, 0);
               
            }

            return RedirectToAction("Index", "Appointments");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.TimeBlockHelper = new SelectList(String.Empty);
            
            var appointment = _appointmentService.GetAppointment(id);
            ViewBag.Date = appointment.Date.Date;
            var viewModel = new AppointmentFormViewModel()
            {
                Heading = "Редактировать Appointment",
                IdAppointment = appointment.Id,
                Date = appointment.Date,
                
                Time = appointment.Time,
                Detail = appointment.Detail,
                Status = appointment.Status,
                Patient = appointment.PatientId,
                Doctor = appointment.DoctorId,
                TimeBlockHelper=appointment.TimeBlockHelper,
                Doctors = _clinicDoctorService.Find(t => (t.IsAvailable == true))
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentFormViewModel viewModel)
        {
            if (viewModel.TimeBlockHelper == "DONT")
                //ModelState.AddModelError("", "No Appointments Available for " + viewModel.Date.ToShortDateString());
                ModelState.AddModelError("", "No Appointments Available for " + viewModel.Date.ToString());
            else
            {
                //Set Time
                viewModel.Time = DateTime.Parse(viewModel.TimeBlockHelper);

                //CheckWorkingHours
                DateTime start = Convert.ToDateTime(viewModel.Date).Add(viewModel.Time.TimeOfDay);
                DateTime end = (Convert.ToDateTime(viewModel.Date).Add(viewModel.Time.TimeOfDay)).AddMinutes(_appointmentService.GetFields(1));
                if (!(_appointmentService.IsInWorkingHours(start, end)))
                    ModelState.AddModelError("", "Часы работы доктора с " + _appointmentService.GetFields(2) + " по " + _appointmentService.GetFields(3));

                //Check Appointment Clash
                string check = _appointmentService.ValidateNoAppoinmentClash(viewModel);
                if (check != "")
                    ModelState.AddModelError("", check);
            }


            if (!ModelState.IsValid)
            {
                //viewModel.Doctors = _unitOfWork.Doctors.GetDectors();
                //viewModel.Patients = _unitOfWork.Patients.GetPatients();
                return View(viewModel);
            }

            var appointmentInDb = _appointmentService.GetAppointment(viewModel.IdAppointment);

            appointmentInDb.Id = viewModel.IdAppointment;
            appointmentInDb.Date = Convert.ToDateTime(viewModel.Date);
            appointmentInDb.Time = viewModel.Time;
            appointmentInDb.TimeBlockHelper = viewModel.TimeBlockHelper;
            appointmentInDb.Detail = viewModel.Detail;
            appointmentInDb.Status = viewModel.Status;
            appointmentInDb.PatientId = viewModel.Patient;
            appointmentInDb.DoctorId = viewModel.Doctor;

            _appointmentService.SaveOrUpdate(appointmentInDb, viewModel.IdAppointment);


            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult UpdateStatus(int id, bool status)
        {
            
            var result = _appointmentService.GetAppointment(id); ;

            if (result != null)
            {
                result.Status = status;
                _appointmentService.SaveOrUpdate(result, 1);
            }
            return Json(true);
        }

        public JsonResult GetAvailableAppointments(int docID, DateTime date)
        {
            List<SelectListItem> resultlist = _appointmentService.AvailableAppointments(docID, date);
            return Json(resultlist);
        }

        //private static void ConvertToDateTime(string value)
        //{
        //    DateTime convertedDate;
        //    try
        //    {
        //        convertedDate = Convert.ToDateTime(value);

        //    }
        //    catch (FormatException)
        //    {
        //        Console.WriteLine("'{0}' is not in the proper format.", value);
        //    }
        //}
    }
}