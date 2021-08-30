using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Local_API_Server.Models
{
    public class LogLibrary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
    }
}
