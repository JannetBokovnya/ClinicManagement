using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Repositories;
using ClinicManagement.Web.ViewModel;
using Itenso.TimePeriod;

namespace ClinicManagement.Web.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IClinicRepository<Appointment> _appointmentRepository;
        private ClinicContext db;

        public AppointmentService(IClinicRepository<Appointment> appointmentRepository, ClinicContext context)
        {
            _appointmentRepository = appointmentRepository;
            db = context;

        }


        public void Add(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }
        public int CountAppointments(int id)
        {
            int result = 0;

            if (id == 0)
            {
                result = _appointmentRepository.GetAll().Count();
            }
            return result;
        }

        public Appointment GetAppointment(int id)
        {
            return _appointmentRepository.Get(id);
        }

        public IEnumerable<Appointment> GetAppointmentByDoctor(int id)
        {
            //return Appointments
            //   .Where(d => d.DoctorId == id)
            //   .Include(p => p.Patient)
            //   .ToList();
            return _appointmentRepository.GetWithInclude(d => d.DoctorId == id, p => p.Patient);
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            //return Appointments
            //     .Include(p => p.Patient)
            //     .Include(d => d.Doctor)
            //     .ToList(); 
            return _appointmentRepository.GetWithInclude(p => p.Patient, d => d.Doctor);
        }

        public IEnumerable<Appointment> GetAppointmentWithPatient(int id)
        {
            //return Appointments
            //   .Where(p => p.PatientId == id)
            //   .Include(p => p.Patient)
            //   .Include(d => d.Doctor)
            //   .ToList();
            return _appointmentRepository.GetWithInclude(p => p.PatientId == id, p => p.Patient, d => d.Doctor);
        }

        public IEnumerable<Appointment> GetDaillyAppointments(DateTime? getDate, DateTime? getDate2)
        {
                        
            DateTime? getDate1_1 = DateTime.Today.Date;
            DateTime? getDate1_2 = DateTime.Today.Date;

            if (getDate !=null)
            {
                getDate1_1 = getDate;
            }

            if (getDate2 != null)
            {
                getDate1_2 = getDate2;
            }

            //allOrdersByDate = db.Orders.Where(a => a.Date >= dateFrom && a.Date <= dateTo)
            //return db.Appointments.Where(a => DbFunctions.DiffDays(a.Date, getDate) == 0)
            //   .Include(p => p.Patient)
            //   .Include(d => d.Doctor)
            //   .ToList();


            return db.Appointments.Where(a => DbFunctions.TruncateTime(a.Date) >= getDate1_1 && DbFunctions.TruncateTime(a.Date) <= getDate1_2)
               .Include(p => p.Patient)
               .Include(d => d.Doctor)
               .ToList();
        }

        public IEnumerable<Appointment> GetTodaysAppointments(int id)
        {
            DateTime today = DateTime.Now.Date;

            return _appointmentRepository.GetWithInclude(d => d.DoctorId == id && d.Date >= today,  p => p.Patient);
            //return _context.Appointments
            //    .Where(d => d.DoctorId == id && d.StartDateTime >= today)
            //    .Include(p => p.Patient)
            //    .OrderBy(d => d.StartDateTime)
            //    .ToList();
           
        }

        public IQueryable<Appointment> FilterAppointments(AppointmentSearchVM searchModel)
        {
            var result = db.Appointments.Include(p => p.Patient).Include(d => d.Doctor).AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.Doctor))
                    if (Int32.TryParse(searchModel.Doctor, out int doctorId))
                        result = result.Where(a => a.Doctor.Id == doctorId);

                if (!string.IsNullOrWhiteSpace(searchModel.Option))
                {
                    if (searchModel.Option == "ThisMonth")
                    {
                        result = result.Where(x => x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month);
                    }
                    else if (searchModel.Option == "Pending")
                    {
                        result = result.Where(x => x.Status == false);
                    }
                    else if (searchModel.Option == "Approved")
                    {
                        result = result.Where(x => x.Status);
                    }
                }
            }

            return result;
        }

        public IEnumerable<Appointment> GetUpcommingAppointments(string userId)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAppointment(DateTime appntDate, int id)
        {
            throw new NotImplementedException();
        }

        public bool IsInWorkingHours(DateTime start, DateTime end)
        {
            // check Not Saturday or Sunday
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            TimeRange workingHours = new TimeRange(TimeTrim.Hour(start, int.Parse(db.Administrations.Find(2).Value)), TimeTrim.Hour(start, int.Parse(db.Administrations.Find(3).Value)));
            return workingHours.HasInside(new TimeRange(start, end));
        }
        public bool IsInWorkingHours(TimeBlock block)
        {
            // check Not Saturday or Sunday
            if (block.Start.DayOfWeek == DayOfWeek.Saturday || block.Start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            TimeRange workingHours = new TimeRange(TimeTrim.Hour(block.Start.Date, int.Parse(db.Administrations.Find(2).Value)), TimeTrim.Hour(block.Start.Date, int.Parse(db.Administrations.Find(3).Value)));
            return workingHours.HasInside(block);
        }

        public int GetFields(int n)
        {
            return int.Parse(db.Administrations.Find(n).Value);
        }
        public List<SelectListItem> AvailableAppointments(int docID, DateTime date)
        {
            
            int A, S, E;
            A = int.Parse(db.Administrations.Find(1).Value);
            S = int.Parse(db.Administrations.Find(2).Value);
            E = int.Parse(db.Administrations.Find(3).Value);
            TimeBlock timeBlock = new MyTimeBlockExtention
                (
                new DateTime(date.Year, date.Month, date.Day, S, 0, 0),
                new TimeSpan(0, A, 0)
                );
            List<SelectListItem> ItemsList = new List<SelectListItem>();
            while (timeBlock.Start.CompareTo(DateTime.Now) <= 0) // No Appointments for past!!
            {
                timeBlock.Move(new TimeSpan(0, A, 0));
                if (!IsInWorkingHours(timeBlock))
                    break;
            }

            //var appointments = from a in db.Appointments.Where(x => x.DoctorID == docID)
            //                   select a;
            var appointments = _appointmentRepository.GetWithInclude(d => d.DoctorId == docID);
            bool overlaps = false;
            while (IsInWorkingHours(timeBlock))
            {
                foreach (var appointment in appointments)
                {
                    TimeBlock BookedTimeBlock = new MyTimeBlockExtention
                (
                (appointment.Date.Date.Add(appointment.Time.TimeOfDay)),
                new TimeSpan(0, A, 0)
                );
                    if (BookedTimeBlock.OverlapsWith(timeBlock))
                    {
                        overlaps = true;
                    }
                }
                if (!overlaps)
                {
                    ItemsList.Add(new SelectListItem() { Text = timeBlock.ToString(), Value = timeBlock.Start.ToString("HH:mm") });

                }
                overlaps = false;
                timeBlock.Move(new TimeSpan(0, A, 0));
            }
            if (ItemsList.Count != 0)
            {
                return ItemsList;
            }
            ItemsList.Add(new SelectListItem() { Text = "Нет записи", Value = "DONT" });
            return ItemsList;
            
        }

        public void SaveOrUpdate(Appointment entity, int id)
        {
            var entry = db.Entry(entity);

            if (id == 0)
            {
                db.Set<Appointment>().Add(entity);
                db.SaveChanges();
            }
            else
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public string ValidateNoAppoinmentClash(AppointmentFormViewModel appointment)
        {
            var appointments = _appointmentRepository.GetWithInclude(d => d.DoctorId == appointment.Doctor);
            foreach (var item in appointments)
            {
                if (item.Time.ToShortTimeString() == appointment.Time.ToShortTimeString() && item.Date.ToShortDateString() == appointment.Date.ToShortDateString())
                {
                    string errorMessage = String.Format(
                        "{0} already has an appointment on {1} on {2}.",
                        item.Doctor.FirstName,
                        item.Date.ToShortDateString(),
                        item.Time.ToShortTimeString());
                    return errorMessage;
                }
            }
            return String.Empty;
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> Find(Func<Appointment, bool> predicate)
        {
            return _appointmentRepository.Find(predicate);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}