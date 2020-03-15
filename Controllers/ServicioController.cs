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
    [Route("api/Servicios")]
    public class ServiciosController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.ServicioService _as;


        public ServiciosController(APIContext context, Services.ServicioService s)
        {
            _context = context;
           _as = s;
        }

        // GET: api/Servicios
        [HttpGet]
        public  APIResponse<List<Servicio>> GetServicios(string filtro)
        {
            
            Func<ServiceResponse<List<Servicio>>> f = delegate {

                return _as.GetServicios(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Servicios/5
        [HttpGet("{id}")]
        public APIResponse<Servicio> GetServicio([FromRoute] string id)
        {

            Func<ServiceResponse<Servicio>> f = delegate {

                return _as.GetServicio(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Servicios/5
        [HttpPut("{id}")]
        public APIResponse<Servicio> PutServicio([FromRoute] string id, [FromBody] Servicio Servicio)
        {
                Func<ServiceResponse<Servicio>> f = delegate {

                    return this._as.UpdateServicio(Servicio);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Servicios
        [HttpPost]
        public APIResponse<Servicio> PostServicio([FromBody] Servicio Servicio)
        {
            Func<ServiceResponse<Servicio>> f = delegate {

                return this._as.CreateServicio(Servicio);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteServicio([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var u = new Servicio { Id = id };

                return this._as.DeleteServicio(u);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}