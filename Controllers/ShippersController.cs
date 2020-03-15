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
    [Route("api/Shippers")]
    public class ShippersController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.ShipperService _cs;


        public ShippersController(APIContext context, Services.ShipperService s)
        {
            _context = context;
           _cs = s;
        }

        // GET: api/Shippers
        [HttpGet]
        public  APIResponse<List<Shipper>> GetShippers(string filtro)
        {
            
            Func<ServiceResponse<List<Shipper>>> f = delegate {

                return _cs.GetShippers(filtro);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/Shippers/5
        [HttpGet("{id}")]
        public APIResponse<Shipper> GetShipper([FromRoute] string id)
        {

            Func<ServiceResponse<Shipper>> f = delegate {

                return _cs.GetShipper(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/Shippers/5
        [HttpPut("{id}")]
        public APIResponse<Shipper> PutShipper([FromRoute] string id, [FromBody] Shipper Shipper)
        {
                Func<ServiceResponse<Shipper>> f = delegate {

                    return this._cs.UpdateShipper(Shipper);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/Shippers
        [HttpPost]
        public APIResponse<Shipper> PostShipper([FromBody] Shipper Shipper)
        {
            Func<ServiceResponse<Shipper>> f = delegate {

                return this._cs.CreateShipper(Shipper);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/Shippers/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteShipper([FromRoute] string id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var c = new Shipper { Id = id };

                return this._cs.DeleteShipper(c);
            };
            return this.CastToAPIResponse(f);

        }

       
    }
}