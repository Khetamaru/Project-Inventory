using System.Collections.Generic;

namespace Project_Inventory.BDD
{
    /// <summary>
    /// Use to save Storage Objects Informations
    /// </summary>
    public class CustomList : BDDObject
    {
        public string Name { get; set; }
        public List<string> Options { get; set; }

        public CustomList(int id, string name, List<string> options)
            : base(id)
        {
            Name = name;
            Options = options;
        }

        public CustomList(string name, List<string> options)
            : base(42)
        {
            Name = name;
            Options = options;
        }

        public CustomList(string name)
            : base(42)
        {
            Name = name;
            Options = new List<string>();
        }

        /// <summary>
        /// Convert Data to json without the Id
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return "{\"Name\":\"" + Name + "\",\"options\":\"" + ToStringOptions() + "\"}";
        }


        /// <summary>
        /// Convert Data to json with the Id
        /// </summary>
        /// <returns></returns>
        public string ToJsonId()
        {
            return "{\"Id\":" + id + ",\"Name\":" + Name + ",\"options\":\"" + ToStringOptions() + "\"}";
        }

        /// <summary>
        /// Convert Data Text to json
        /// </summary>
        /// <returns></returns>
        public string ToStringOptions()
        {
            string stg = "";
            int i = 0;

            foreach(string text in Options)
            {
                stg += text;

                if (i < Options.Count - 1)
                {
                    stg += "~";
                }
                i++;
            }

            return stg;
        }
    }
}
