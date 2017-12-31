using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.WebAPI.Models
{
    public class ReturnModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}
