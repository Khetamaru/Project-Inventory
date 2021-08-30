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
        public bool IsActive { get; set; }

        public User(int id, string name, int accessibilityLevel, bool isActive)
            : base(id)
        {
            Name = name;
            AccessibilityLevel = accessibilityLevel;
            IsActive = isActive;
        }

        public User(string name, int accessibilityLevel, bool isActive)
            : base(42)
        {
            Name = name;
            AccessibilityLevel = accessibilityLevel;
            IsActive = isActive;
        }

        /// <summary>
        /// Convert User to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"name\":\"" + Name + "\",\"accessibilityLevel\":" + AccessibilityLevel + ",\"isActive\":\"" + IsActive + "\"}";
        }

        /// <summary>
        /// Convert User to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"name\":\"" + Name + "\",\"accessibilityLevel\":" + AccessibilityLevel + ",\"isActive\":\"" + IsActive + "\"}";
        }
    }
}
