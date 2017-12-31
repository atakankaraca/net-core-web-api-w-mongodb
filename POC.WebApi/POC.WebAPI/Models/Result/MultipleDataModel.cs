using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.WebAPI.Models
{
    public class MultipleDataModel<T> : ReturnModel
    {
        public IEnumerable<T> Data { get; set; }
    }
}
