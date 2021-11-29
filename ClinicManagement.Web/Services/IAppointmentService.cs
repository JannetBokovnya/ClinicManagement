using ClinicManagement.Web.Models;
using ClinicManagement.Web.ViewModel;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicManagement.Web.Services
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();//получить коллекцию из всех объектов
        IEnumerable<Appointment> GetAppointments();
        IEnumerable<Appointment> GetAppointmentWithPatient(int id);
        IEnumerable<Appointment> GetAppointmentByDoctor(int id);
        IEnumerable<Appointment> GetTodaysAppointments(int id);
        IEnumerable<Appointment> GetUpcommingAppointments(string userId);
        IEnumerable<Appointment> GetDaillyAppointments(DateTime? getDate, DateTime? getDate2);
        IQueryable<Appointment> FilterAppointments(AppointmentSearchVM searchModel);
        bool ValidateAppointment(DateTime appntDate, int id);
        int CountAppointments(int id);
        Appointment GetAppointment(int id);
        void Add(Appointment appointment);
        void SaveOrUpdate(Appointment entity, int id);// сохранение сделанных изменений
        List<SelectListItem> AvailableAppointments(int docID, DateTime date);

        bool IsInWorkingHours(TimeBlock block);
        bool IsInWorkingHours(DateTime start, DateTime end);

        int GetFields(int n);
        string ValidateNoAppoinmentClash(AppointmentFormViewModel appointment);

        IEnumerable<Appointment> Find(Func<Appointment, bool> predicate);//получить коллекцию объектов, удовлетворяющих заданному условию
    }
}
