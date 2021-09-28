using System;
using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class Log : BDDObject
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Log(int id, int userId, string message)
            : base(id)
        {
            UserId = userId;
            Message = message;
            Date = DateTime.Now;
        }

        public Log(int id, int userId, string message, DateTime date)
            : base(id)
        {
            UserId = userId;
            Message = message;
            Date = date;
        }

        public Log(int userId, string message)
            : base(42)
        {
            UserId = userId;
            Message = message;
            Date = DateTime.Now;
        }

        public Log(int userId, string message, DateTime date)
            : base(42)
        {
            UserId = userId;
            Message = message;
            Date = date;
        }

        /// <summary>
        /// Convert Log to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"" + LogEnum.userId + "\":" + UserId + ",\"" + LogEnum.message + "\":\"" + Message + "\",\"" + LogEnum.date + "\":\"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"}";
        }


        /// <summary>
        /// Convert Log to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"" + LogEnum.id + "\":" + id + ",\"" + LogEnum.userId + "\":" + UserId + ",\"" + LogEnum.message + "\":\"" + Message + "\",\"" + LogEnum.date + "\":\"" + Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "\"}";
        }
    }

    public enum LogEnum
    {
        id,
        userId,
        message,
        date
    }
}
