using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using System.Reflection;
using System.Data;
using System.Text;
using System.ComponentModel;
namespace WebAPI.Services
{
    public class BaseService
    {
        public APIContext _APIContext;


        public BaseService(APIContext APIContext)
        {
            this._APIContext = APIContext;
            //this._APIContext.Database.EnsureCreated();
        }
        public U ChangeType<U>(object source)
        {
            if (source is U)
                return (U)source;

            var destinationType = typeof(U);
            if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                destinationType = new NullableConverter(destinationType).UnderlyingType;

            return (U)Convert.ChangeType(source, destinationType);
        }
        public U ChangeType<U>(object source, U returnValueIfException)
        {
            try
            {
                return this.ChangeType<U>(source);
            }
            catch
            {
                return returnValueIfException;
            }
        }

        protected ServiceResponse<TResult> Execute<TResult>(Func<TResult> func)
        {
            var response = new ServiceResponse<TResult>();
            try
            {
                response.Result = func.Invoke();
                response.HasError = false;
                response.Exception = null;
            }
            catch (Exception ex)
            {

                response.Result = default(TResult);
                response.HasError = true;
                response.Exception = ex;
            }
            return response;
        }



        #region "StoredProcedure Execution Helper"
        public List<Dictionary<string, object>> ExecuteStoreProcedure(Action<System.Data.Common.DbCommand> cmd)
        {

            var conn = _APIContext.Database.GetDbConnection();
            var NewCmd = conn.CreateCommand();
            NewCmd.CommandType = System.Data.CommandType.StoredProcedure;

            var result = new List<Dictionary<string, object>>();

            try
            {

                conn.Open();

                cmd(NewCmd);


                using (var reader = NewCmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {


                    while (reader.Read())
                    {
                        result.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
                    }


                }


                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();




            }
            catch (Exception ex)
            {

                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                throw ex;

            }



            return result;





        }

        public void SetParameterCommand(System.Data.Common.DbCommand cmd, string Name, object Value)
        {

            var p = cmd.CreateParameter();
            p.ParameterName = Name;
            p.Value = Value;
            cmd.Parameters.Add(p);
        }

        public List<T> MapToList<T>(System.Data.Common.DbDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        #endregion






    }






    public class ServiceResponse<T>
    {
        public T Result { get; set; }
        public bool HasError { get; internal set; }
        public Exception Exception { get; internal set; }
    }
}