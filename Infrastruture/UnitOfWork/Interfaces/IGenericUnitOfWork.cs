using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.UnitOfWork.Interfaces
{
    public interface IGenericUnitOfWork : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        Task Commit();
        void BeginTransaction();
        void CommitTransaction();
    }
}
