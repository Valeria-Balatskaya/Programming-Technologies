using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;

namespace Library.Tests
{
    public static class TestData
    {
        // Возвращает один и тот же экземпляр при каждом вызове
        public static User Alice => new User { Id = 1, Name = "Алиса" };

        public static CatalogItem CSharpBook => new CatalogItem
        {
            Id = 101,
            Title = "C# in action",
            Author = "Josef Albahari"
        };

        public static CatalogItem DotNetBook => new CatalogItem
        {
            Id = 102,
            Title = "ASP.NET Core",
            Author = "Microsoft"
        };
    }
}