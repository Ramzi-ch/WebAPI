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

namespace WebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IGenericRepository<Employee> _empRepository;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env, IGenericRepository<Employee> empRepository)
        {
            _configuration = configuration;
            _env = env;
            _empRepository = empRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_empRepository.GetAll());
        }

        [HttpGet]
        [Route("GetEmployeeById/id")]
        public JsonResult GetEmployeeById(int id)
        {
            return new JsonResult(_empRepository.GetById(id));
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = $"insert into dbo.Employee (EmployeeName,Department,DateOfJoining,PhotoFileName)" +
                           $"values ('{emp.EmployeeName}', '{emp.Department}','{emp.DateOfJoining}','{emp.PhotoFileName}')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = $"update dbo.Employee set EmployeeName = '{emp.EmployeeName}', Department = '{emp.Department}'," +
                           $"DateOfJoining = '{emp.DateOfJoining}', PhotoFileName = '{emp.PhotoFileName}'" +
                           $"where EmployeeId = {emp.EmployeeId}";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfully");
        }

        [HttpDelete, Route("{id}")]
        public JsonResult Delete(int id)
        {
            string query = $"delete from dbo.Employee where EmployeeId = {id}";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted successfully");
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
            catch (Exception e)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = $"select DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        
    }
}