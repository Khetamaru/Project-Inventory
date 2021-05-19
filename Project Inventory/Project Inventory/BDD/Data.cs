using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Inventory.BDD
{
    public class Data
    {
        public string DataText { get; set; }
        public string DataType { get; set; }

        public Data(string dataText, string dataType)
        {
            DataText = dataText;
            DataType = dataType;
        }
    }
}
