using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ClinicManagement.Web.Services
{
    public class DoctorService : IClinicService<Doctor>
    {

        private readonly IClinicRepository<Doctor> _doctorRepository;
        private ClinicContext db;


        public DoctorService(IClinicRepository<Doctor> doctorRepository, ClinicContext context)
        {
            _doctorRepository = doctorRepository;
            db = context;

        }
        public int CountRelation(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Doctor item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> Find(Func<Doctor, bool> predicate)
        {
            return _doctorRepository.Find(predicate);
        }

        public Doctor Get(int id)
        {
            //https://metanit.com/sharp/entityframework/3.13.php
            //return _context.Doctors
            //   .Include(s => s.Specialization)
            //   .Include(u => u.Physician)
            //   .SingleOrDefault(d => d.Id == id);
            return _doctorRepository.GetWithIncludeSingl(d => d.Id == id, s => s.Specialization, u => u.Physician);
            
        }

        public IEnumerable<Doctor> GetAll()
        {
            //return _doctorRepository.GetWithInclude(s => s.Specialization, u => u.Physician);
            return _doctorRepository.GetAll();
        }

       
        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Doctor entity, int id)
        {
            _doctorRepository.SaveOrUpdate(entity, id);
        }

        public void Update(Doctor item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAllWithInclude()
        {
            return _doctorRepository.GetWithInclude(s => s.Specialization, u => u.Physician);
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