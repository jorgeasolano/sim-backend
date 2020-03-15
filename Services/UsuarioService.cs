using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebAPI.Security;


using WebAPI.Models;

namespace WebAPI.Services
{
    public class UsuarioService : BaseService
    {

        //private readonly CipherService _CipherService;

        public UsuarioService(APIContext APIContext) : base(APIContext)
        {

            // this._CipherService = CipherService;
        }


        #region "Security"
        public ServiceResponse<Usuario> Login(string id, string contrasena)
        {

            Func<Usuario> func = delegate
            {


                string encryptedPassword = Encryptor.EncryptString(contrasena);
                Usuario finalResult;
                var UsuarioResult = _APIContext.Usuarios.Where(u => u.Id == id && u.Contrasena == encryptedPassword && u.EsActivo == true);
                if (UsuarioResult.Count() >= 1)
                {
                    finalResult = UsuarioResult.First();
                    finalResult.Contrasena = "";

                }
                else
                {
                    // Usuario no existe
                    throw new Exception("Credenciales Invalidas");
                }


                return finalResult;
            };



            return this.Execute(func);
        }

        #endregion



        public ServiceResponse<Usuario> GetUsuario(string id)
        {

            Func<Usuario> func = delegate
            {

                var u = this._APIContext.Usuarios.Find(id);
                return u;

            };

            return this.Execute(func);

        }

        public ServiceResponse<List<Dictionary<string, object>>> GetUsuarios(string filter)
        {

            Func<List<Dictionary<string, object>>> f = delegate
            {


                return this.ExecuteStoreProcedure(cmd =>
                {
                    cmd.CommandText = "GetUsuarios";

                    if (filter == null)
                    { filter = ""; }

                    SetParameterCommand(cmd, "filter", filter);

                });


            };
            return this.Execute(f);
        }


        public ServiceResponse<Usuario> CreateUsuario(Usuario u)
        {


            Func<Usuario> f = delegate
            {

                var usuario_result = this._APIContext.Usuarios.Find(u.Id);
                if (usuario_result != null)
                {

                    throw new Exception("Usuario ya existe");
                }
                else
                {
                    u.Contrasena = Encryptor.EncryptString(u.Contrasena);

                    _APIContext.Usuarios.Add(u);
                    _APIContext.SaveChanges();
                    return u;
                }

            };

            return this.Execute(f);

        }

        public ServiceResponse<Usuario> UpdateUsuario(Usuario u)
        {


            Func<Usuario> f = delegate
            {



                var e = _APIContext.Entry(u);
                e.State = EntityState.Modified;
                e.Property(p => p.Contrasena).IsModified = false;
                _APIContext.SaveChanges();

                return u;

            };

            return this.Execute(f);

        }

        public ServiceResponse<bool> DeleteUsuario(Usuario u)
        {


            Func<bool> f = delegate
            {

                var usuario_result = this._APIContext.Usuarios.Remove(u);
                _APIContext.SaveChanges();
                return true;

            };

            return this.Execute(f);

        }



    }
}
