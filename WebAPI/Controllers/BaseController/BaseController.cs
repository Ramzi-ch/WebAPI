using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastruture.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.BaseController
{
    [Route("api/base")]
    public class BaseController<T> : Controller, IBaseController<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public BaseController(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //[Authorize(Policy = "PublicSecure")] //identity server with separate project
        public virtual JsonResult Get()
        {
            return new JsonResult(_repository.GetAll());
        }

        [HttpGet]
        [Route("GetById/id")]
        public virtual JsonResult GetById(int id)
        {
            return new JsonResult(_repository.GetById(id));
        }

        [HttpPost]
        public virtual JsonResult Post(T objectToSave)
        {
            _repository.Add(objectToSave);
            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public virtual JsonResult Put(T objectToSave)
        {
            _repository.Update(objectToSave);
            return new JsonResult("Updated successfully");
        }

        [HttpDelete]
        public virtual JsonResult Delete(T objectToSave)
        {
            _repository.Delete(objectToSave);
            return new JsonResult("Deleted successfully");
        }


    }
}
