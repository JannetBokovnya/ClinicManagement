using ClinicManagement.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ClinicManagement.Web.Services
{
    public class ClinicService<T> : IClinicService<T> where T : class
    {
        private readonly IClinicRepository<T> _clinicRepository;

        public ClinicService(IClinicRepository<T> clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public int CountRelation(int id)
        {
            throw new NotImplementedException();
        }
        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            //return _clinicRepository.Find(predicate);
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            // return _clinicRepository.Get(id);
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            //return _clinicRepository.GetAll();
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(T entity, int id)
        {
            //_clinicRepository.SaveOrUpdate(entity, id);
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}