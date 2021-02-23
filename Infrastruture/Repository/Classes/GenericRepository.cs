using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataMapping.DataContext;
using Infrastruture.Repository.Interfaces;
using Infrastruture.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repository.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IGenericUnitOfWork _unitOfWork;

        public GenericRepository(IGenericUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<T> GetAll()
        {
            return _unitOfWork.Set<T>();
        }

        public T GetById(int id)
        {
            return _unitOfWork.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            _unitOfWork.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _unitOfWork.Set<T>().Attach(entity);
            //_DataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _unitOfWork.Set<T>().Remove(entity);
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  // Violates rule
        }

        protected virtual void Dispose(bool isdisposing)
        {
            if (isdisposing)
            {
                _unitOfWork.Dispose();
            }
        }

        #endregion

    }
}