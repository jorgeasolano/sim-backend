using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class CarrierService : BaseService
    {



        public CarrierService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<Carrier> GetCarrier(string id)
        {

            Func<Carrier> func = delegate
            {

                var c = this._APIContext.Carriers.Find(id);
                return c;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Carrier>> GetCarriers(string filter)
        {

            Func<List<Carrier>> f = delegate
            {

                return this._APIContext.Carriers.Where(c => c.Nombre.Contains(filter)).ToList();


            };
            return this.Execute(f);
        }


        public ServiceResponse<Carrier> CreateCarrier(Carrier c)
        {


            Func<Carrier> f = delegate
            {

                var Carrier_result = this._APIContext.Carriers.Find(c.Id);
                if (Carrier_result != null)
                {

                    throw new Exception("Carrier ya existe");
                }
                else
                {

                    _APIContext.Carriers.Add(c);
                    _APIContext.SaveChanges();
                    return c;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Carrier> UpdateCarrier(Carrier c)
        {


            Func<Carrier> f = delegate
            {



                var e = _APIContext.Entry(c);
                e.State = EntityState.Modified;

                _APIContext.SaveChanges();

                return c;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteCarrier(Carrier c)
        {


            Func<bool> f = delegate
            {

                var Carrier_result = this._APIContext.Carriers.Remove(c);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        private bool CarrierExists(string id)
        {
            return _APIContext.Carriers.Any(e => e.Id == id);
        }

    }
}
