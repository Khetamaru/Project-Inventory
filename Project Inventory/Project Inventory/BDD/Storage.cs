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

        public Storage(string name)
            : base(42)
        {
            Name = name;
        }

        public string ToJson()
        {
            return "{\"name\":\"" + Name + "\"}";
        }
    }
}
