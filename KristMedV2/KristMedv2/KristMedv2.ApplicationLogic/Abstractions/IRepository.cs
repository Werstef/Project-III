using System;
using System.Collections.Generic;
using System.Text;

namespace KristMedv2.ApplicationLogic.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        T Add(T itemToAdd);
        T Update(T itemToUpdate);
        bool Delete(T itemToDelete);
    }
}
