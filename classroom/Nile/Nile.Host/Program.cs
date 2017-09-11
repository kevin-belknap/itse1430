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
            throw new NotImplementedException();
        }

        private static void AddProduct()
        {
            throw new NotImplementedException();
        }

        static char GetInput()
        {
            while (true)
            { 
                Console.WriteLine("A)dd Product");
                Console.WriteLine("L)List Products");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine();


                char letter = Char.ToUpper(input[0]);

                if (letter == 'A')
                    return 'A';
                else if (letter == 'L')
                    return 'L';
                else if (letter == 'Q')
                    return 'Q';

                //Error
                Console.WriteLine("Please choose a valid option");
            };
        }

        static void Main2( string[] args)
        {

        }

    }
}
