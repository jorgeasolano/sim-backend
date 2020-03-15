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
    [Route("api/Searcher")]
    public class SearcherController : BaseAPIController
    {
        private readonly APIContext _context;
        private readonly Services.SearcherService _s;


        public SearcherController(APIContext context, Services.SearcherService s)
        {
            _context = context;
            _s = s;

        }

        // GET: api/Searcher
        [HttpGet]
        public APIResponse<List<Dictionary<string, object>>> GetUsuarios(string Filter, string TableName)
        {

            Func<ServiceResponse<List<Dictionary<string, object>>>> f = delegate
            {

                return _s.searcher(Filter, TableName);

            };
            return this.CastToAPIResponse(f);

        }
    }
}