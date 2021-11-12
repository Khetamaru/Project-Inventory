using System;
using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class Bug: BDDObject
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public bool Handled { get; set; }

        public Bug(int id, int userId, string description, bool handle)
            : base(id)
        {
            UserId = userId;
            Description = description;
            Handled = handle;
        }

        public Bug(int userId, string description, bool handle)
            : base(42)
        {
            UserId = userId;
            Description = description;
            Handled = handle;
        }

        /// <summary>
        /// Convert Bug to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"" + BugEnum.userId + "\":" + UserId + ",\"" + BugEnum.description + "\":\"" + Description + "\"}";
        }


        /// <summary>
        /// Convert Bug to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"" + BugEnum.id + "\":" + id + ",\"" + BugEnum.userId + "\":" + UserId + ",\"" + BugEnum.description + "\":\"" + Description + "\",\"" + BugEnum.handled + "\":" + Handled.ToString().ToLower() + "}";
        }
    }

    public enum BugEnum
    {
        id,
        userId,
        description,
        handled
    }
}
