using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Host
{
    class Program
    {

        static void Main( string[] args )
        {
            bool quit = false;
            do
            { 
                char choice = GetInput();

                switch (choice)
                {
                    //case statements (more efficient then else if. Use when finite number of statements)
                    //[case][label]:statement;
                    //[default:statement;

                    //every case statement has to have a break to prevent fall through. No blocks needed if single statement.  C# by default does not allow fall through.  You can
                    //do fall through by having a same case with nothing being it (look at L below).
                    case 'A': AddProduct(); break;

                    case 'l':    //allowing fall through
                    case 'L': ListProducts(); break;
                    case 'Q': quit = true; break;
                }    
            } while (!quit);
        }

        private static void ListProducts()
        {
            //Name $price (Discontinued)
            //Description

            //String.Format
            //string msg = String.Format("{0}\t\t\t{1}\t\t{2}", productName, productPrice, productDiscontinued ? "[Discontinued]" : "");

            //String.Interpolation
            string msg = $"{productName}\t\t\t${productPrice}\t\t{(productDiscontinued ? "[Discontinued]" : "")}";


            Console.WriteLine(msg);
            Console.WriteLine(productDescription);
            


        }

        private static void AddProduct()
        {
            Console.Write("Enter product name: ");  //Write will have the cursor at the end of the line so the user knows they are putting in a product name
            productName = Console.ReadLine().Trim();

            //Ensure not empty
            //TODO
            //Do this for homework

            Console.Write("Enter price (> 0): ");
            productPrice = ReadDecimal();

            Console.Write("Enter optional description:");
            productDescription = Console.ReadLine();


            Console.Write("Is it discontinued (Y/N): ");
            string productDiscounted = Console.ReadLine().Trim();   //reads user input until Enter key pressed

        }

        static char GetInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");            //WriteLine puts the message out and puts cursor on next line
                Console.WriteLine("".PadLeft(10), '-');

                Console.WriteLine("A)dd Product");
                Console.WriteLine("L)List Products");
                Console.WriteLine("Q)uit");


                string input = Console.ReadLine().Trim();

                //Handle scenarios for empty string
                //Option 1 - ""  (string literal with no characters in it)  runtime error
                //Might not work in other programs that don't support string types
                //if (input != "")

                //Option 2 - string field
                //if (input != String.Empty)   (will work in all programs)

                //Option 3 - check length
                //if (input.Length != 0)

                //Anything after a 'dot' on a variable, etc. that has not been assigned a value will return a null.  Null will cause problems, so check for null, too
                //if (input != null && input.Length != 0)
                //short circuit evaluation will look at the left side first. If it is false, it does not check the right side.  So the above line will work because it will ignore the right
                //side if the variable is null

                if (input != null && input.Length != 0)
                {

                    //String comparison
                    if (String.Compare(input, "A", true) == 0)
                        return 'A';

                    //char comparison
                    char letter = Char.ToUpper(input[0]);

                    if (letter == 'A')
                        return 'A';
                    else if (letter == 'L')
                        return 'L';
                    else if (letter == 'Q')
                        return 'Q';
                }

                //Error  
                Console.WriteLine("Please choose a valid option");
            };
        }

        static void Main2( string[] args)
        {

        }

        /// <summary>Reads a decimal from Console</summary>
        /// <returns>The decimal value</returns>
        static decimal ReadDecimal()
        {
            string input = Console.ReadLine();

            return Decimal.Parse(input);
        }

        //only put attributes for products outside of functions
        //Product 
        static string productName;
        static decimal productPrice;
        static string productDescription;
        static bool productDiscontinued;

    }
}
