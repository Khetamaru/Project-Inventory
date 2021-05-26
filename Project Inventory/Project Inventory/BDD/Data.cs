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

        public Data(string[] dataText, string[] dataType)
            : base(42)
        {
            DataText = dataText;
            DataType = dataType;
        }

        public string ToJson()
        {
            return "{\"dataText\":\"" + ToStringDataText() + ",\"dataType\":\"" + ToStringDataType() + "\"}";
        }

        public string ToStringDataText()
        {
            string stg = "";

            foreach(string text in DataText)
            {
                stg += text + "~";
            }

            stg.Remove(stg.Length - 1, 1);

            return stg;
        }

        public string ToStringDataType()
        {
            string stg = "";

            foreach (string type in DataType)
            {
                stg += type + "~";
            }

            stg.Remove(stg.Length - 1, 1);

            return stg;
        }
    }
}
