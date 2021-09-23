using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class StorageXCustomList : BDDObject
    {
        public int StorageId { get; set; }
        public int CustomListId { get; set; }

        public StorageXCustomList(int id, int storageId, int customListId)
            : base(id)
        {
            StorageId = storageId;
            CustomListId = customListId;
        }

        public StorageXCustomList(int storageId, int customListId)
            : base(42)
        {
            StorageId = storageId;
            CustomListId = customListId;
        }

        /// <summary>
        /// Convert StorageXCustomList to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"" + StorageXCustomListEnum.storageId + "\":" + StorageId + ",\"" + StorageXCustomListEnum.customListId + "\":" + CustomListId + "}";
        }

        /// <summary>
        /// Convert StorageXCustomList to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"" + StorageXCustomListEnum.id + "\":" + id + ",\"" + StorageXCustomListEnum.storageId + "\":" + StorageId + ",\"" + StorageXCustomListEnum.customListId + "\":" + CustomListId + "}";
        }
    }

    public enum StorageXCustomListEnum
    {
        id,
        storageId,
        customListId
    }
}
