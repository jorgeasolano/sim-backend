using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



using WebAPI.Models;

namespace WebAPI.Services
{
    public class SearcherService : BaseService
    {



        public SearcherService(APIContext APIContext) : base(APIContext)
        {


        }




        public ServiceResponse<List<Dictionary<string, object>>> searcher(string Filter, string TableName)
        {

            Func<List<Dictionary<string, object>>> f = delegate
            {


                return this.ExecuteStoreProcedure(cmd =>
                {
                    cmd.CommandText = "searcher";

                    SetParameterCommand(cmd, "Filter", Filter);
                    SetParameterCommand(cmd, "TableName", TableName);

                });


            };
            return this.Execute(f);
        }



    }
}
