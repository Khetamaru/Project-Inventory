using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Object use to regroup Datas by Storages
    /// </summary>
    public class Storage : BDDObject
    {
        public string Name { get; set; }

        public Storage(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public Storage(string name)
            : base(42)
        {
            Name = name;
        }

        /// <summary>
        /// Convert Storage to json without Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"name\":\"" + Name + "\"}";
        }

        /// <summary>
        /// Convert Storage to json with Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"name\":\"" + Name + "\"}";
        }
    }
}
