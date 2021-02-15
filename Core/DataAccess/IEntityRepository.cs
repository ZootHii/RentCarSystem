using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public List<T> GetAll(Expression<Func<T, bool>> filter = null);
        public T Get(Expression<Func<T, bool>> filter);
    }
}