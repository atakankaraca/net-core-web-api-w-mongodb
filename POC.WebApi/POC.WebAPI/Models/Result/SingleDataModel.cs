using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.WebAPI.Models.Result
{
    public class SingleDataModel<T> : ReturnModel
    {
        public T Data { get; set; }
    }
}
