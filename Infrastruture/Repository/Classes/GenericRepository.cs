using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var data=_unitOfWork.Set<T>().Add(entity);
            data.State = EntityState.Added;
            _unitOfWork.Commit();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var data = _unitOfWork.Set<T>().Attach(entity);
            data.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var data = _unitOfWork.Set<T>().Remove(entity);
            data.State = EntityState.Deleted;
            _unitOfWork.Commit();
        }
        
        public IEnumerable<TResult> GetWithSpeceficColumns<TResult>(Expression<Func<T, TResult>> objectToSave) where TResult : class
        {
            return _unitOfWork.Set<T>().Select(objectToSave);
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