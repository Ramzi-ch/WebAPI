﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataMapping.DataContext;
using Infrastruture.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.UnitOfWork.Classes
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private readonly DataDbContext _dataContext;

        public GenericUnitOfWork(DataDbContext context)
        {
            _dataContext = context;
        }
        
        public DbSet<T> Set<T>() where T : class
        {
            return _dataContext.Set<T>();
        }
        public async Task Commit()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _dataContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dataContext.Database.CommitTransaction();
        }

        #region IDisposable

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        #endregion
    }
}
