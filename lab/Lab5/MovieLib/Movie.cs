/* ITSE1430
 * Kevin Belknap
 * November 17, 2017
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib {
    /// <summary>Represents a Movie</summary>
    /// <remarks>
    /// This will represent a Movie
    /// </remarks>
    public class Movie : IValidatableObject {

        /// <summary>
        /// Gets or Sets the Unique Identifier
        /// </summary>
        public int Id { get; set; }

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

        public override string ToString()
        {
            return Title;
        }

        /// <summary>Validates the object.</summary>
        /// <returns>The error message or null.</returns>      
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Title cannot be empty
            if (String.IsNullOrEmpty(Title))
                yield return new ValidationResult("Title cannot be empty.", new[] { nameof(Title) });
            
            //Length >= 0
            if (Length < 0)
                yield return new ValidationResult("Length must be >= 0.", new[] { nameof(Length) });
        }
        
        //Backing Fields
        private string _title;
        private string _description;
    }
}
