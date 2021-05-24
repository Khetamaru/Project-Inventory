using Project_Inventory.BDD;
using System;
using System.Windows;

namespace Project_Inventory.Tools
{
    static class JsonCenter
    {
        public static string ObjectJsonBuilder(Data data)
        {
            string json = "\"DataText\":" + "\"" + data.DataText + "\"" + "," +
                          "\"DataType\":" + "\"" + data.DataType + "\"";

            return json;
        }

        public static string ObjectJsonBuilder(Storage storage)
        {
            string json = "\"Name\":" + "\"" + storage.Name + "\"";

            return json;
        }

        public static Storage[] LoadStorageSelectionInfos(RequestCenter requestCenter)
        {
            //string responseBdd = requestCenter.GetRequest("StorageLibraries", "");

            string responseBdd = "[{\"id\":1,\"name\":\"walk dog\"},{\"id\":2,\"name\":\"walk dog\"},{\"id\":3,\"name\":\"walk dog\"},{\"id\":4,\"name\":\"walk dog\"},{\"id\":5,\"name\":\"walk dog\"}]";

            return FormatToBDDObject(responseBdd, "storage") as Storage[];
        }

        public static RoutedEventHandler[] SetEventHandlerTab(int length, RoutedEventHandler eventHandler)
        {
            RoutedEventHandler[] bottomSwitchEvents = new RoutedEventHandler[length];

            for (int i = 0; i < length; i++)
            {
                bottomSwitchEvents[i] = eventHandler;
            }

            return bottomSwitchEvents;
        }

        private static BDDObject[] FormatToBDDObject(string stf, string objectType)
        {
            string[] splitTab = stf.Split(new string[] { ",{" }, StringSplitOptions.None);
            int i = 0;

            switch (objectType)
            {
                case "data":
                    Data[] dataLibrary = new Data[splitTab.Length];
                    foreach(string sf in splitTab)
                    {
                        dataLibrary[i] = FormatObject(sf, new string [] {"id", "dataText", "dataType"}, dataLibrary[i]);
                        i++;
                    }
                    return dataLibrary;

                case "storage":
                    Storage[] storageLibrary = new Storage[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        storageLibrary[i] = FormatObject(sf, new string[] {"id", "name"}, storageLibrary[i]);
                        i++;
                    }
                    return storageLibrary;
            }

            return null;
        }

        private static Data FormatObject(string stf, string[] separators, Data data)
        {
            int id = 42;
            string[] dataText;
            string dataTextTemp = string.Empty;
            string[] dataType;
            string dataTypeTemp = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == "id")
                {
                    string temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                } 
                else if (split == "dataText")
                {
                    dataTextTemp = splitTab[i + 2];
                }
                else if (split == "dataType")
                {
                    dataTypeTemp = splitTab[i + 2];
                }

                i++;
            }

            dataText = dataTextTemp.Split(new string[] {"~"}, StringSplitOptions.None);
            dataType = dataTypeTemp.Split(new string[] { "~" }, StringSplitOptions.None);

            data = new Data(id, dataText, dataType);

            return data;
        }

        private static Storage FormatObject(string stf, string[] separators, Storage storage)
        {

            int id = 42;
            string name = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == "id")
                {
                    string temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                else if (split == "name")
                {
                    name = splitTab[i + 2];
                }

                i++;
            }

            storage = new Storage(id, name);

            return storage;
        }
    }
}
