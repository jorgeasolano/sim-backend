using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class APIResponse<T>
    {

        public int code { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public T data { get; set; }
    }
}
