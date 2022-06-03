using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.WebApi19Sample.Utility
{
    public class Enums
    {
        public enum EntityState
        {
            Added = 1,
            Modified =2,
            Deleted = 3
        }

        public enum StatusCodeEnum
        {
            Information = 100,
            Successful = 200,
            Redirection = 300,
            ClientError = 400,
            ServerError = 500
        }
    }
}
