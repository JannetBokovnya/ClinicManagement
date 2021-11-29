using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Repositories
{
    public interface IClinicRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();//получить коллекцию из всех объектов
        T Get(int id);//получить объект по индексу
        //PagedCollection<T> GetNumberItems(Func<T, bool> predicate, int page = 1, int pageSize = 30);//получить коллекцию pageSize объектов расположенных на page странице и удовлетворяющих заданному условияю
        IEnumerable<T> Find(Func<T, bool> predicate);//получить коллекцию объектов, удовлетворяющих заданному условию
        void SaveOrUpdate(T entity, int id);
        void Create(T item);//создать объект
        void Delete(int id);//удалить объект
        void Update(T item);//обновить объект
        void Save(); // сохранение сделанных изменений

        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);

        T GetWithIncludeSingl(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);



        //List<T> GetAll();

        //T Find(int entityId);

        //T SaveOrUpdate(T entity);

        //T Add(T entity);

        //T Update(T entity);

        //void Delete(T entity);

        //// возвращает список ошибок
        //DbEntityValidationResult Validate(T entity);

        //// возвращает строку с ошибками
        //string ValidateAndReturnErrorString(T entity, out bool isValid);
    }
    
}
