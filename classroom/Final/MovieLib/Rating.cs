/*
 * ITSE 1430
 */
using System;

namespace MovieLib
{
    /// <summary>Provides the different types of movie ratings.</summary>
    public enum Rating
    {
        /// <summary>Unknown or unspecified rating.</summary>
        Unspecified,

        /// <summary>General Audience.</summary>
        G,

        /// <summary>Parental Guidance suggested.</summary>
        PG,

        /// <summary>Parental Guidance suggested for those under 13.</summary>
        PG13,

        /// <summary>Restricted</summary>
        R,
    }
}
