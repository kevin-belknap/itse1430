/*
 * ITSE-1430
 * Kevin Belknap
 * 9.2.2017
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Movies
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public bool Owned { get; set; }

        public Movies(string title, string description, int length, bool owned)
        {
            Title = title;
            Description = description;
            Length = length;
            Owned = owned;
        }
    }
}