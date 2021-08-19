using Project_Inventory.BDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_Inventory.Tools
{
    /// <summary>
    /// Use to convert jsons and Objects between UI and API
    /// </summary>
    static class JsonCenter
    {
        const string empty = "[]";

        /// <summary>
        /// Convert a Tab of Datas to json
        /// </summary>
        /// <param name="dataTab"></param>
        /// <returns></returns>
        public static string ObjectJsonBuilder(Data[] dataTab)
        {
            string json = "[";

            foreach (Data data in dataTab)
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
            string json = storage.ToJson();

            return json;
        }

        /// <summary>
        /// Convert a Custom List to json
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public static string ObjectJsonBuilder(CustomList customList)
        {
            string json = customList.ToJson();

            return json;
        }

        /// <summary>
        /// Get all infos for the Storage Selection Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static Storage[] LoadStorageSelectionInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibraries.ToString());

            if (responseBdd == empty)
            {
                return new Storage[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.Storage) as Storage[];
            }
        }

        /// <summary>
        /// Get all infos for the Custom List Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static CustomList[] LoadListMenuInfos(RequestCenter requestCenter, out List<StorageXCustomList> storageXCustomList, int storageId)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/storage/" + storageId);

            if (responseBdd == empty)
            {
                storageXCustomList = new List<StorageXCustomList>();
            }
            else
            {
                storageXCustomList = (FormatToBDDObject(responseBdd, ObjectName.StorageXCustomList) as StorageXCustomList[]).ToList();
            }

            responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString());

            if (responseBdd == empty)
            {
                return new CustomList[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[];
            }
        }

        /// <summary>
        /// Get all infos for the Custom List Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static CustomList[] LoadInitStorageInfos(RequestCenter requestCenter)
        {
            CustomList[] customLists;
            List<CustomList> listCustomLists = new List<CustomList>();

            string responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString());

            if (responseBdd == empty)
            {
                customLists = new CustomList[0];
            }
            else
            {
                customLists = FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[];

                foreach(CustomList customList in customLists)
                {
                    responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/customList/" + customList.id);

                    if (responseBdd != empty)
                    {
                        listCustomLists.Add(customList);
                    }
                }

                customLists = new CustomList[listCustomLists.Count];

                for (int i = 0; i < customLists.Length; i++)
                {
                    customLists[i] = listCustomLists[i];
                }
            }

            return customLists;
        }

        /// <summary>
        /// Get all infos for the Custom List Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static CustomList[] LoadListMenuInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString());

            if (responseBdd == empty)
            {
                return new CustomList[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[];
            }
        }

        /// <summary>
        /// Get all infos of Custom Lists and "Data Cross Custom List" link
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static CustomList[] LoadAllListInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString());

            if (responseBdd == empty)
            {
                return new CustomList[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[];
            }
        }

        /// <summary>
        /// Get all infos for the Custom List Viewer Page
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static List<ListOption> LoadListViewerPageInfos(RequestCenter requestCenter, int customListId)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/customList/" + customListId);

            if (responseBdd == empty)
            {
                return new List<ListOption>();
            }
            else
            {
                return (FormatToBDDObject(responseBdd, ObjectName.ListOption) as ListOption[]).ToList();
            }
        }

        /// <summary>
        /// Get all infos for the Storage Viewer
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public static Data[] LoadStorageViewerInfos(RequestCenter requestCenter, int storageId, out List<List<ListOption>> listOptionsTab, out List<int> customListIds)
        {
            listOptionsTab = new List<List<ListOption>>();
            customListIds = new List<int>();
            List<StorageXCustomList> storagesXLists;

            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/storage/" + storageId);

            if (responseBdd == empty)
            {
                storagesXLists = new List<StorageXCustomList>();
            }
            else
            {
                storagesXLists = (FormatToBDDObject(responseBdd, ObjectName.StorageXCustomList) as StorageXCustomList[]).ToList();
            }

            foreach (StorageXCustomList storageXCustom in storagesXLists)
            {
                customListIds.Add(storageXCustom.CustomListId);
                responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/customList/" + storageXCustom.CustomListId);

                if (responseBdd == empty)
                {
                    listOptionsTab.Add(new List<ListOption>());
                }
                else
                {
                    listOptionsTab.Add((FormatToBDDObject(responseBdd, ObjectName.ListOption) as ListOption[]).ToList().OrderBy(obj => obj.Index).ToList());
                }
            }

            responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/storage/" + storageId);

            if (responseBdd == empty)
            {
                return new Data[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.Data) as Data[];
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
            string responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/storage/" + storageId);

            if (responseBdd == empty)
            {
                return new Data[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.Data) as Data[];
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
            string responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/storage/" + storageId + "/" + researchString);

            if (responseBdd == empty)
            {
                return new Data[0];
            }
            else
            {
                return FormatToBDDObject(responseBdd, ObjectName.Data) as Data[];
            }
        }

        /// <summary>
        /// Load All Cross Infos between Storage and CustomList
        /// </summary>
        /// <returns></returns>
        public static List<StorageXCustomList> LoadStorageXCustomLists(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/customList/" + id);

            StorageXCustomList[] storageXCustomLists = FormatToBDDObject(responseBdd, ObjectName.StorageXCustomList) as StorageXCustomList[];

            return storageXCustomLists.ToList();
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
        private static BDDObject[] FormatToBDDObject(string stf, ObjectName objectType)
        {
            string[] splitTab = stf.Split(new string[] { ",{" }, StringSplitOptions.None);
            int i = 0;

            switch (objectType)
            {
                case ObjectName.Data:
                    Data[] dataLibrary = new Data[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        dataLibrary[i] = FormatObject(sf, new string[] { "id", "storageId", "dataText", "dataType", "isHeader", "codeBar" }, dataLibrary[i]);
                        i++;
                    }
                    return dataLibrary;

                case ObjectName.Storage:
                    Storage[] storageLibrary = new Storage[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        storageLibrary[i] = FormatObject(sf, new string[] { "id", "name" }, storageLibrary[i]);
                        i++;
                    }
                    return storageLibrary;

                case ObjectName.CustomList:
                    CustomList[] customListLibrary = new CustomList[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        customListLibrary[i] = FormatObject(sf, new string[] { "id", "name" }, customListLibrary[i]);
                        i++;
                    }
                    return customListLibrary;

                case ObjectName.ListOption:
                    ListOption[] listOptionLibrary = new ListOption[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        listOptionLibrary[i] = FormatObject(sf, new string[] { "id", "customListId", "index", "name" }, listOptionLibrary[i]);
                        i++;
                    }
                    return listOptionLibrary;

                case ObjectName.StorageXCustomList:
                    StorageXCustomList[] storageXCustomListsLibrary = new StorageXCustomList[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        storageXCustomListsLibrary[i] = FormatObject(sf, new string[] { "id", "storageId", "customListId" }, storageXCustomListsLibrary[i]);
                        i++;
                    }
                    return storageXCustomListsLibrary;
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
            List<string> dataText;
            string dataTextTemp = string.Empty;
            List<string> dataType;
            string dataTypeTemp = string.Empty;
            bool isHeader = false;

            string temp;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                switch (split)
                {
                    case "id":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);

                        id = Int32.Parse(temp);
                        break;

                    case "storageId":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);

                        storageId = Int32.Parse(temp);
                        break;

                    case "dataText":
                        dataTextTemp = splitTab[i + 2];
                        break;

                    case "dataType":
                        dataTypeTemp = splitTab[i + 2];
                        break;

                    case "isHeader":
                        if (splitTab[i + 2] == "True")
                        {
                            isHeader = true;
                        }
                        else if (splitTab[i + 2] == "False")
                        {
                            isHeader = false;
                        }
                        break;
                }
                i++;
            }

            dataText = dataTextTemp.Split('~').ToList();
            dataType = dataTypeTemp.Split('~').ToList();

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
                switch (split)
                {
                    case "id":
                        string temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        id = Int32.Parse(temp);
                        break;

                    case "name":
                        name = splitTab[i + 2];
                        break;
                }
                i++;
            }

            storage = new Storage(id, name);

            return storage;
        }

        /// <summary>
        /// Convert a json to Custom List
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="customList"></param>
        /// <returns></returns>
        private static CustomList FormatObject(string stf, string[] separators, CustomList customList)
        {

            int id = 42;
            string name = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                switch (split)
                {
                    case "id":
                        string temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);

                        id = Int32.Parse(temp);
                        break;

                    case "name":
                        name = splitTab[i + 2];
                        break;
                }

                i++;
            }

            customList = new CustomList(id, name);

            return customList;
        }

        /// <summary>
        /// Convert a json to ListOption
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="listOption"></param>
        /// <returns></returns>
        private static ListOption FormatObject(string stf, string[] separators, ListOption listOption)
        {

            int id = 42;
            int customListId = 42;
            int index = 42;
            string name = string.Empty;

            string temp = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                switch (split)
                {
                    case "id":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        id = Int32.Parse(temp);
                        break;

                    case "customListId":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        customListId = Int32.Parse(temp);
                        break;

                    case "index":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        index = Int32.Parse(temp);
                        break;

                    case "name":
                        name = splitTab[i + 2];
                        break;
                }
                i++;
            }

            listOption = new ListOption(id, customListId, index, name);

            return listOption;
        }

        /// <summary>
        /// Convert a json to StorageXCustomList
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="storagesXCustomLists"></param>
        /// <returns></returns>
        private static StorageXCustomList FormatObject(string stf, string[] separators, StorageXCustomList storagesXCustomLists)
        {

            int id = 42;
            int storageId = 42;
            int customListId = 42;

            string temp = string.Empty;
            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                switch (split)
                {
                    case "id":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        id = Int32.Parse(temp);
                        break;

                    case "storageId":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        storageId = Int32.Parse(temp);
                        break;

                    case "customListId":
                        temp = splitTab[i + 1];
                        temp = temp.Remove(0, 1);
                        do 
                        {
                            temp = temp.Remove(temp.Length - 1, 1);
                        } 
                        while (!Int32.TryParse(temp, out customListId));

                        break;
                }
                i++;
            }

            storagesXCustomLists = new StorageXCustomList(id, storageId, customListId);

            return storagesXCustomLists;
        }
    }
}
