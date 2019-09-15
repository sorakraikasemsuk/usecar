using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UseCar.Helper
{
    public class ResponseResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
    public static class ResponseCode
    {
        public const int ok = 200;
        public const int error = 500;
    }
}
