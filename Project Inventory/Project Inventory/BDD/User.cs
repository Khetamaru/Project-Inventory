using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class User : BDDObject
    {
        public string Name { get; set; }
        public int AccessibilityLevel { get; set; }

        public User(int id, string message, int accessibilityLevel)
            : base(id)
        {
            Name = message;
            AccessibilityLevel = accessibilityLevel;
        }

        /// <summary>
        /// Convert User to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"message\":\"" + Name + ",\"accessibilityLevel\":\"" + AccessibilityLevel + "\"}";
        }


        /// <summary>
        /// Convert User to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"message\":\"" + Name + ",\"accessibilityLevel\":\"" + AccessibilityLevel + "\"}";
        }
    }
}
