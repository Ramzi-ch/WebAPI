using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMapping.DataContext;
using Infrastruture.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repository.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataDbContext _DataContext;
        private readonly DbSet<T> _DbSet;

        public GenericRepository(DataDbContext context)
        {
            _DataContext = context;
            _DbSet = _DataContext.Set<T>();

        }
        public IEnumerable<T> GetAll()
        {
            return _DbSet.ToList();
        }

        public T GetById(int id)
        {
            return _DbSet.Find(id);
        }

        public void Add(T entity)
        {
            _DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _DbSet.Attach(entity);
            _DataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _DbSet.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  // Violates rule
        }

        protected virtual void Dispose(bool isdisposing)
        {
            if (isdisposing)
            {
               //_unitOfWork.Dispose();
            }
        }
    }
}
