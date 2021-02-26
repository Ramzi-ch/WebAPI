using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DataMapping.Models;
using Infrastruture.Repository.Interfaces;
using WebAPI.Controllers.BaseController;

namespace WebAPI.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IGenericRepository<Department> depRepository) : base(depRepository)
        {
        }

    }
}