using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores {
    /// <summary>Base class for product database</summary>
    public abstract class ProductDatabase : IProductDatabase
    { 
        
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
            return AddCore(product);
            
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
            
            return GetCore(id);
        }

        protected abstract Product GetCore( int id );

        /// <summary>Gets all products</summary>
        /// <returns>The products.</returns>
        public IEnumerable<Product> GetAll()
        {
            return GetAllCore();
            
            #region Ignore
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
            #endregion

        }

        protected abstract IEnumerable<Product> GetAllCore();

        public void Remove (int id)
        {
            if (id <= 0)
                return;

            var product = GetCore(id);
            if (product.Id == id)
                RemoveCore(id);
        }

        /// <summary>Removes the product</summary>
        /// <param name="product">The product to remove</param>
        protected abstract void RemoveCore( int id );
        

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
            var existing = GetCore(product.Id);
            if (existing == null)
                return null;

            return UpdateCore(existing, product);
        }

        protected abstract Product UpdateCore( Product existing, Product newItem );

        
        protected abstract Product AddCore( Product product );
    }
}
