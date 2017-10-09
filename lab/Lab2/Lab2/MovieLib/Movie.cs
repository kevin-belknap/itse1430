/* ITSE1430
 * Kevin Belknap
 * October 7, 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib {
    /// <summary>Represents a Movie</summary>
    /// <remarks>
    /// This will represent a Movie
    /// </remarks>
    public class Movie {

        /// <summary>Gets or Sets the Title</summary>
        /// <value>Never returns null</value>
        public string Title
        {
            //use null conditional to ensure users never get a null value for the title variable
            get { return _title ?? ""; }
            set { _title = value?.Trim(); }
        }

        /// <summary>Gets or Sets the Description</summary>
        /// <value>Never returns null</value>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim(); }
        }

        /// <summary>Gets or Sets the Length</summary>
        public int Length { get; set; }

        /// <summary>Determines if Movie is owned</summary>
        public bool Owned { get; set; }

        public virtual string Validate(string LengthValue)
        {
            if (String.IsNullOrEmpty(Title))
                return "Title cannot be empty";
            
            if (Length < 0 && LengthValue.Length != 0)
                return "Length must be a number >= 0";

            return null;
        }

        //Backing Fields
        private string _title;
        private string _description;
    }

    
    
 
}
