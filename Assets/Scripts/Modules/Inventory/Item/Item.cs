using Model.Item;

namespace Modules.Inventory.Item
{
    public class Item
    {
        public int Id { get; }
        public ItemType Type { get; }
        public string Name { get; }
        public float Width { get; }

        public Item(int id, ItemType type, string name, float width)
        {
            Id = id;
            Type = type;
            Name = name;
            Width = width;
        }
    }
}
