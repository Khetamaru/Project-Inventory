using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Inventory.BDD
{
    public class Data : BDDObject
    {
        public string[] DataText { get; set; }
        public string[] DataType { get; set; }

        public Data(int id, string[] dataText, string[] dataType)
            : base(id)
        {
            DataText = dataText;
            DataType = dataType;
        }
    }
}
