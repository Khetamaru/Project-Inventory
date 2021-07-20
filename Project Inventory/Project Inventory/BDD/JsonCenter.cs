using Project_Inventory.BDD;
using System;
using System.Windows;

namespace Project_Inventory.Tools
{
    /// <summary>
    /// Use to convert jsons and Objects between UI and API
    /// </summary>
    static class JsonCenter
    {
        /// <summary>
        /// Convert a Tab of Datas to json
        /// </summary>
        /// <param name="dataTab"></param>
        /// <returns></returns>
        public static string ObjectJsonBuilder(Data[] dataTab)
        {
            string json = "[";

            foreach(Data data in dataTab)
            {
                json += ObjectJsonBuilder(data) + ", ";
            }

            json.Remove(json.Length - 1, 1);

            json += "]";

            return json;
        }

        /// <summary>
        /// Convert a Data to json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ObjectJsonBuilder(Data data)
        {
            string json = data.ToJson();

            return json;
        }

        /// <summary>
        /// Convert a Storage to json
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public static string ObjectJsonBuilder(Storage storage)
        {
            string json = "\"Name\":" + "\"" + storage.Name + "\"";

            return json;
        }

        /// <summary>
        /// Get all infos for the Storage Selection Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static Storage[] LoadStorageSelectionInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest("StorageLibraries");

            if (responseBdd == "[]")
            {
                return new Storage[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, "storage") as Storage[];
            }
        }

        /// <summary>
        /// Get all infos for the Storage Viewer
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public static Data[] LoadStorageViewerInfos(RequestCenter requestCenter, int storageId)
        {
            string responseBdd = requestCenter.GetRequest("DataLibraries/storage/" + storageId);

            if (responseBdd == "[]")
            {
                return new Data[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, "data") as Data[];
            }
        }

        /// <summary>
        /// Get all researched infos for the Storage Viewer
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public static Data[] LoadStorageViewerInfos(RequestCenter requestCenter, int storageId, string researchString)
        {
            string responseBdd = requestCenter.GetRequest("DataLibraries/storage/" + storageId + "/" + researchString);

            if (responseBdd == "[]")
            {
                return new Data[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, "data") as Data[];
            }
        }

        /// <summary>
        /// Set up rederection in all buttons of Storage Selection Menu
        /// </summary>
        /// <param name="length"></param>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        public static RoutedEventLibrary[] SetEventHandlerTab(int length, RoutedEventHandler eventHandler)
        {
            RoutedEventLibrary[] bottomSwitchEvents = new RoutedEventLibrary[length];

            for (int i = 0; i < length; i++)
            {
                bottomSwitchEvents[i] = new RoutedEventLibrary();
                bottomSwitchEvents[i].changePageEvent = eventHandler;
            }

            return bottomSwitchEvents;
        }

        /// <summary>
        /// Convert a json to a BDD Object
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
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
                        dataLibrary[i] = FormatObject(sf, new string [] {"id", "storageId", "dataText", "dataType", "isHeader"}, dataLibrary[i]);
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

        /// <summary>
        /// Convert a json to Data
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Data FormatObject(string stf, string[] separators, Data data)
        {
            int id = 42;
            int storageId = 42;
            string[] dataText;
            string dataTextTemp = string.Empty;
            string[] dataType;
            string dataTypeTemp = string.Empty;
            bool isHeader = false;

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
                else if (split == "storageId")
                {
                    string temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    storageId = Int32.Parse(temp);
                }
                else if (split == "dataText")
                {
                    dataTextTemp = splitTab[i + 2];
                }
                else if (split == "dataType")
                {
                    dataTypeTemp = splitTab[i + 2];
                }
                else if (split == "isHeader")
                {
                    if(splitTab[i + 2] == "True")
                    {
                        isHeader = true;
                    }
                    else if(splitTab[i + 2] == "False")
                    {
                        isHeader = false;
                    }
                }

                i++;
            }

            dataText = dataTextTemp.Split(new string[] {"~"}, StringSplitOptions.None);
            dataType = dataTypeTemp.Split(new string[] {"~"}, StringSplitOptions.None);

            data = new Data(id, storageId, dataText, dataType, isHeader);

            return data;
        }

        /// <summary>
        /// Convert a json to Storage
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
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
