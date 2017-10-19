using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores {
    /// <summary>Base class for product database</summary>
    public class MemoryProductDatabase : ProductDatabase
    { 
        /// <summary>Adds a Product</summary>
        /// <param name="product">The product to add</param>
        protected override Product AddCore( Product product )
        {
            var newProduct = CopyProduct(product);
            _products.Add(newProduct);

            if (newProduct.Id <= 0)
                newProduct.Id = _nextId++;
            else if (newProduct.Id >= _nextId)
            {
                _nextId = newProduct.Id + 1;
            }

            return CopyProduct(newProduct);
        }

        /// <summary>Get a specific product</summary>
        /// <returns>the product, if it exists</returns>
        protected override Product GetCore( int id )
        {
            var product = FindProduct(id);

            return (product != null) ? CopyProduct(product) : null;
        }

        /// <summary>Gets all products</summary>
        /// <returns>The products.</returns>
        protected override IEnumerable<Product> GetAllCore()
        {
            foreach (var product in _products)
                yield return CopyProduct(product);
            
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
        protected override void RemoveCore( int id )
        {
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
        protected override Product UpdateCore( Product existing, Product product )
        {
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
