using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class AttendanceService : IClinicService<Attendance>
    {
        private readonly IClinicRepository<Attendance> _attendanceRepository;
        private ClinicContext db;

        public AttendanceService(IClinicRepository<Attendance> attendanceRepository, ClinicContext context)
        {
            _attendanceRepository = attendanceRepository;
            db = context;

        }
        public int CountRelation(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Attendance item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> Find(Func<Attendance, bool> predicate)
        {
            return _attendanceRepository.Find(predicate);

        }

        
        public Attendance Get(int id)
        {
           
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetAll()
        {
            return _attendanceRepository.GetWithInclude(p => p.Patient);
        }

        public IEnumerable<Attendance> GetAllWithInclude()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Attendance entity, int id)
        {
            _attendanceRepository.SaveOrUpdate(entity, id);
            
        }

        public void Update(Attendance item)
        {
            throw new NotImplementedException();
        }
    }
}