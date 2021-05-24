using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Inventory.BDD
{
    public class Storage : BDDObject
    {
        public string Name { get; set; }

        public Storage(int id, string name)
            : base(id)
        {
            Name = name;
        }
    }
}
