namespace Project_Inventory.BDD
{
    public class Data : BDDObject
    {
        public int StorageId { get; set; }
        public string[] DataText { get; set; }
        public string[] DataType { get; set; }
        public bool IsHeader { get; set; }

        public Data(int id, int storageId, string[] dataText, string[] dataType, bool isHeader)
            : base(id)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
            IsHeader = isHeader;
        }

        public Data(int storageId, string[] dataText, string[] dataType, bool isHeader)
            : base(42)
        {
            StorageId = storageId;
            DataText = dataText;
            DataType = dataType;
            IsHeader = isHeader;
        }

        public string ToJson()
        {
            return "{\"storageId\":" + StorageId + ",\"dataText\":\"" + ToStringDataText() + "\",\"dataType\":\"" + ToStringDataType() + "\",\"isHeader\":\"" + IsHeader + "\"}";
        }

        public string ToStringDataText()
        {
            string stg = "";
            int i = 0;

            foreach(string text in DataText)
            {
                stg += text;

                if (i < DataText.Length - 1)
                {
                    stg += "~";
                }
                i++;
            }

            return stg;
        }

        public string ToStringDataType()
        {
            string stg = "";
            int i = 0;

            foreach (string type in DataType)
            {
                stg += type;

                if (i < DataType.Length - 1)
                {
                    stg += "~";
                }
                i++;
            }

            return stg;
        }
    }
}
