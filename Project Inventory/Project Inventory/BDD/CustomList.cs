using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class CustomList : BDDObject
    {
        public string Name { get; set; }

        public CustomList(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public CustomList(string name)
            : base(42)
        {
            Name = name;
        }

        /// <summary>
        /// Convert Data to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"Name\":\"" + Name + "\"}";
        }


        /// <summary>
        /// Convert Data to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"Name\":\"" + Name + "\"}";
        }
    }
}
