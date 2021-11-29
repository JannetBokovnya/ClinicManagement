using ClinicManagement.Web.Common;
using ClinicManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Linq.Expressions;

namespace ClinicManagement.Web.Repositories
{
    public class ClinicRepository<T> : IClinicRepository<T> where T : class
    {

        private ClinicContext db;
        //Конструктор класса
        public ClinicRepository(ClinicContext context)
        {
            db = context;
        }
        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            T item = db.Set<T>().Find(id);
            if (item != null)
            {
                db.Set<T>().Remove(item);
                db.SaveChanges();
            }
        }

        //Возвращает коллекцию объектов, удовлетворяющих заданному условию
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return db.Set<T>().Where(predicate).ToList();
        }

        //Возвращает один объект, выбранный по заданному ключу
        public T Get(int id)
        {
            return db.Set<T>().Find(id);
        }

        //Возвращает коллкцию из всех объектов 
        public IEnumerable<T> GetAll()
        {
            return db.Set<T>();
            
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(T entity, int id)
        {
                var entry = db.Entry(entity);

                if (id == 0)
                {
                    db.Set<T>().Add(entity);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
         }
        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~Repository() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }


        public T GetWithIncludeSingl(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.SingleOrDefault(predicate);
        }
        #endregion

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = db.Set<T>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

       
    }
}


//////////////////////////////////////////////////////////////////////////////
///
//private string GetPrimaryKey(DbEntityEntry<T> entry)
//{
//    var myObject = entry.Entity;
//    var property = myObject.GetType().GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));

//    return property.Name;

//}
//public void Update(T item)
//{
//    throw new NotImplementedException();
//}


//    public static void SaveOrUpdate
//(this ObjectContext context, TEntity entity)
//where TEntity : class
//    {
//        ObjectStateEntry stateEntry = null;
//        context.ObjectStateManager
//            .TryGetObjectStateEntry(entity, out stateEntry);

//        var objectSet = context.CreateObjectSet();
//        if (stateEntry == null || stateEntry.EntityKey.IsTemporary)
//        {
//            objectSet.AddObject(entity);
//        }

//        else if (stateEntry.State == EntityState.Detached)
//        {
//            objectSet.Attach(entity);
//            context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
//        }
//    }

//    public static ObjectContext GetObjectContext(this DbContext c)
//    {
//        return ((IObjectContextAdapter)c).ObjectContext;
//    }

//    public static void SaveOrUpdate
//        (this DbContext context, TEntity entity)
//        where TEntity : class
//    {
//        context.GetObjectContext().SaveOrUpdate(entity);
//    }




/// <summary>
/// ////////////////////////////////////////////////
/// </summary>
/// <param name="entry"></param>
/// <returns></returns>
//public static void AddOrUpdate(ClinicContext ctx, object entity)
//{
//    var entry = ctx.Entry(entity);
//    switch (entry.State)
//    {
//        case System.Data.Entity.EntityState.Detached:
//            ctx.Add(entity);
//            break;
//        case EntityState.Modified:
//            ctx.Update(entity);
//            break;
//        case EntityState.Added:
//            ctx.Add(entity);
//            break;
//        case EntityState.Unchanged:
//            //item already in db no need to do anything  
//            break;

//        default:
//            throw new ArgumentOutOfRangeException();
//    }
//}
//var myObject = entry.Entity;
//var property = myObject.GetType().GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
//string idName = property.Name;
//return idName = 0 ? Add(entity) : Update(entity);

//public static void AddOrUpdate(this DbContext ctx, object entity)
//{
//    var entry = ctx.Entry(entity);
//    switch (entry.State)
//    {
//        case EntityState.Detached:
//            ctx.Add(entity);
//            break;
//        case EntityState.Modified:
//            ctx.Update(entity);
//            break;
//        case EntityState.Added:
//            ctx.Add(entity);
//            break;
//        case EntityState.Unchanged:
//            //item already in db no need to do anything  
//            break;

//        default:
//            throw new ArgumentOutOfRangeException();
//    }
//}

//Student result = null;

//using (var schoolContext = new SchoolContext())
//{
//    result = schoolContext.Students.Add(student);
//    await schoolContext.SaveChangesAsync();
//}

//return result;

//var iDbEntity = entity as IDbEntity;

//if (iDbEntity == null)
//    throw new ArgumentException("entity should be IDbEntity type", "entity");

//return iDbEntity.GetPrimaryKey() == 0 ? Add(entity) : Update(entity);