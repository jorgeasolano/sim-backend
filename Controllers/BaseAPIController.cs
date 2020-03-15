using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using WebAPI.Services;

namespace WebAPI.Controllers
{

    public class BaseAPIController : ControllerBase
    {

        public string GetAuthenticatedUsuarioID
        {
            get
            {

                var i = Request.HttpContext.User;
                var id = i.Claims.Where(c => c.Type == "Usuario").FirstOrDefault().Value;
                return id;

            }
        }



        protected Models.APIResponse<TResult> CastToAPIResponse<TResult>(Func<ServiceResponse<TResult>> func)
        {

            var sr = new ServiceResponse<TResult>();

            sr = func.Invoke();

            var rm = new Models.APIResponse<TResult>();
            rm.data = sr.Result;
            rm.HasError = sr.HasError;
            if (sr.HasError)
            {
                if (sr.Exception != null) rm.ErrorMessage = sr.Exception.Message;
                rm.code = -1;
            }
            else
            {
                rm.code = 0;
            }

            return rm;


        }


    }
}