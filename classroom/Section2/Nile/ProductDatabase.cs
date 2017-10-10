using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile {
    /// <summary>Base class for product database</summary>
    public class ProductDatabase 
    {
        public ProductDatabase()
        {
            _products[0] = new Product();
            _products[0].Name = "Kevin's recipe book";
            _products[0].Price = 250;

            _products[1] = new Product();
            _products[1].Name = "Kevin's Car book";
            _products[1].Price = 500;

            _products[2] = new Product();
            _products[2].Name = "iPhone X";
            _products[2].Price =900;
            _products[2].IsDiscontinued = true;
        }

        /// <summary>Adds a Product</summary>
        /// <param name="product">The product to add</param>
        public Product Add( Product product )
        {
            //TODO: Implement Add
            return product;
        }

        /// <summary>Get a specific product</summary>
        /// <returns>the product, if it exists</returns>
        public Product Get()
        {
            //TODO: Implement Get
            return null;
        }

        /// <summary>Gets all products</summary>
        /// <returns>The products.</returns>
        public Product[] GetAll()
        {
            var items = new Product[_products.Length];
            var index = 0;

            foreach(var product in _products)
            {
                items[index++] = CopyProduct(product);
            }

            return _products;
        }

        /// <summary>Removes the product</summary>
        /// <param name="product">The product to remove</param>
        public void Remove( Product product )
        {
            //TODO: Implement Remove
        }

        /// <summary>Updates a product</summary>
        /// <param name="product">The updated product.</param>
        public Product Update( Product product )
        {
            //TODO: Implement Update
            return product;
        }

        private Product CopyProduct( Product product )
        {
            if (product == null)
                return null;

            var newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.IsDiscontinued = product.IsDiscontinued;

            return newProduct;
        }

        private Product[] _products = new Product[100];
    }
}
