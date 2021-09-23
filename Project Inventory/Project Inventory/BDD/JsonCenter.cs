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

        public static Storage GetStorage(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.Storage) as Storage[])[0];
        }

        public static Data GetData(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.Data) as Data[])[0];
        }

        public static User GetUser(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.UserLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.User) as User[])[0];
        }

        public static Log GetLog(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.LogLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.Log) as Log[])[0];
        }

        public static StorageXCustomList GetStorageXCustomList(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.StorageXCustomList) as StorageXCustomList[])[0];
        }

        public static CustomList GetCustomList(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[])[0];
        }

        public static ListOption GetListOption(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + id);

            return (FormatToBDDObject(responseBdd, ObjectName.ListOption) as ListOption[])[0];
        }

        public static bool IsStorageInitialised(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Storage.ToString() + "/" + id);

            if (responseBdd != empty) return true;
            else return false;
        }

        public static bool IsCustomListEmpty(RequestCenter requestCenter, int id)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + id);

            if (responseBdd != empty) return true;
            else return false;
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
        /// Get all infos for the Storage Transfert Selection Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static List<Storage> LoadStorageTransfertSelectionInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibraries.ToString());

            if (responseBdd == empty)
            {
                return new List<Storage>();
            }
            else
            {
                List<Storage> storageList = (FormatToBDDObject(responseBdd, ObjectName.Storage) as Storage[]).ToList();
                List<Storage> returnList = new List<Storage>();

                foreach(Storage storage in storageList)
                {
                    if (IsStorageInitialised(requestCenter, storage.id))
                    {
                        returnList.Add(storage);
                    }
                }

                return returnList;
            }
        }

        /// <summary>
        /// Get all infos for the Global Storage Research Page
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static List<Data> LoadGlobalStorageResearchInfos(RequestCenter requestCenter, out List<Storage> storages)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibraries.ToString());

            if (responseBdd == empty)
            {
                storages = new List<Storage>();
            }
            else
            {
                storages = (FormatToBDDObject(responseBdd, ObjectName.Storage) as Storage[]).ToList();
            }

            responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString());

            if (responseBdd == empty)
            {
                return new List<Data>();
            }
            else
            {
                return (FormatToBDDObject(responseBdd, ObjectName.Data) as Data[]).ToList();
            }
        }

        /// <summary>
        /// Get all infos for the Main Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static List<User> LoadMainMenuInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.UserLibraries.ToString());

            if (responseBdd == empty)
            {
                return new List<User>();
            }
            else
            {
                return (FormatToBDDObject(responseBdd, ObjectName.User) as User[]).ToList();
            }
        }

        /// <summary>
        /// Get all infos for the Logs Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static List<Log> LoadLogsMenuInfos(RequestCenter requestCenter, out List<User> users)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.UserLibraries.ToString());

            if (responseBdd == empty)
            {
                users = new List<User>();
            }
            else
            {
                users = (FormatToBDDObject(responseBdd, ObjectName.User) as User[]).ToList();
            }

            responseBdd = requestCenter.GetRequest(BDDTabsName.LogLibraries.ToString());

            if (responseBdd == empty)
            {
                return new List<Log>();
            }
            else
            {
                return (FormatToBDDObject(responseBdd, ObjectName.Log) as Log[]).ToList();
            }
        }

        /// <summary>
        /// Get all infos for the Custom List Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static CustomList[] LoadListMenuInfos(RequestCenter requestCenter, out List<StorageXCustomList> storageXCustomList, int storageId)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/" + ObjectName.Storage.ToString() + "/" + storageId);

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
                    responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + customList.id);

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
        /// Get all infos for the User Menu
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <returns></returns>
        public static List<User> LoadUserMenuInfos(RequestCenter requestCenter)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.UserLibraries.ToString());

            if (responseBdd == empty)
            {
                return new List<User>();
            }
            else
            {
                return (FormatToBDDObject(responseBdd, ObjectName.User) as User[]).ToList();
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
            string responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + customListId);

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

            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/" + ObjectName.Storage.ToString() + "/" + storageId);

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
                responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + storageXCustom.CustomListId);

                if (responseBdd == empty)
                {
                    listOptionsTab.Add(new List<ListOption>());
                }
                else
                {
                    listOptionsTab.Add((FormatToBDDObject(responseBdd, ObjectName.ListOption) as ListOption[]).ToList().OrderBy(obj => obj.Index).ToList());
                }
            }

            responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Storage.ToString() + "/" + storageId);

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
        /// Get all infos for the Data details page
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static Data LoadDataDetailsPageInfos(RequestCenter requestCenter, int dataId, out List<List<ListOption>> listOptionsTab, out List<int> customListIds, out Data header)
        {
            listOptionsTab = new List<List<ListOption>>();
            customListIds = new List<int>();
            List<CustomList> customLists;
            string responseBdd;

            responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString());

            if (responseBdd == empty)
            {
                customLists = new List<CustomList>();
            }
            else
            {
                customLists = (FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[]).ToList();
            }

            foreach (CustomList customList in customLists)
            {
                responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + customList.id);

                if (responseBdd == empty)
                {
                    listOptionsTab.Add(new List<ListOption>());
                }
                else
                {
                    listOptionsTab.Add((FormatToBDDObject(responseBdd, ObjectName.ListOption) as ListOption[]).ToList().OrderBy(obj => obj.Index).ToList());
                }
                customListIds.Add(customList.id);
            }

            Data data = GetData(requestCenter, dataId);

            responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Header.ToString() + "/" + data.StorageId);

            if (responseBdd == empty)
            {
                throw new Exception();
            }
            else
            {
                header = (FormatToBDDObject(responseBdd, ObjectName.Data) as Data[])[0];
            }

            return data;
        }

        /// <summary>
        /// Get all infos for the Data transfert page
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static Data LoadDataTransfertPageInfos(RequestCenter requestCenter, int dataId, int storageId, out Data newData, out List<List<ListOption>> listOptionsTab, out List<int> customListIds, out Data header, out Data newHeader)
        {
            listOptionsTab = new List<List<ListOption>>();
            customListIds = new List<int>();
            List<CustomList> customLists;
            string responseBdd;

            responseBdd = requestCenter.GetRequest(BDDTabsName.CustomListLibraries.ToString());

            if (responseBdd == empty)
            {
                customLists = new List<CustomList>();
            }
            else
            {
                customLists = (FormatToBDDObject(responseBdd, ObjectName.CustomList) as CustomList[]).ToList();
            }

            foreach (CustomList customList in customLists)
            {
                responseBdd = requestCenter.GetRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + customList.id);

                if (responseBdd == empty)
                {
                    listOptionsTab.Add(new List<ListOption>());
                }
                else
                {
                    listOptionsTab.Add((FormatToBDDObject(responseBdd, ObjectName.ListOption) as ListOption[]).ToList().OrderBy(obj => obj.Index).ToList());
                }
                customListIds.Add(customList.id);
            }

            Data data = GetData(requestCenter, dataId);

            responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Header.ToString() + "/" + storageId);

            if (responseBdd == empty)
            {
                throw new Exception();
            }
            else
            {
                newHeader = (FormatToBDDObject(responseBdd, ObjectName.Data) as Data[])[0];
            }

            responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Header.ToString() + "/" + data.StorageId);

            if (responseBdd == empty)
            {
                throw new Exception();
            }
            else
            {
                header = (FormatToBDDObject(responseBdd, ObjectName.Data) as Data[])[0];
            }

            List<string> dataText = new List<string>();
            foreach(string str in newHeader.DataText)
            {
                dataText.Add("");
            }

            newData = new Data(storageId, dataText, newHeader.DataType, false);

            return data;
        }

        /// <summary>
        /// Get all infos for the Storage Viewer
        /// </summary>
        /// <param name="requestCenter"></param>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public static Data[] LoadStorageViewerInfos(RequestCenter requestCenter, int storageId)
        {
            string responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Storage.ToString() + "/" + storageId);

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
            string responseBdd = requestCenter.GetRequest(BDDTabsName.DataLibraries.ToString() + "/" + ObjectName.Storage.ToString() + "/" + storageId + "/" + researchString);

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
            string responseBdd = requestCenter.GetRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/" + ObjectName.CustomList.ToString() + "/" + id);

            if (responseBdd == empty)
            {
                return new List<StorageXCustomList>();
            }
            else
            {
                StorageXCustomList[] storageXCustomLists = FormatToBDDObject(responseBdd, ObjectName.StorageXCustomList) as StorageXCustomList[];

                return storageXCustomLists.ToList();
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
                        dataLibrary[i] = FormatObject(sf, dataLibrary[i]);
                        i++;
                    }
                    return dataLibrary;

                case ObjectName.Storage:
                    Storage[] storageLibrary = new Storage[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        storageLibrary[i] = FormatObject(sf, storageLibrary[i]);
                        i++;
                    }
                    return storageLibrary;

                case ObjectName.CustomList:
                    CustomList[] customListLibrary = new CustomList[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        customListLibrary[i] = FormatObject(sf, customListLibrary[i]);
                        i++;
                    }
                    return customListLibrary;

                case ObjectName.ListOption:
                    ListOption[] listOptionLibrary = new ListOption[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        listOptionLibrary[i] = FormatObject(sf, listOptionLibrary[i]);
                        i++;
                    }
                    return listOptionLibrary;

                case ObjectName.StorageXCustomList:
                    StorageXCustomList[] storageXCustomListsLibrary = new StorageXCustomList[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        storageXCustomListsLibrary[i] = FormatObject(sf, storageXCustomListsLibrary[i]);
                        i++;
                    }
                    return storageXCustomListsLibrary;

                case ObjectName.Log:
                    Log[] logLibrary = new Log[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        logLibrary[i] = FormatObject(sf, logLibrary[i]);
                        i++;
                    }
                    return logLibrary;

                case ObjectName.User:
                    User[] userLibrary = new User[splitTab.Length];
                    foreach (string sf in splitTab)
                    {
                        userLibrary[i] = FormatObject(sf, userLibrary[i]);
                        i++;
                    }
                    return userLibrary;
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
        private static Data FormatObject(string stf, Data data)
        {
            int id = 42;
            int storageId = 42;
            List<string> dataText;
            string dataTextTemp = string.Empty;
            List<string> dataType;
            string dataTypeTemp = string.Empty;
            bool isHeader = false;
            string isHeaderStr = string.Empty;
            string codeBar = string.Empty;

            string temp;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if(split == DataEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                } 
                else if (split == DataEnum.storageId.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    storageId = Int32.Parse(temp);
                }
                else if (split == DataEnum.dataText.ToString())
                {
                    dataTextTemp = splitTab[i + 2];
                }
                else if (split == DataEnum.dataType.ToString())
                {
                    dataTypeTemp = splitTab[i + 2];
                }
                else if (split == DataEnum.isHeader.ToString())
                {
                    isHeaderStr = splitTab[i + 1];
                    isHeaderStr = isHeaderStr.Remove(0, 1);
                    isHeaderStr = isHeaderStr.Remove(isHeaderStr.Length - 1, 1);
                    if (isHeaderStr == "true")
                    {
                        isHeader = true;
                    }
                    else if (isHeaderStr == "false")
                    {
                        isHeader = false;
                    }
                }
                else if (split == DataEnum.codeBar.ToString())
                {
                    codeBar = splitTab[i + 2];
                }
                i++;
            }

            dataText = dataTextTemp.Split('~').ToList();
            dataType = dataTypeTemp.Split('~').ToList();

            data = new Data(id, storageId, dataText, dataType, isHeader, codeBar);

            return data;
        }

        /// <summary>
        /// Convert a json to Storage
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        private static Storage FormatObject(string stf, Storage storage)
        {

            int id = 42;
            string name = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == StorageEnum.id.ToString())
                {
                    string temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    id = Int32.Parse(temp);
                }
                else if (split == StorageEnum.name.ToString())
                {
                    name = splitTab[i + 2];
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
        private static CustomList FormatObject(string stf, CustomList customList)
        {

            int id = 42;
            string name = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == CustomListEnum.id.ToString())
                {
                    string temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                else if (split == CustomListEnum.name.ToString())
                {
                    name = splitTab[i + 2];
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
        private static ListOption FormatObject(string stf, ListOption listOption)
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
                if (split == ListOptionEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    id = Int32.Parse(temp);
                }
                else if(split == ListOptionEnum.customListId.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    customListId = Int32.Parse(temp);
                }
                else if(split == ListOptionEnum.index.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    index = Int32.Parse(temp);
                }
                else if(split == ListOptionEnum.name.ToString())
                {
                    name = splitTab[i + 2];
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
        private static StorageXCustomList FormatObject(string stf, StorageXCustomList storagesXCustomLists)
        {

            int id = 42;
            int storageId = 42;
            int customListId = 42;

            string temp = string.Empty;
            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == StorageXCustomListEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    id = Int32.Parse(temp);
                } 
                else if (split == StorageXCustomListEnum.storageId.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    storageId = Int32.Parse(temp);
                }
                else if(split == StorageXCustomListEnum.customListId.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    do
                    {
                        temp = temp.Remove(temp.Length - 1, 1);
                    }
                    while (!Int32.TryParse(temp, out customListId));
                }
                i++;
            }

            storagesXCustomLists = new StorageXCustomList(id, storageId, customListId);

            return storagesXCustomLists;
        }

        /// <summary>
        /// Convert a json to Log
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static Log FormatObject(string stf, Log log)
        {

            int id = 42;
            int userId = 42;
            string message = string.Empty;
            DateTime date = new DateTime();

            string temp = string.Empty;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == LogEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    id = Int32.Parse(temp);
                }
                else if (split == LogEnum.userId.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    userId = Int32.Parse(temp);
                }
                else if(split == LogEnum.message.ToString())
                {
                    message = splitTab[i + 2];
                }
                else if(split == LogEnum.date.ToString())
                {
                    date = Convert.ToDateTime(splitTab[i + 2]);
                }
                i++;
            }

            log = new Log(id, userId, message, date);

            return log;
        }

        /// <summary>
        /// Convert a json to User
        /// </summary>
        /// <param name="stf"></param>
        /// <param name="separators"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static User FormatObject(string stf, User user)
        {
            int id = 42;
            string name = string.Empty;
            int accessibilityLevel = 42;
            bool isActive = false;
            string isActiveTemp = string.Empty;

            string temp;

            int i = 0;

            string[] splitTab = stf.Split(new string[] { "\"" }, StringSplitOptions.None);

            foreach (string split in splitTab)
            {
                if (split == UserEnum.id.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    id = Int32.Parse(temp);
                }
                else if (split == UserEnum.name.ToString())
                {
                    name = splitTab[i + 2];
                }
                else if(split == UserEnum.accessibilityLevel.ToString())
                {
                    temp = splitTab[i + 1];
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    accessibilityLevel = Int32.Parse(temp);
                }
                else if(split == UserEnum.isActive.ToString())
                {
                    isActiveTemp = splitTab[i + 1];
                    isActiveTemp = isActiveTemp.Remove(0, 1);
                    isActiveTemp = isActiveTemp.Remove(isActiveTemp.Length - 1, 1);

                    if (isActiveTemp[isActiveTemp.Length - 1] == '}') { isActiveTemp = isActiveTemp.Remove(isActiveTemp.Length - 1, 1); }

                    switch (isActiveTemp)
                    {
                        case "true":
                            isActive = true;
                            break;

                        case "false":
                            isActive = false;
                            break;

                        default:
                            throw new Exception();
                    }
                }
                i++;
            }

            user = new User(id, name, accessibilityLevel, isActive);

            return user;
        }
    }
}
