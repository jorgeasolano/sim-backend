using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers

{
    [Route("ReporteSI")]
    public class ReporteSIController : Controller
    {
        private ShippingInstructionService _shippingInstructionService;

        public ReporteSIController(
                    ShippingInstructionService srv
            )
        {


            _shippingInstructionService = srv;
        }


        [AllowAnonymous]
        [Route("{id}/{tkn}")]
        public IActionResult Index(int id, string tkn)
        {

            var si = this._shippingInstructionService.GetShippingInstruction(id);
            if (si.Result.PrintToken == tkn)
            {

                return View(si.Result);

            }
            else
            {
                return View(null);
            }

        }
    }
}