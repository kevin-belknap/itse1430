/*
 * ITSE 1430
 * Kevin Belknap
 * 12/14/2017
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MovieLib.Web.Models
{
    /// <summary>Provides a view model for movies.</summary>
    public class MovieViewModel : IValidatableObject
    {
        public int Id { get; set; }

        //Kevin Belknap
        //CR4 – (Feature) Require the movie name to be between 2 and 100 characters
        [Required(AllowEmptyStrings = false)]
        [MinLength(2, ErrorMessage ="Movie Title must be at least 2 characters in length.")]
        [MaxLength(100, ErrorMessage ="Movie Title cannot be more than 100 characters in length.")]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Display(Name = "Is Owned")]
        public bool IsOwned { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Length { get; set; }

        public Rating Rating { get; set; }

        //Kevin Belknap
        //CR2 - (Bug) Release year is not being properly limited to the given years
        [Range(1900, 2100)]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext ) => Enumerable.Empty<ValidationResult>();        
    }
}