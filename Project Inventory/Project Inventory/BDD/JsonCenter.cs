using Project_Inventory.BDD;

namespace Project_Inventory.Tools
{
    static class JsonCenter
    {
        static string ObjectJsonBuilder(Data data)
        {
            string json = "\"DataText\":" + "\"" + data.DataText + "\"" + "," +
                          "\"DataType\":" + "\"" + data.DataType + "\"";

            return json;
        }

        static string ObjectJsonBuilder(Storage storage)
        {
            string json = "\"Name\":" + "\"" + storage.Name + "\"";

            return json;
        }
    }
}
