using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Inventory.BDD
{
    class RequestMySQL : BDDObject
    {
        public string Request { get; set; }

        public RequestMySQL(string request)
            : base(42)
        {
            Request = request;
        }

        /// <summary>
        /// Convert Data to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"" + RequestMySQLEnum.request + "\":\"" + Request + "\"}";
        }

        public enum RequestMySQLEnum
        {
            request
        }
    }
}
