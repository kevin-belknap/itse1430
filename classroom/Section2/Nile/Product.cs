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
    public class Product {


        //public readonly Product None = new Product();
        //public Product None = new Product();


        //Each instance will have its own copy of these fields.  Field = data
        //Whenever you are talking about variables that are part of the class, you refer to them as fields.

        ////////Public members should at least have a summary  
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>Never returns null.</value>
        public string Name
        {
            //get must ALWAYS have a return statement.  It must return the exact type of the property.
            //equivelant method would look like --> string get_Name()
            //the gets and the sets make this a property.  So the user sees it as a field.  But the compiler takes a field call and runs the gettr or setter method.
            get {
                //use null conditional to ensure users never get a null value for the name variable
                return _name ?? "";
            }

            //equivelant method
            //void set_Name (string value)
            set {
                //inside of a setter, value is a keyword.  The parameter of the set is always value.
                //set the return value to a trimmed state.  Use Null Conditional to make sure the _name variable isn't null
                _name = value?.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim(); }
        }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public decimal Price { get; set; }


        /// <summary>Determines if discontinued.</summary>
        /// <value></value>
        public bool IsDiscontinued { get; set; }

        public const decimal DiscontinuedDiscountRate = 0.10M;

        //functions are a way to have access to all fields without having to expose those fields to the calling agent
        /// <summary>Gets the discounted price, if applicable</summary>
        /// <returns>The price.</returns>
        //public decimal GetDiscountedPrice()
        //{
        //    if (IsDiscontinued)
        //        return Price * 0.10M;

        //    return Price;

        //}

        //change above method into a property.  This is called a calculated property.
        public decimal DiscountedProperty
        {
            get {
                // if (IsDiscontinued)
                if (this.IsDiscontinued)
                    return Price * DiscontinuedDiscountRate;

                return Price;
            }
        }

        public override string ToString()
        {
            return base.ToString();
            {
                return Name;
            }
        }

        public virtual string Validate()
        {
            //TODO: validate

            if (String.IsNullOrEmpty(Name))
                return "Name cannot be empty";

                //Pri9ce > 0
                if (Price < 0)
                    return "Price must be >= 0";

                return null;
            }
        

        //property that allows anyone to get value, but i am only one that can set it.
        //you can mix accessibility.  You can have an access modifier on a get or a set.  It may be on one, but not both.
        //you always have to be same or more restrictive than property access modifier
        //public int ICanOnlySetIt { get; private set; }


        //public int ICanOnlySetIt2 { get; }

        //The two properties above are identical because the second will automatically set the set backing field to private.


        //fields are always camel cased
        //these are referred to as backing fields.
        private string _name;
        private string _description;

        //readonly applied to a field effectively makes this a CONST... with differences.  Readonly means the value is fixed the moment the instance of the class is created.  
        //it is not restricted to value types
        private readonly double _someValueICannotChange = 10;
    }
}
