using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Web.Services
{
    public interface IClinicService<T> where T : class
    {
        IEnumerable<T> GetAll();//получить коллекцию из всех объектов
        IEnumerable<T> GetAllWithInclude();//получить коллекцию из всех объектов
        T Get(int id);//получить объект по индексу
        //PagedCollection<T> GetNumberItems(Func<T, bool> predicate, int page = 1, int pageSize = 30);//получить коллекцию pageSize объектов расположенных на page странице и удовлетворяющих заданному условияю
        IEnumerable<T> Find(Func<T, bool> predicate);//получить коллекцию объектов, удовлетворяющих заданному условию
        void SaveOrUpdate(T entity, int id);// сохранение сделанных изменений
        void Create(T item);//создать объект
        void Delete(int id);//удалить объект
        void Update(T item);//обновить объект
        void Save(); // сохранение сделанных изменений

        int CountRelation(int id);//количество связей с таблицами

        //IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        //T GetWithIncludeSingl(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
        //IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
        //Если у нас навигационные свойства помечены как виртуальные, то с помощью Lazy Loading связанные данные 
        //    автоматически будут подгружаться к загруженным сущностям.



    }
}
