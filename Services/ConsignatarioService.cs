using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class ConsignatarioService : BaseService
    {


        public ConsignatarioService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<Consignatario> GetConsignatario(string id)
        {

            Func<Consignatario> func = delegate
            {

                var c = this._APIContext.Consignatarios.Find(id);
                return c;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Consignatario>> GetConsignatarios(string filter)
        {

            Func<List<Consignatario>> f = delegate
            {

                return this._APIContext.Consignatarios.Where(c => c.Nombre.Contains(filter)).ToList();


            };
            return this.Execute(f);
        }


        public ServiceResponse<Consignatario> CreateConsignatario(Consignatario c)
        {


            Func<Consignatario> f = delegate
            {

                var Consignatario_result = this._APIContext.Consignatarios.Find(c.Id);
                if (Consignatario_result != null)
                {

                    throw new Exception("Consignatario ya existe");
                }
                else
                {

                    _APIContext.Consignatarios.Add(c);
                    _APIContext.SaveChanges();
                    return c;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Consignatario> UpdateConsignatario(Consignatario c)
        {


            Func<Consignatario> f = delegate
            {



                var e = _APIContext.Entry(c);
                e.State = EntityState.Modified;

                _APIContext.SaveChanges();

                return c;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteConsignatario(Consignatario c)
        {


            Func<bool> f = delegate
            {

                var Consignatario_result = this._APIContext.Consignatarios.Remove(c);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        private bool ConsignatarioExists(string id)
        {
            return _APIContext.Consignatarios.Any(e => e.Id == id);
        }

    }
}
