using LabProject.Logic;
using LabProject.Data;

namespace LabProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new InventoryRepository();
            var service = new InventoryService(repository);

            // Example usage
            service.AddItem("Apple");
            service.AddItem("Banana");

            var items = service.GetAllItems();
            System.Console.WriteLine("Inventory Items:");
            foreach (var item in items)
            {
                System.Console.WriteLine($"- {item}");
            }
        }
    }
}