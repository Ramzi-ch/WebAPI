using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruture.Repository.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
