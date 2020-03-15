using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class AgenteService : BaseService
    {



        public AgenteService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<Agente> GetAgente(string id)
        {

            Func<Agente> func = delegate
            {

                var a = this._APIContext.Agentes.Find(id);
                return a;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Agente>> GetAgentes(string filter)
        {

            Func<List<Agente>> f = delegate
            {

                return this._APIContext.Agentes.Where(a => a.Nombre.Contains(filter)).ToList();


            };
            return this.Execute(f);
        }


        public ServiceResponse<Agente> CreateAgente(Agente a)
        {


            Func<Agente> f = delegate
            {

                var Agente_result = this._APIContext.Agentes.Find(a.Id);
                if (Agente_result != null)
                {

                    throw new Exception("Agente ya existe");
                }
                else
                {

                    _APIContext.Agentes.Add(a);
                    _APIContext.SaveChanges();
                    return a;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Agente> UpdateAgente(Agente a)
        {


            Func<Agente> f = delegate
            {



                var e = _APIContext.Entry(a);
                e.State = EntityState.Modified;

                _APIContext.SaveChanges();

                return a;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteAgente(Agente a)
        {


            Func<bool> f = delegate
            {

                var Agente_result = this._APIContext.Agentes.Remove(a);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        private bool AgenteExists(string id)
        {
            return _APIContext.Agentes.Any(e => e.Id == id);
        }

    }
}
