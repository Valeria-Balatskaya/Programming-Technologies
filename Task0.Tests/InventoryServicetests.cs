using Xunit;
using LabProject.Data;
using LabProject.Logic;

namespace LabProject.Tests
{
    public class InventoryServiceTests
    {
        [Fact]
        public void AddItem_ShouldAddItemToInventory()
        {
            var repository = new InventoryRepository();
            var service = new InventoryService(repository);

            service.AddItem("Apple");

            var items = service.GetAllItems();
            Assert.Contains("Apple", items);
        }

        [Fact]
        public void RemoveItem_ShouldRemoveExistingItem()
        {
            var repository = new InventoryRepository();
            var service = new InventoryService(repository);
            service.AddItem("Banana");

            var result = service.RemoveItem("Banana");

            Assert.True(result);
            Assert.DoesNotContain("Banana", service.GetAllItems());
        }

        [Fact]
        public void RemoveItem_ShouldReturnFalseForNonExistingItem()
        {
            var repository = new InventoryRepository();
            var service = new InventoryService(repository);

            var result = service.RemoveItem("Orange");

            Assert.False(result);
        }

        [Fact]
        public void GetAllItems_ShouldReturnAllItemsInInventory()
        {
            var repository = new InventoryRepository();
            var service = new InventoryService(repository);
            service.AddItem("Mango");
            service.AddItem("Grapes");

            var items = service.GetAllItems();

            Assert.Equal(2, items.Count);
            Assert.Contains("Mango", items);
            Assert.Contains("Grapes", items);
        }

        [Fact]
        public void AddItem_ShouldThrowExceptionForEmptyItemName()
        {
            var repository = new InventoryRepository();
            var service = new InventoryService(repository);

            Assert.Throws<ArgumentException>(() => service.AddItem(""));
        }
    }
}