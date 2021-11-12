using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Local_API_Server.Models
{
    public class BugLibrary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public bool Handled { get; set; }
    }
}
