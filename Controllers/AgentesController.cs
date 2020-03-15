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
    [Route("api/Agentes")]
    public class AgentesController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.AgenteService _as;


        public AgentesController(APIContext context, Services.AgenteService s)
        {
            _context = context;
           _as = s;
        }

        // GET: api/Agentes
        [HttpGet]
        public  APIResponse<List<Agente>> GetAgentes(string filtro)
        {
            
            Func<ServiceResponse<List<Agente>>> f = delegate {

                return _as.GetAgentes(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Agentes/5
        [HttpGet("{id}")]
        public APIResponse<Agente> GetAgente([FromRoute] string id)
        {

            Func<ServiceResponse<Agente>> f = delegate {

                return _as.GetAgente(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Agentes/5
        [HttpPut("{id}")]
        public APIResponse<Agente> PutAgente([FromRoute] string id, [FromBody] Agente Agente)
        {
                Func<ServiceResponse<Agente>> f = delegate {

                    return this._as.UpdateAgente(Agente);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Agentes
        [HttpPost]
        public APIResponse<Agente> PostAgente([FromBody] Agente Agente)
        {
            Func<ServiceResponse<Agente>> f = delegate {

                return this._as.CreateAgente(Agente);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Agentes/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteAgente([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var u = new Agente { Id = id };

                return this._as.DeleteAgente(u);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}