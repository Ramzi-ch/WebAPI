using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.BaseController
{
    public interface IBaseController<T> where T : class
    {
        JsonResult Get();
        JsonResult GetById(int id);
        JsonResult Post(T objectToSave);
        JsonResult Put(T objectToSave);
        JsonResult Delete(T objectToSave);


    }
}
