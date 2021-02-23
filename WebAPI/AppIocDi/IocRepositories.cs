using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMapping.Models;
using Infrastruture.Repository.Classes;
using Infrastruture.Repository.Interfaces;
using Infrastruture.UnitOfWork.Classes;
using Infrastruture.UnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.AppIocDi
{
    public static class IocRepositories
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericUnitOfWork, GenericUnitOfWork>();
            services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
        }
    }
}
