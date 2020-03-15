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
    [Route("api/Clientes")]
    public class ClientesController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.ClienteService _us;


        public ClientesController(APIContext context, Services.ClienteService us)
        {
            _context = context;
           _us = us;
        }

        // GET: api/Clientes
        [HttpGet]
        public  APIResponse<List<Cliente>> GetClientes(string filtro)
        {
            
            Func<ServiceResponse<List<Cliente>>> f = delegate {

                return _us.GetClientes(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public APIResponse<Cliente> GetCliente([FromRoute] string id)
        {

            Func<ServiceResponse<Cliente>> f = delegate {

                return _us.GetCliente(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public APIResponse<Cliente> PutCliente([FromRoute] string id, [FromBody] Cliente Cliente)
        {
                Func<ServiceResponse<Cliente>> f = delegate {

                    return this._us.UpdateCliente(Cliente);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Clientes
        [HttpPost]
        public APIResponse<Cliente> PostCliente([FromBody] Cliente Cliente)
        {
            Func<ServiceResponse<Cliente>> f = delegate {

                return this._us.CreateCliente(Cliente);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteCliente([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var u = new Cliente { Id = id };

                return this._us.DeleteCliente(u);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}