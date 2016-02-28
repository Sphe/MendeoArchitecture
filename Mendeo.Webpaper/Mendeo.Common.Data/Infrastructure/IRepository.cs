using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;

namespace Common.Data.Infrastructure
{
    public interface IRepository<T> 
            where T : class
    {
        DbContext UnderlyingDbContext { get; }
        IDbSet<T> DbSet { get; }
        void Add(T entity);
        void Update(T entity);
        void Update(T entity, Expression<Func<T, object>> expId);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int Id);
        T GetById(long Id);
        T GetById(string Id);
        T GetById(Guid Id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
