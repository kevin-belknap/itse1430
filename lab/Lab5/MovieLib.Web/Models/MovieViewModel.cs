/* ITSE1430
 * Kevin Belknap
 * December 8, 2017
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieLib.Web.Models {
    /// <summary>Isolate the movie model from the business logic</summary>
    public class MovieViewModel {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(0, Int32.MaxValue)]
        [Required(ErrorMessage = "Length must be >= 0")]
        public int Length { get; set; }

        public bool Owned { get; set; }
    }
}