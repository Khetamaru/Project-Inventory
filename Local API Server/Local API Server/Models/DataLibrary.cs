using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Local_API_Server.Models
{
    public class DataLibrary
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public string DataText { get; set; }
        public string DataType { get; set; }
        public bool IsHeader { get; set; }
    }
}
