using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class ServicioService : BaseService
    {



        public ServicioService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<Servicio> GetServicio(string id)
        {

            Func<Servicio> func = delegate
            {

                var a = this._APIContext.Servicios.Find(id);
                return a;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Servicio>> GetServicios(string filter)
        {

            Func<List<Servicio>> f = delegate
            {

                return this._APIContext.Servicios.Where(a => a.Nombre.Contains(filter)).ToList();


            };
            return this.Execute(f);
        }


        public ServiceResponse<Servicio> CreateServicio(Servicio a)
        {


            Func<Servicio> f = delegate
            {

                var Servicio_result = this._APIContext.Servicios.Find(a.Id);
                if (Servicio_result != null)
                {

                    throw new Exception("Servicio ya existe");
                }
                else
                {

                    _APIContext.Servicios.Add(a);
                    _APIContext.SaveChanges();
                    return a;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Servicio> UpdateServicio(Servicio a)
        {


            Func<Servicio> f = delegate
            {



                var e = _APIContext.Entry(a);
                e.State = EntityState.Modified;

                _APIContext.SaveChanges();

                return a;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteServicio(Servicio a)
        {


            Func<bool> f = delegate
            {

                var Servicio_result = this._APIContext.Servicios.Remove(a);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        private bool ServicioExists(string id)
        {
            return _APIContext.Servicios.Any(e => e.Id == id);
        }

    }
}
