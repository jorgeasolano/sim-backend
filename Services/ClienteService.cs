using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class ClienteService : BaseService
    {



        public ClienteService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<Cliente> GetCliente(string id)
        {

            Func<Cliente> func = delegate
            {

                var u = this._APIContext.Clientes.Find(id);
                return u;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Cliente>> GetClientes(string filter)
        {

            Func<List<Cliente>> f = delegate
            {

                return this._APIContext.Clientes.Where(u => u.Nombre.Contains(filter)).ToList();


            };
            return this.Execute(f);
        }


        public ServiceResponse<Cliente> CreateCliente(Cliente u)
        {


            Func<Cliente> f = delegate
            {

                var Cliente_result = this._APIContext.Clientes.Find(u.Id);
                if (Cliente_result != null)
                {

                    throw new Exception("Cliente ya existe");
                }
                else
                {

                    _APIContext.Clientes.Add(u);
                    _APIContext.SaveChanges();
                    return u;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Cliente> UpdateCliente(Cliente u)
        {


            Func<Cliente> f = delegate
            {



                var e = _APIContext.Entry(u);
                e.State = EntityState.Modified;

                _APIContext.SaveChanges();

                return u;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteCliente(Cliente u)
        {


            Func<bool> f = delegate
            {

                var Cliente_result = this._APIContext.Clientes.Remove(u);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }

        private bool ClienteExists(string id)
        {
            return _APIContext.Clientes.Any(e => e.Id == id);
        }

    }
}
