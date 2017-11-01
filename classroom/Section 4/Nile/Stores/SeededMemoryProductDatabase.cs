using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores {
    /// <summary>Base class for product database</summary>
    public static class ProductDatabaseExtensions {
        public static void WithSeedData( IProductDatabase database)
        {
            database.Add(new Product() { Id = 1, Name = "Galaxy S7", Price = 650 });
            database.Add(new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true });
            database.Add(new Product() { Id = 3, Name = "iPhoneX", Price = 900 });
            database.Add(new Product() { Id = 4, Name = "Windows", Price = 1200 });
        }
    }
}
