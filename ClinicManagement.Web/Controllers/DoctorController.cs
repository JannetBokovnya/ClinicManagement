using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Services;
using ClinicManagement.Web.ViewModel;
using Microsoft.AspNet.Identity;

namespace ClinicManagement.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IClinicService<Doctor> _clinicDoctorService;
        private readonly IClinicService<Specialization> _specService;
        private readonly IAppointmentService _appointmentService;
       

        public DoctorController(IClinicService<Doctor> clinicDoctorService, IClinicService<Specialization> specService, IAppointmentService appointmentService)
        {
            _clinicDoctorService = clinicDoctorService;
            _specService = specService;
            _appointmentService = appointmentService;


    }
    // GET: Doctor
    public ActionResult Index()
        {
            var doctorsList = _clinicDoctorService.GetAllWithInclude();
            //return _context.Doctors
            //   .Include(s => s.Specialization)
            //   .Include(u => u.Physician)
            //   
            
            return View(doctorsList);
        }

        //Details for admin
        public ActionResult Details(int id)
        {
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _clinicDoctorService.Get(id),
                UpcomingAppointments = _appointmentService.GetTodaysAppointments(id),
                Appointments = _appointmentService.GetAppointmentByDoctor(id)
            };
            //.Include(s => s.Specialization)
            //    .Include(u => u.Physician)
            //    .SingleOrDefault(d => d.Id == id);
            return View(viewModel);
        }

        //Details for doctor
        public ActionResult DoctorProfile()
        {
            var userId = User.Identity.GetUserId();
            var viewModel = new DoctorDetailViewModel
            {
                //доделать с доступом позже
               // Doctor = _clinicDoctorService.Get(userId),
                //Appointments = _unitOfWork.Appointments.GetUpcommingAppointments(userId),
            };
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var doctor = _clinicDoctorService.Get(id);
            if (doctor == null) return HttpNotFound();
            var viewModel = new DoctorFormViewModel()
            {

                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Phone = doctor.Phone,
                Address = doctor.Address,
                IsAvailable = doctor.IsAvailable,
                Specialization = doctor.SpecializationId,
                Specializations = _specService.GetAll()

            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Specializations = _specService.GetAll();
                return View(viewModel);
            }

            var doctorInDb = _clinicDoctorService.Get(viewModel.Id);
            doctorInDb.Id = viewModel.Id;
            doctorInDb.FirstName = viewModel.FirstName;
            doctorInDb.LastName = viewModel.LastName;
            doctorInDb.Phone = viewModel.Phone;
            doctorInDb.Address = viewModel.Address;
            doctorInDb.IsAvailable = viewModel.IsAvailable;
            doctorInDb.SpecializationId = viewModel.Specialization;

            _clinicDoctorService.SaveOrUpdate(doctorInDb, viewModel.Id);

            return RedirectToAction("Details", new { id = viewModel.Id });
        }




    }
}