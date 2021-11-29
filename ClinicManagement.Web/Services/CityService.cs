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
    public class CityService : IClinicService<City>
    {
        private readonly IClinicRepository<City> _cityRepository;
        private ClinicContext db;


        public CityService(IClinicRepository<City> cityRepository, ClinicContext context)
        {
            _cityRepository = cityRepository;
            db = context;

        }
        public void Create(City item)
        {
            
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _cityRepository.Delete(id);
        }

        public IEnumerable<City> Find(Func<City, bool> predicate)
        {
            return _cityRepository.Find(predicate);
        }

        public City Get(int id)
        {
            return _cityRepository.Get(id);
        }

        public IEnumerable<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(City entity, int id)
        {
            _cityRepository.SaveOrUpdate(entity, id);
        }

        private void Json(object p, object allowGet)
        {
            throw new NotImplementedException();
        }

        public void Update(City item)
        {
            throw new NotImplementedException();
        }

        public int CountRelation(int id)
        {
            return db.Patients.Count(x => x.CityId == id);
            
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

        public IEnumerable<City> GetAllWithInclude()
        {
            throw new NotImplementedException();
        }


        //public IEnumerable<City> GetWithInclude(params Expression<Func<City, object>>[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}

        //public City GetWithIncludeSingl(Func<City, bool> predicate, params Expression<Func<City, object>>[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}


        //public IEnumerable<City> GetWithInclude(Func<City, bool> predicate,  params Expression<Func<City, object>>[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}
    }
}