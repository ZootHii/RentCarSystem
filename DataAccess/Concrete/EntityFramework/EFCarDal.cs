using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarDal : ICarDal
    {
        public void Add(Car entity)
        {
            using (var context = new CarRentalContext())
            {
                var entityAdded = context.Entry(entity);
                entityAdded.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Car entity)
        {
            using (var context = new CarRentalContext())
            {
                var entityDeleted = context.Entry(entity);
                entityDeleted.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Delete(Car entity)
        {
            using (var context = new CarRentalContext())
            {
                var entityUpdated = context.Entry(entity);
                entityUpdated.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (var context = new CarRentalContext())
            {
                return filter == null
                    ? context.Set<Car>().ToList()
                    : context.Set<Car>().Where(filter).ToList();
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (var context = new CarRentalContext())
            {
                return context.Set<Car>().SingleOrDefault(filter);
            }
        }
    }
}