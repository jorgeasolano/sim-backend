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
    [Route("api/Consignatarios")]
    public class ConsignatariosController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.ConsignatarioService _cs;


        public ConsignatariosController(APIContext context, Services.ConsignatarioService s)
        {
            _context = context;
           _cs = s;
        }

        // GET: api/Consignatarios
        [HttpGet]
        public  APIResponse<List<Consignatario>> GetConsignatarios(string filtro)
        {
            
            Func<ServiceResponse<List<Consignatario>>> f = delegate {

                return _cs.GetConsignatarios(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Consignatarios/5
        [HttpGet("{id}")]
        public APIResponse<Consignatario> GetConsignatario([FromRoute] string id)
        {

            Func<ServiceResponse<Consignatario>> f = delegate {

                return _cs.GetConsignatario(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Consignatarios/5
        [HttpPut("{id}")]
        public APIResponse<Consignatario> PutConsignatario([FromRoute] string id, [FromBody] Consignatario Consignatario)
        {
                Func<ServiceResponse<Consignatario>> f = delegate {

                    return this._cs.UpdateConsignatario(Consignatario);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Consignatarios
        [HttpPost]
        public APIResponse<Consignatario> PostConsignatario([FromBody] Consignatario Consignatario)
        {
            Func<ServiceResponse<Consignatario>> f = delegate {

                return this._cs.CreateConsignatario(Consignatario);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Consignatarios/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteConsignatario([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var c = new Consignatario { Id = id };

                return this._cs.DeleteConsignatario(c);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}