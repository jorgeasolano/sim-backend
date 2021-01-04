using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class ShippingInstructionService : BaseService
    {



        public ShippingInstructionService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<ShippingInstruction> GetShippingInstruction(long id)
        {

            Func<ShippingInstruction> func = delegate
            {



                var c = this._APIContext.ShippingInstructions
                 .Where(si => si.Id == id)
                .Include(i => i.Agente)
                .Include(i => i.Carrier)
                .Include(i => i.Cliente)
                .Include(i => i.Consignatario)
                .Include(i => i.PuertoDeDescarga)
                .Include(i => i.PuertoDeDespacho)
                .Include(i => i.Shipper)
                .Include(i => i.Usuario)
                .Include(i => i.Region)
                .Include(i => i.Commodity)
                .Include(i => i.ShippingInstructionDetails).ThenInclude(i => i.Servicio).FirstOrDefault();


                return c;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Dictionary<string, object>>> GetShippingInstructions(DateTime desde, DateTime hasta, long Id, string UsuarioId)
        {

            Func<List<Dictionary<string, object>>> f = delegate
             {

                 return this.ExecuteStoreProcedure(cmd =>
                 {

                     cmd.CommandText = "getShippingInstructions";
                     SetParameterCommand(cmd, "desde", desde);
                     SetParameterCommand(cmd, "hasta", hasta);
                     SetParameterCommand(cmd, "SIID", Id);
                     SetParameterCommand(cmd, "UsuarioID", UsuarioId);


                 });


             };
            return this.Execute(f);
        }


        public ServiceResponse<ShippingInstruction> CreateShippingInstruction(ShippingInstruction c)
        {


            Func<ShippingInstruction> f = delegate
            {

                long max = 1;

                try
                {
                    max = this._APIContext.ShippingInstructions.Where(si => si.Fecha.Year == DateTime.Now.Year).Max(c => c.Id);
                }
                catch (System.Exception)
                {
                    max = 1;
                    //throw;
                }



                if (max == 0)
                {
                    string tempID = DateTime.Now.Year.ToString().Substring(2, 2) + max.ToString("0000");
                    c.Id = long.Parse(tempID);
                }
                else
                {
                    max = max + 1;
                    string tempID = DateTime.Now.Year.ToString().Substring(2, 2) + max.ToString("0000");
                    c.Id = long.Parse(tempID);
                }



                var ShippingInstruction_result = this._APIContext.ShippingInstructions.Find(c.Id);
                if (ShippingInstruction_result != null)
                {

                    throw new Exception("ShippingInstruction ya existe");
                }
                else
                {



                    c.Cliente = null;
                    c.Agente = null;
                    c.Consignatario = null;
                    c.Shipper = null;
                    c.PuertoDeDespacho = null;
                    c.PuertoDeDescarga = null;
                    // c.DestinoFinal = null;
                    c.Region = null;
                    c.Carrier = null;
                    c.Usuario = null;
                    c.Commodity = null;
                    c.PrintToken = Guid.NewGuid().ToString();

                    if (c.LugarDeCarga == "") { c.LugarDeCarga = " "; };
                    if (c.Comentarios == "") { c.Comentarios = " "; };
                    if (c.Nominacion == "") { c.Nominacion = " "; };


                    foreach (var item in c.ShippingInstructionDetails)
                    {
                        item.Servicio = null;
                    }



                    _APIContext.ShippingInstructions.Add(c);
                    _APIContext.SaveChanges();
                    return c;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<ShippingInstruction> UpdateShippingInstruction(ShippingInstruction c)
        {
            c.PrintToken = c.PrintToken = Guid.NewGuid().ToString();



            Func<ShippingInstruction> f = delegate
            {



                var si = _APIContext.ShippingInstructions.Where(col => col.Id == c.Id).Include(i => i.ShippingInstructionDetails).SingleOrDefault();


                foreach (var item in si.ShippingInstructionDetails)
                {
                    _APIContext.Entry(item).State = EntityState.Deleted;
                }




                foreach (var SIDetail in c.ShippingInstructionDetails)
                {

                    if (SIDetail.Id != 0)
                    {
                        var DetailInDb = si.ShippingInstructionDetails.Single(e => e.Id == SIDetail.Id);
                        _APIContext.Entry(DetailInDb).CurrentValues.SetValues(SIDetail);
                        _APIContext.Entry(DetailInDb).State = EntityState.Modified;

                    }
                    else
                    {

                        SIDetail.Servicio = null;
                        si.ShippingInstructionDetails.Add(SIDetail);

                    }
                }



                _APIContext.Entry(si).CurrentValues.SetValues(c);
                _APIContext.Entry(si).State = EntityState.Modified;

                _APIContext.SaveChanges();

                return c;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteShippingInstruction(ShippingInstruction c)
        {


            Func<bool> f = delegate
            {

                var ShippingInstruction_result = this._APIContext.ShippingInstructions.Remove(c);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        public ServiceResponse<string> DownloadShippinInstructions(DateTime desde, DateTime hasta, long Id, string UsuarioId)
        {
            Func<List<Dictionary<string, object>>> f = delegate
            {

                return this.ExecuteStoreProcedure(cmd =>
                {

                    cmd.CommandText = "getShippingInstructionsXLS";
                    SetParameterCommand(cmd, "desde", desde);
                    SetParameterCommand(cmd, "hasta", hasta);
                    SetParameterCommand(cmd, "SIID", Id);
                    SetParameterCommand(cmd, "UsuarioID", UsuarioId);

                });


            };
            var result = this.Execute(f);


            StringBuilder html = new StringBuilder();

            html.Append("<html>");
            html.Append("<body>");
            html.Append("<table>");


            if (result.Result.Count >= 1)
            {

                html.Append("<tr>");

                foreach (var h in result.Result[0])
                {
                    html.Append("<th>" + h.Key + "</th>");
                }


                html.Append("</tr>");


            }



            foreach (var r in result.Result)
            {
                html.Append("<tr>");
                foreach (var c in r)
                {
                    html.Append("<td>" + c.Value.ToString() + "</td>");
                }
                html.Append("</tr>");

            }


            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");









            var sr = new ServiceResponse<string>();
            sr.Result = html.ToString();
            return sr;

        }


        private bool ShippingInstructionExists(long id)
        {
            return _APIContext.ShippingInstructions.Any(e => e.Id == id);
        }

    }
}
