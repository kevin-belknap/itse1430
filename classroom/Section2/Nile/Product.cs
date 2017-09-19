using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile {
    /// <summary>Represents a Product</summary>
    /// <remarks>
    /// This will represent a product with other stuff.
    /// </remarks>
    public class Product 
    {
        //Each instance will have its own copy of these fields.
        //Whenever you are talking about variables that are part of the class, you refer to them as fields.

        ////////Public members should at least have a summary  
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name;

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description;

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public decimal Price;

        /// <summary>
        /// Determines if discontinued.
        /// </summary>
        public bool IsDiscontinued;
    }
}
