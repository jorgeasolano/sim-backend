using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Usuarios")]
    public class UsuariosController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.UsuarioService _us;


        public UsuariosController(APIContext context, Services.UsuarioService us)
        {
            _context = context;
           _us = us;
        }

        // GET: api/Usuarios
        [HttpGet]
        public  APIResponse<List<Dictionary<string,object>>> GetUsuarios(string filtro)
        {
            
            Func<ServiceResponse<List<Dictionary<string, object>>>> f = delegate {

                return _us.GetUsuarios(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public APIResponse<Usuario> GetUsuario([FromRoute] string id)
        {

            Func<ServiceResponse<Usuario>> f = delegate {

                return _us.GetUsuario(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public APIResponse<Usuario> PutUsuario([FromRoute] string id, [FromBody] Usuario usuario)
        {
                Func<ServiceResponse<Usuario>> f = delegate {

                    return this._us.UpdateUsuario(usuario);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Usuarios
        [HttpPost]
        public APIResponse<Usuario> PostUsuario([FromBody] Usuario usuario)
        {
            Func<ServiceResponse<Usuario>> f = delegate {

                return this._us.CreateUsuario(usuario);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteUsuario([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var u = new Usuario { Id = id };

                return this._us.DeleteUsuario(u);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}