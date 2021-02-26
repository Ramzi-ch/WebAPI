using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DataMapping.Models;
using Infrastruture.Repository.Interfaces;
using WebAPI.Controllers.BaseController;

namespace WebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IGenericRepository<Employee> _empRepository;

        public EmployeeController(IWebHostEnvironment env,
            IGenericRepository<Employee> empRepository) : base(empRepository)
        {
            _env = env;
            _empRepository = empRepository;
        }

        [HttpGet]
        [Route("GetAllEmployeeNames")]
        public JsonResult GetAllEmployeeNames()
        {
            return new JsonResult(_empRepository.GetWithSpeceficColumns(x => new { x.EmployeeName, x.DateOfJoining }));
        }

        [HttpPost]
        [Route("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                using (FileStream stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}