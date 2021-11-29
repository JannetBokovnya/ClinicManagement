using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClinicService<Patient> _patientService;
        private readonly IClinicService<Doctor> _clinicDoctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly IApplicationUserServise _applicationUser;
        

        public HomeController(IClinicService<Patient> patientService, IAppointmentService appointmentService, 
            IClinicService<Doctor> clinicDoctorService, IApplicationUserServise applicationUser)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _clinicDoctorService = clinicDoctorService;
            _applicationUser = applicationUser;
           

        }
        public ActionResult Index()
        {

            var list = _appointmentService.GetAppointments();
            List<int> repartitions = new List<int>();
            List<string> doctorName = new List<string>();
            //var doctors=list.Select(t => new { t.DoctorId, t.FullNameDoctor}).Distinct();
            var doctors = _clinicDoctorService.GetAll();
            foreach (var item in doctors)
            {
                repartitions.Add(list.Count(x => x.DoctorId == item.Id));
                doctorName.Add(item.FullNameDoctor);
            }

            ViewBag.Doctors = doctorName;
            ViewBag.Rep = repartitions;

            return View();
        }

        #region Dashboard Statistics
        public ActionResult TotalPatients()
        {
            var patients = _patientService.GetAll();
           
            return Json(patients.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TodaysPatients()
        {
            DateTime today = DateTime.Now.Date;
            var patients = _patientService.Find(t => (t.DateTime >= today));

            return Json(patients.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TotalAppointments()
        {
            var appointments = _appointmentService.CountAppointments(0);
            return Json(appointments, JsonRequestBehavior.AllowGet);
        }

        //Todays appointments
        public ActionResult TodaysAppointments()
        {
            DateTime today = DateTime.Now.Date;
            var appointments = _appointmentService.Find(t => (t.Date >= today));
            return Json(appointments.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TotalDoctors()
        {
            var doctors = _clinicDoctorService.GetAll();
            return Json(doctors.Count(), JsonRequestBehavior.AllowGet);
        }

        //Available doctors
        public ActionResult AvailableDoctors()
        {
            var doctors = _clinicDoctorService.Find(d => d.IsAvailable);
            return Json(doctors.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TotalUsers()
        {
            var users = _applicationUser.GetUsers();
            return Json(users.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActiveAccounts()
        {
            var users = _applicationUser.GetAllWithInclude();
            return Json(users.Count(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult DashboardChart()
        {
            var listAppointments = _appointmentService.GetAll().ToList();
            //List<int> monsAppoint = new List<int>();
            List<string> month = new List<string>();
            List<int> countmonth = new List<int>();
            Dictionary<string, int> tt = new Dictionary<string, int>();

            Dictionary<string, int> monthcount = new Dictionary<string, int>();
            Dictionary<string, int> monthcount1 = new Dictionary<string, int>();

            var listMons = listAppointments.Select(x => x.Date.Month.ToString()).Distinct();

            foreach (var item in listMons)
            {
                //monsAppoint.Add(listAppointments.Count(x => x.Date.Month.ToString() == item));
                tt.Add(item, listAppointments.Count(x => x.Date.Month.ToString() == item));
            }

            for (int i = 1; i <= 12; i++)
            {
                int value;
                if (tt.TryGetValue(i.ToString(), out value))
                {
                    monthcount.Add(i.ToString(), value);
                }
                else
                {
                    monthcount.Add(i.ToString(), 0);
                }
            }

            foreach (var item in monthcount)
            {
                switch (item.Key)
                {
                    case "1":
                        monthcount1.Add("Jan", item.Value);
                        break;
                    case "2":
                        monthcount1.Add("Feb", item.Value);
                        break;
                    case "3":
                        monthcount1.Add("Mar", item.Value);
                        break;
                    case "4":
                        monthcount1.Add("Apr", item.Value);
                        break;
                    case "5":
                        monthcount1.Add("May", item.Value);
                        break;
                    case "6":
                        monthcount1.Add("Jun", item.Value);
                        break;
                    case "7":
                        monthcount1.Add("Jul", item.Value);
                        break;
                    case "8":
                        monthcount1.Add("Aug", item.Value);
                        break;
                    case "9":
                        monthcount1.Add("Sep", item.Value);
                        break;
                    case "10":
                        monthcount1.Add("Oct", item.Value);
                        break;
                    case "11":
                        monthcount1.Add("Nov", item.Value);
                        break;
                    case "12":
                        monthcount1.Add("Dec", item.Value);
                        break;
                }
            }

            foreach (var item in monthcount1)
            {
                month.Add(item.Key);
                countmonth.Add(item.Value);
            }
            var jsonData = new
            {
                month = month,
                countmonth= countmonth
                
            };
            //var mA = monthcount;
            //ViewBag.ListMons = listMons;
            //ViewBag.MonsApp = monsAppoint.ToList();
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}