using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows {
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var product = new Product();
            ////product.Name = "Product A";

            ////var price = product.GetDiscountedPrice();
            //var name = product.Name.Trim();

            ////in line 18, product is the Instance. The dot is the modifier?


            //String Split Example
            //var csv = "Field1 | Field2 ,, Field3 | Field4";
            //var delimiters = new char[2];
            //delimiters[0] = '|';
            //delimiters[1] = ',';

            //var tokens = csv.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            //var numberOfElements = tokens.Length;

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
