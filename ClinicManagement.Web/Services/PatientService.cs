using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Web.Services
{
    public class PatientService : IClinicService<Patient>
    {
        private readonly IClinicRepository<Patient> _patientRepository;
        private ClinicContext db;

        public PatientService(IClinicRepository<Patient> patientRepository, ClinicContext context)
        {
            _patientRepository = patientRepository;
            db = context;
        }
        public int CountRelation(int id)
        {
            int result = 0;

            if (id == 0)
            {
                result = _patientRepository.GetAll().Count();
            }
            return result;
        }

        public void Create(Patient item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _patientRepository.Delete(id);
        }

        public IEnumerable<Patient> Find(Func<Patient, bool> predicate)
        {
            return _patientRepository.Find(predicate);
        }

        public Patient Get(int id)
        {
            return _patientRepository.GetWithIncludeSingl(d => d.Id == id, c => c.City);
        }

        public IEnumerable<Patient> GetAll()
        {
            //return _context.Patients.Include(c => c.Cities);
            // var patients = _patientRepository.GetWithInclude(c => c.City);

            var patients = _patientRepository.GetAll();
            return patients;
        }

        public IEnumerable<Patient> GetAllWithInclude()
        {
            var patients = _patientRepository.GetWithInclude(c => c.City);
            return patients;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Patient entity, int id)
        {
            _patientRepository.SaveOrUpdate(entity, id);
        }

        public void Update(Patient item)
        {
            throw new NotImplementedException();
        }
    }
}