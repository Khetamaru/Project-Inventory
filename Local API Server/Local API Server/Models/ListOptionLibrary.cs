using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Local_API_Server.Models
{
    public class ListOptionLibrary
    {
        public int Id { get; set; }
        public int CustomListId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
    }
}
