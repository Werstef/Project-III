using KristMedv2.ApplicationLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly KristMedv2DbContext dbContext;

        public BaseRepository(KristMedv2DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Add(T itemToAdd)
        {
            var entity = dbContext.Set<T>().Add(itemToAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }

        public T GetById(Guid Id)
        {
            return dbContext.Set<T>()
                            .Find(Id);
        }

        public bool Delete(T itemToDelete)
        {
            dbContext.Remove<T>(itemToDelete);
            dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>()
                           .AsEnumerable();
        }

        public T Update(T itemToUpdate)
        {
            var entity = dbContext.Update<T>(itemToUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
    }
}
