using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Version number
    /// </summary>
    public class SoftwareVersion : BDDObject
    {
        public string version { get; set; }

        public SoftwareVersion(int id, string _version)
            : base(id)
        {
            version = _version;
        }

        public SoftwareVersion(string _version)
            : base(42)
        {
            version = _version;
        }

        /// <summary>
        /// Convert Version number to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"" + VersionEnum.version + "\":\"" + version + "\"}";
        }

        /// <summary>
        /// Convert Version number to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"" + VersionEnum.id + "\":" + id + ",\"" + VersionEnum.version + "\":\"" + version + "\"}";
        }
    }

    public enum VersionEnum
    {
        id,
        version
    }
}
