using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Inventory.BDD
{
    public class Data : BDDObject
    {
        public int StorageId { get; set; }
        public string[] DataText { get; set; }
        public string[] DataType { get; set; }

        public Data(int id, int storageId, string[] dataText, string[] dataType)
            : base(id)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
        }

        public Data(int storageId, string[] dataText, string[] dataType)
            : base(42)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
        }

        public string ToJson()
        {
            return "{\"storageId\":" + StorageId + ",\"dataText\":\"" + ToStringDataText() + ",\"dataType\":\"" + ToStringDataType() + "\"}";
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
