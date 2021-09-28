using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
//using Newtonsoft.Json;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.JWT;
using WebAPI.Security;
using Microsoft.Extensions.Configuration;
namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class TestController : BaseAPIController
    {

        private readonly Services.UsuarioService _us;
        public IConfiguration Configuration { get; private set; }
        public TestController(UsuarioService us, IConfiguration configuration)
        {
            _us = us;
            this.Configuration = configuration;

        }

        [AllowAnonymous]
        [Route("env")]
        [HttpGet]
        public IActionResult getenv()
        {

            var ENV = this.Configuration["APP_ENV"].ToString();
            var PORT = this.Configuration["Kestrel:EndPoints:Http:Url"].ToString();
            return Ok(new { env = ENV, port = PORT });

        }

        [AllowAnonymous]
        [Route("encrypt/{pass}")]
        [HttpGet]
        public IActionResult encrypt(string pass)
        {
            string e = Encryptor.EncryptString(pass);
            var c = new { pass = e };
            return Ok(c);
        }

        [AllowAnonymous]
        [Route("get-token")]
        [HttpGet]
        public IActionResult gettoken()
        {
            var x = new JWT.JWTHelper();
            string r = x.GenerateToken("jorge", true);

            return Ok(r);

        }


        [Route("info")]
        [HttpGet]

        public IActionResult info()
        {
            return Ok("this is" + User.Identity.ToString());
        }
        [Authorize(Roles = "Admin")]
        [Route("infoforadmins")]
        [HttpGet]
        public IActionResult infoforadmins()
        {
            return Ok("this is" + User.Identity.ToString());
        }



    }
}