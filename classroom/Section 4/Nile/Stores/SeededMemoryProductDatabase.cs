using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores {
    /// <summary>Base class for product database</summary>
    public class SeededMemoryProductDatabase : MemoryProductDatabase {
        public SeededMemoryProductDatabase()
        {
            //Long way to initialize an object
            //product = new Product();
            //product.Name = "Samsung Note 7";
            //product.Price =150;
            //product.IsDiscontinued = true;
            //Add(product);

            //Short way to initialize an object (object initializer)
            //_products.Add(new Product() { Id = 1, Name = "Galaxy S7", Price = 650 });
            //_products.Add(new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true });
            //_products.Add(new Product() { Id = 3, Name = "iPhoneX", Price = 900 });

            //Collection Initializer syntax
            //Add Call is implicit.
            //_products = new List<Product>() {
            //    new Product() { Id = 1, Name = "Galaxy S7", Price = 650 },
            //    new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true },
            //    new Product() { Id = 3, Name = "iPhoneX", Price = 900 }
            //};

            //Collection Initializer syntax with array
            //_products.AddRange(new[] {               //You can take type out, it is implied
                AddCore(new Product() { Id = 1, Name = "Galaxy S7", Price = 650 });
                AddCore(new Product() { Id = 2, Name = "Samsung Note 7", Price = 150, IsDiscontinued = true });
            AddCore(new Product() { Id = 3, Name = "iPhoneX", Price = 900 });
            AddCore(new Product() { Id = 4, Name = "Windows", Price = 1200 });
            //});

            // _nextId = _products.Count + 1;
        }



        /// <summary>Adds a Product</summary>
        /// <param name="product">The product to add</param>
        public Product Add( Product product )
        {
            //TODO: validate
            if (product == null)
                return null;

            if (!ObjectValidator.TryValidate(product, out var errors))
                return null;
            
            //emulate database by storing copy
            var newProduct = CopyProduct(product);
            _products.Add(newProduct);
            newProduct.Id = _nextId++;

            return CopyProduct(newProduct);

            //var item = _list[0];

            //TODO: Implement Add
            //return product;
        }

        /// <summary>Get a specific product</summary>
        /// <returns>the product, if it exists</returns>
        public Product Get( int id )
        {
            if (id <= 0)
                return null;

            var product = FindProduct(id);

            return (product != null) ? CopyProduct(product) : null;

            //TODO: Implement Get
            return null;
        }

        /// <summary>Gets all products</summary>
        /// <returns>The products.</returns>
        public Product[] GetAll()
        {
            var items = new Product[_products.Count];
            var index = 0;

            foreach (var product in _products)
                items[index++] = CopyProduct(product);

            return items;

            //how many products?
            //var count = 0;
            //foreach (var product in _products)
            //{
            //    if (product != null)
            //        ++count;
            //}

            //var items = new Product[count];
            //var index = 0;

            //foreach(var product in _products)
            //{
            //    if (product != null)
            //        items[index++] = CopyProduct(product);
            //}

            //return items;
        }

        /// <summary>Removes the product</summary>
        /// <param name="product">The product to remove</param>
        public void Remove( int id )
        {

            //TODO: Validate
            if (id <= 0)
                return;

            var product = FindProduct(id);

            if (product != null)
                _products.Remove(product);

            //for (var index = 0; index < _list.Count; ++index)
            //{
            //    if (_list[index].Name == product.Name)
            //    {
            //        _list.RemoveAt(index);
            //        break;
            //    }
            //}

            //TODO: Implement Remove
        }

        /// <summary>Updates a product</summary>
        /// <param name="product">The updated product.</param>
        public Product Update( Product product )
        {

            //TODO: validate
            if (product == null)
                return null;

            if (!ObjectValidator.TryValidate(product, out var errors))
                return null;

            //Get existing product
            var existing = FindProduct(product.Id);
            if (existing == null)
                return null;

            //Replace
            _products.Remove(existing);

            //emulate database by storing copy
            var newProduct = CopyProduct(product);
            _products.Add(newProduct);

            return CopyProduct(newProduct);
        }

        private Product FindProduct ( int id )
        {
            foreach (var product in _products)
            {
                if (product.Id == id)
                    return product;
            }

            return null;
        }

        private Product CopyProduct( Product product )
        {
            if (product == null)
                return null;

            var newProduct = new Product();
            newProduct.Id = product.Id;
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.IsDiscontinued = product.IsDiscontinued;

            return newProduct;
        }

        //private Product[] _products = new Product[100];
        private List<Product> _products = new List<Product>();
        private int _nextId = 1;
    }
}
