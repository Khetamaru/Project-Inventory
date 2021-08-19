using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class Log : BDDObject
    {
        public int StorageId { get; set; }
        public string Message { get; set; }

        public Log(int id, int storageId, string message)
            : base(id)
        {
            StorageId = storageId;
            Message = message;
        }

        /// <summary>
        /// Convert Log to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"storageId\":" + StorageId + ",\"message\":\"" + Message + "\"}";
        }


        /// <summary>
        /// Convert Log to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"storageId\":" + StorageId + ",\"message\":\"" + Message + "\"}";
        }
    }
}
