using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAPİ.Tekrar.Models
{
    public class GeneralApiType<T> where T : class
    {
        public string Message { get; set; }
        public int ApiStatusCode { get; set; }
        public T ReturnObject { get; set; }
    }
}
