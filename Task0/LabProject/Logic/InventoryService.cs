using LabProject.Data;
using System;

namespace LabProject.Logic
{
    public class InventoryService
    {
        private readonly InventoryRepository _repository;

        public InventoryService(InventoryRepository repository)
        {
            _repository = repository;
        }

        public void AddItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("Item name cannot be null or empty.");
            _repository.AddItem(item);
        }

        public bool RemoveItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("Item name cannot be null or empty.");
            return _repository.RemoveItem(item);
        }

        public List<string> GetAllItems()
        {
            return _repository.GetAllItems();
        }
    }
}