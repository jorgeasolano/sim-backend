using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ShippingInstructions")]
    public class ShippingInstructionsController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.ShippingInstructionService _cs;


        public ShippingInstructionsController(APIContext context, Services.ShippingInstructionService s)
        {
            _context = context;
           _cs = s;
        }

        // GET: api/ShippingInstructions
        [HttpGet]
        public  APIResponse<List<Dictionary<string, object>>> GetShippingInstructions(string filtro,DateTime desde, DateTime hasta, long Id)
        {
            
            Func<ServiceResponse<List<Dictionary<string, object>>>>  f = delegate {

                return _cs.GetShippingInstructions(desde,hasta, Id, GetAuthenticatedUsuarioID);

            };
            return this.CastToAPIResponse(f);

        }

        // GET: api/ShippingInstructions/5
        [HttpGet("{id}")]
        public APIResponse<ShippingInstruction> GetShippingInstruction([FromRoute] long id)
        {

            Func<ServiceResponse<ShippingInstruction>> f = delegate {

              
                return _cs.GetShippingInstruction(id);

            };

            return this.CastToAPIResponse(f);
            
     
        }

        // PUT: api/ShippingInstructions/5
        [HttpPut("{id}")]
        public APIResponse<ShippingInstruction> PutShippingInstruction([FromRoute] string id, [FromBody] ShippingInstruction ShippingInstruction)
        {
                Func<ServiceResponse<ShippingInstruction>> f = delegate {

                    return this._cs.UpdateShippingInstruction(ShippingInstruction);
                };
            return this.CastToAPIResponse(f);
        
        }

        // POST: api/ShippingInstructions
        [HttpPost]
        public APIResponse<ShippingInstruction> PostShippingInstruction([FromBody] ShippingInstruction  ShippingInstruction)
        {
            Func<ServiceResponse<ShippingInstruction>> f = delegate {

                return this._cs.CreateShippingInstruction(ShippingInstruction);
            };
            return this.CastToAPIResponse(f);
        }

        // DELETE: api/ShippingInstructions/5
        [HttpDelete("{id}")]
        public APIResponse<bool> DeleteShippingInstruction([FromRoute] long id)
        {

            Func<ServiceResponse<bool>> f = delegate {
                var c = new ShippingInstruction { Id = id };

                return this._cs.DeleteShippingInstruction(c);
            };
            return this.CastToAPIResponse(f);

        }




        [HttpGet("DownloadSI")]
        public FileContentResult DownloadSI(DateTime desde, DateTime hasta, long Id)
        {
            Func<ServiceResponse<string>> f = delegate
            {
                return this._cs.DownloadShippinInstructions(desde, hasta, Id, GetAuthenticatedUsuarioID);
            };
            var result = f.Invoke();

            var html =  Encoding.ASCII.GetBytes(result.Result);
             return File(html, "application/vnd.ms-excel", "shippingInstructions_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        }

    }
}