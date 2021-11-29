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
    public class SpecializationService : IClinicService<Specialization>
    {
        private readonly IClinicRepository<Specialization> _specRepository;
        private ClinicContext db;

        public SpecializationService(IClinicRepository<Specialization> specRepository, ClinicContext context)
        {
            _specRepository = specRepository;
            db = context;

        }
        public int CountRelation(int id)
        {
            return db.Doctors.Count(x => x.SpecializationId == id);
        }

        public void Create(Specialization item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _specRepository.Delete(id);
        }

        public IEnumerable<Specialization> Find(Func<Specialization, bool> predicate)
        {
            return _specRepository.Find(predicate);
        }

        public Specialization Get(int id)
        {
            return _specRepository.Get(id);
        }

        public IEnumerable<Specialization> GetAll()
        {
            return _specRepository.GetAll();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Specialization entity, int id)
        {
            _specRepository.SaveOrUpdate(entity, id);
        }

        public void Update(Specialization item)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Specialization> GetAllWithInclude()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Specialization> GetWithInclude(params Expression<Func<Specialization, object>>[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}

        //public Specialization GetWithIncludeSingl(Func<Specialization, bool> predicate, params Expression<Func<Specialization, object>>[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}


        //public IEnumerable<Specialization> GetWithInclude(Func<Specialization, bool> predicate, params Expression<Func<Specialization, object>>[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}
    }
}