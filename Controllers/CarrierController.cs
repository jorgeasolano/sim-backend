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
    [Route("api/Carriers")]
    public class CarriersController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.CarrierService _cs;


        public CarriersController(APIContext context, Services.CarrierService s)
        {
            _context = context;
           _cs = s;
        }

        // GET: api/Carriers
        [HttpGet]
        public  APIResponse<List<Carrier>> GetCarriers(string filtro)
        {
            
            Func<ServiceResponse<List<Carrier>>> f = delegate {

                return _cs.GetCarriers(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Carriers/5
        [HttpGet("{id}")]
        public APIResponse<Carrier> GetCarrier([FromRoute] string id)
        {

            Func<ServiceResponse<Carrier>> f = delegate {

                return _cs.GetCarrier(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Carriers/5
        [HttpPut("{id}")]
        public APIResponse<Carrier> PutCarrier([FromRoute] string id, [FromBody] Carrier Carrier)
        {
                Func<ServiceResponse<Carrier>> f = delegate {

                    return this._cs.UpdateCarrier(Carrier);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Carriers
        [HttpPost]
        public APIResponse<Carrier> PostCarrier([FromBody] Carrier Carrier)
        {
            Func<ServiceResponse<Carrier>> f = delegate {

                return this._cs.CreateCarrier(Carrier);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Carriers/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteCarrier([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var c = new Carrier { Id = id };

                return this._cs.DeleteCarrier(c);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}