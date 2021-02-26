using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataMapping.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.UnitOfWork.Interfaces
{
    public interface IGenericUnitOfWork : IDisposable
    {
        DataDbContext GetContext();
        DbSet<T> Set<T>() where T : class;
        void Commit();
        Task CommitAsync();
        void BeginTransaction();
        void CommitTransaction();
    }
}
