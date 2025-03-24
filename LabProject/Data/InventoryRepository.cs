using System.Collections.Generic;

namespace LabProject.Data
{
    public class InventoryRepository
    {
        private readonly List<string> _items = new();

        public void AddItem(string item)
        {
            _items.Add(item);
        }

        public bool RemoveItem(string item)
        {
            return _items.Remove(item);
        }

        public List<string> GetAllItems()
        {
            return new List<string>(_items); // Return a copy to avoid direct modification
        }
    }
}