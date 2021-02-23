using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataMapping.DataContext;
using Infrastruture.UnitOfWork.Interfaces;

namespace Infrastruture.UnitOfWork.Classes
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        public readonly DataDbContext _DataContext;

        public GenericUnitOfWork(DataDbContext context)
        {
            _DataContext = context;
        }
        public void Dispose()
        {
            _DataContext.Dispose();
        }

        public async Task Commit()
        {
            await _DataContext.SaveChangesAsync();
        }
    }
}
