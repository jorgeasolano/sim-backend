using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using WebAPI.Models;
using WebAPI.Services;

using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Net.Http;
using WebAPI.JWT;

namespace WebAPI.Controllers
{


    #region "Security Models"

    public class LoginModel
    {

        public string Id { get; set; }
        public string Contrasena { get; set; }
        public Boolean EsAdmin { get; set; }
        public string token_access { get; set; }
        public int token_expiresIn { get; set; }
    }


    public class JWTModel
    {

        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    #endregion




    [Produces("application/json")]
    [Route("api/security")]
    public class SecurityController : BaseAPIController
    {

        private readonly UsuarioService _UsuarioService;


        public SecurityController(UsuarioService us)
        {
            this._UsuarioService = us;
        }





        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody]LoginModel model)
        {



            var sr = _UsuarioService.Login(model.Id, model.Contrasena);

            var gr = new APIResponse<LoginModel>();

            gr.data = new LoginModel();

            if (sr.HasError)
            {
                gr.code = -1;
                gr.ErrorMessage = sr.Exception.Message + " - Source: " + sr.Exception.Source + " -  St: " + sr.Exception.StackTrace;
                gr.data.token_access = "";

                return Ok(gr);
            }
            else
            {


                gr.code = 0;
                gr.data.Id = sr.Result.Id;
                gr.data.EsAdmin = sr.Result.EsAdmin;

                var JWTdata = new JWT.JWTHelper().GenerateToken(gr.data.Id, gr.data.EsAdmin);

                gr.data.token_access = JWTdata;


                return Ok(gr);


            }



        }


        [HttpPost]
        [Route("renewToken")]
        public IActionResult renewToken()
        {

            var i = Request.HttpContext.User;
            var id = i.Claims.Where(c => c.Type == "Usuario").FirstOrDefault().Value;


            var sr = this._UsuarioService.GetUsuario(id);

            var gr = new APIResponse<LoginModel>();

            if (sr.HasError)
            {
                gr.code = -1;
                gr.ErrorMessage = sr.Exception.Message;
                gr.data.token_access = "";

                return Ok(gr);
            }
            else
            {


                gr.code = 0;
                gr.data = new LoginModel();

                gr.data.Id = sr.Result.Id;
                gr.data.EsAdmin = sr.Result.EsAdmin;

                var JWTdata = new JWT.JWTHelper().GenerateToken(gr.data.Id, gr.data.EsAdmin);

                gr.data.token_access = JWTdata;


                return Ok(gr);


            }




        }



        [HttpPost]
        [Route("AmIAuthenticated")]
        public IActionResult AmIAuthenticated()
        {

            var i = Request.HttpContext.User;

            var id = i.Claims.Where(c => c.Type == "Usuario").FirstOrDefault().Value;


            var gr = new APIResponse<Usuario>();

            var u = this._UsuarioService.GetUsuario(id).Result;
            u.Contrasena = "";

            gr.code = 0;
            gr.data = u;

            return Ok(gr);




        }



    }




}