using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class Data : BDDObject
    {
        public int StorageId { get; set; }
        public List<string> DataText { get; set; }
        public List<string> DataType { get; set; }
        public bool IsHeader { get; set; }
        public string CodeBar { get; set; }

        public Data(int id, int storageId, List<string> dataText, List<string> dataType, bool isHeader)
            : base(id)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
            IsHeader = isHeader;
        }

        public Data(int storageId, List<string> dataText, List<string> dataType, bool isHeader)
            : base(42)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
            IsHeader = isHeader;
        }

        public Data(int id, int storageId, List<string> dataText, List<string> dataType, bool isHeader, string codeBar)
            : base(id)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
            IsHeader = isHeader;
            CodeBar = codeBar;
        }

        public Data(int storageId, List<string> dataText, List<string> dataType, bool isHeader, string codeBar)
            : base(42)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
            IsHeader = isHeader;
            CodeBar = codeBar;
        }

        /// <summary>
        /// Convert Data to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"" + DataEnum.storageId + "\":" + StorageId + ",\"" + DataEnum.dataText + "\":\"" + ToStringDataText() + "\",\"" + DataEnum.dataType + "\":\"" + ToStringDataType() + "\",\"" + DataEnum.isHeader + "\":" + IsHeader.ToString().ToLower() + ",\"" + DataEnum.codeBar + "\":\"" + CodeBar + "\"}";
        }

        /// <summary>
        /// Convert Data to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"" + DataEnum.id + "\":" + id + ",\"" + DataEnum.storageId + "\":" + StorageId + ",\"" + DataEnum.dataText + "\":\"" + ToStringDataText() + "\",\"" + DataEnum.dataType + "\":\"" + ToStringDataType() + "\",\"" + DataEnum.isHeader + "\":" + IsHeader.ToString().ToLower() + ",\"" + DataEnum.codeBar + "\":\"" + CodeBar + "\"}";
        }

        /// <summary>
        /// Convert Data Text to json
        /// </summary>
        /// <returns></returns>
        public string ToStringDataText()
        {
            string stg = "";
            int i = 0;

            foreach(string text in DataText)
            {
                stg += text;

                if (i < DataText.Count - 1)
                {
                    stg += "~";
                }
                i++;
            }

            return stg;
        }

        /// <summary>
        /// Convert Data Type to json
        /// </summary>
        /// <returns></returns>
        public string ToStringDataType()
        {
            string stg = "";
            int i = 0;

            foreach (string type in DataType)
            {
                stg += type;

                if (i < DataType.Count - 1)
                {
                    stg += "~";
                }
                i++;
            }

            return stg;
        }
    }

    public enum DataEnum
    {
        id,
        storageId,
        dataText,
        dataType,
        isHeader,
        codeBar
    }
}
