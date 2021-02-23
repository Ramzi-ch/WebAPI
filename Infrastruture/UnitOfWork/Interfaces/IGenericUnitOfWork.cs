using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.UnitOfWork.Interfaces
{
    public interface IGenericUnitOfWork : IDisposable
    {
        Task Commit();
    }
}
