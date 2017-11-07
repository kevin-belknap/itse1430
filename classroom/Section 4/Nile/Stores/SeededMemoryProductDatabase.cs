using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores {
    /// <summary>Base class for product database</summary>
    public static class ProductDatabaseExtensions {

        public static Product GetByName( this IProductDatabase source, string name)
        {
            foreach (var item in source.GetAll())
            {
                if (String.Compare(item.Name, name, true) == 0)
                    return item;
            };

            return null;
        }
        public static void WithSeedData( this IProductDatabase source)
        {
            source.Add(new Product() { Id = 1, Name = "Galaxy S7", Price = 650 });
            source.Add(new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true });
            source.Add(new Product() { Id = 3, Name = "iPhoneX", Price = 900 });
            source.Add(new Product() { Id = 4, Name = "Windows", Price = 1200 });
        }
    }
}
