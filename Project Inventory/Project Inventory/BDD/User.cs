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
            return "{\"" + UserEnum.name + "\":\"" + Name + "\",\"" + UserEnum.accessibilityLevel + "\":" + AccessibilityLevel + ",\"" + UserEnum.isActive + "\":" + IsActive.ToString().ToLower() + "}";
        }

        /// <summary>
        /// Convert User to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"" + UserEnum.id + "\":" + id + ",\"" + UserEnum.name + "\":\"" + Name + "\",\"" + UserEnum.accessibilityLevel + "\":" + AccessibilityLevel + ",\"" + UserEnum.isActive + "\":" + IsActive.ToString().ToLower() + "}";
        }
    }

    public enum UserEnum
    {
        id,
        name,
        accessibilityLevel,
        isActive
    }
}
