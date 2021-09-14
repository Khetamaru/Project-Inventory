using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class ListOption : BDDObject
    {
        public int CustomListId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }

        public ListOption(int id, int customListId, int index, string name)
            : base(id)
        {
            CustomListId = customListId;
            Index = index;
            Name = name;
        }

        public ListOption(int customListId, int index, string name)
            : base(42)
        {
            CustomListId = customListId;
            Index = index;
            Name = name;
        }

        /// <summary>
        /// Convert Data to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"CustomListId\":" + CustomListId + ",\"Index\":" + Index + ",\"Name\":\"" + Name + "\"}";
        }


        /// <summary>
        /// Convert Data to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"CustomListId\":" + CustomListId + ",\"Index\":" + Index + ",\"Name\":\"" + Name + "\"}";
        }
    }
}
