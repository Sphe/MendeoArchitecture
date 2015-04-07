using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading;

namespace Common.Data.Infrastructure
{
    public class RepositoryBase<T, D> : IRepository<T>
            where T : class
            where D : DbContext, new()
    {
        private D dataContext;
        private readonly IDbSet<T> dbset;
        private readonly object syncObject = new object();

        public RepositoryBase(IDatabaseFactory<D> databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory<D> DatabaseFactory
        {
            get;
            private set;
        }

        protected virtual D DataContext
        {
            get 
            {
                if (dataContext == null)
                {
                    lock (syncObject)
                    {
                        if (dataContext == null)
                        {
                            dataContext = DatabaseFactory.Get();
                        }
                    }
                }

                return dataContext;
            }
        }

        protected IDbSet<T> DbSet
        {
            get
            {
                return dbset;
            }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(T entity, Expression<Func<T, object>> expId)
        {
            var funcId = expId.Compile();
            var valueId = funcId(entity);

            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = dataContext.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                T attachedEntity = dbset.Find(valueId);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = dataContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(Guid id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
