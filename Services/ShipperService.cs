using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class ShipperService : BaseService
    {



        public ShipperService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<Shipper> GetShipper(string id)
        {

            Func<Shipper> func = delegate
            {

                var c = this._APIContext.Shippers.Find(id);
                return c;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Shipper>> GetShippers(string filter)
        {

            Func<List<Shipper>> f = delegate
            {

                return this._APIContext.Shippers.Where(c => c.Nombre.Contains(filter)).ToList();


            };
            return this.Execute(f);
        }


        public ServiceResponse<Shipper> CreateShipper(Shipper c)
        {


            Func<Shipper> f = delegate
            {

                var Shipper_result = this._APIContext.Shippers.Find(c.Id);
                if (Shipper_result != null)
                {

                    throw new Exception("Shipper ya existe");
                }
                else
                {

                    _APIContext.Shippers.Add(c);
                    _APIContext.SaveChanges();
                    return c;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Shipper> UpdateShipper(Shipper c)
        {


            Func<Shipper> f = delegate
            {



                var e = _APIContext.Entry(c);
                e.State = EntityState.Modified;

                _APIContext.SaveChanges();

                return c;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteShipper(Shipper c)
        {


            Func<bool> f = delegate
            {

                var Shipper_result = this._APIContext.Shippers.Remove(c);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        private bool ShipperExists(string id)
        {
            return _APIContext.Shippers.Any(e => e.Id == id);
        }

    }
}
