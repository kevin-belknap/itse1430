using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile {
    /// <summary>Base class for product database</summary>
    public abstract class ProductDatabase : IProductDatabase
    { 
        
        /// <summary>Adds a Product</summary>
        /// <param name="product">The product to add</param>
        /// <returns>The added product</returns>
        /// <exception cref="ArgumentNullException"><paramref name="product"/>Product is null</exception>
        /// <exception cref="ValidationException"><paramref name="product"/>Product is invalid</exception>
        public Product Add( Product product )
        {
            //TODO: validate
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product was null");
            //return null;

            //if (!ObjectValidator.TryValidate(product, out var errors))
            //    throw new System.ComponentModel.DataAnnotations.ValidationException("Product was not valid", nameof(product));
            //    //return null;

            ObjectValidator.Validate(product);

            //emulate database by storing copy
            try
            {
                return AddCore(product);
            } catch (Exception e)
            {
                //throw different exception
                throw new Exception("Add failed", e);

                //Re-throw
                throw;

                //Silently ignore - almost always bad
            }
        }

        /// <summary>Get a specific product</summary>
        /// <returns>the product, if it exists</returns>
        public Product Get( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");
                //return null;
            
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
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0.");

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
                throw new ArgumentNullException(nameof(product));

            //if (!ObjectValidator.TryValidate(product, out var errors))
            //    throw new ArgumentNullException("Product is invalid", nameof(product));
            ObjectValidator.Validate(product);

            //Get existing product
            //var existing = GetCore(product.Id);
            //if (existing == null)
            //    throw new Exception("Product not found.");

            //instead of the above, use:        //allows you to not have to use if statements.  Generally used for null checking.
            var existing = GetCore(product.Id) ?? throw new Exception("Productnot found.");

            return UpdateCore(existing, product);
        }


        protected abstract Product UpdateCore( Product existing, Product newItem );

        
        protected abstract Product AddCore( Product product );
    }
}
