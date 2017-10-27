﻿/* ITSE1430
 * Kevin Belknap
 * October 27, 2017
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib {
    /// <summary>Validate objects</summary>
    public class ObjectValidator {
        public static bool TryValidate(IValidatableObject value, out IEnumerable<ValidationResult> errors)
        {
            var context = new ValidationContext(value);
            var results = new List<ValidationResult>();

            errors = results;

            return Validator.TryValidateObject(value, context, results);
        }
    }
}
