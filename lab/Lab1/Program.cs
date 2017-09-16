/*
 * ITSE-1430
 * Kevin Belknap
 * 9.15.2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

            do
            {
                char userSelection = GetUserInput();

                switch (userSelection)
                {
                    case 'L': ListMovie(); break;
                    case 'A': AddMovie(); break;
                    case 'R': RemoveMovie(); break;
                    case 'Q': quit = true; break;
                }
            }
            while (!quit);
        }
        
        static char GetUserInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("---------");
                Console.WriteLine("L)ist Movies");
                Console.WriteLine("A)dd Movies");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine().Trim();

                if (input != null && input.Length != 0)
                {
                    char letter = Char.ToUpper(input[0]);

                    if (letter == 'L')
                        return 'L';
                    else if (letter == 'A')
                        return 'A';
                    else if (letter == 'R')
                        return 'R';
                    else if (letter == 'Q')
                        return 'Q';
                }

                Console.WriteLine("Please choose a valid option");
            }
        }

       private static void ListMovie()
       {
            string message = "";

            if (String.IsNullOrEmpty(movieDescription))
                movieDescription = "[No Description Given]";

           if (String.IsNullOrEmpty(movieTitle))
           {
                message = "No movies available";
           }
           else
           {
                message = $"{movieTitle}\n{movieDescription}\nRun Length - {movieLength} mins\nStatus = {(movieOwned ? "Owned" : "Not Owned")}";
            }

            Console.WriteLine(message);
       }

        private static void AddMovie()
        {
            bool movieEntered = false;
            
            while (!movieEntered)
            {
                Console.Write("Enter a Title: ");
                movieTitle = Console.ReadLine();
                if (movieTitle.Trim().Length == 0)
                {
                    Console.WriteLine("You must enter a value");
                }
                else
                {
                    movieEntered = true;
                }
            }

            Console.Write("Enter an optional description: ");
            movieDescription = Console.ReadLine();

            Console.Write("Enter the optional length (in minutes): ");
            movieLength = ReadInt();

            Console.Write("Do you own this movie? (Y/N): ");
            movieOwned = ReadYesNo();
        }

        static void RemoveMovie()
        {
            Console.Write("Are you sure you want to remove the movie?(Y/N)\n");
            bool removeResponse = ReadYesNo();

            if (removeResponse)
            {
                movieTitle = "";
                movieDescription = "";
                movieLength = 0;
                movieOwned = false;
            }

            Console.Write("\n");
        }

        /// <summary>Reads a decimal from Console</summary>
        /// <returns>The decimal value</returns>
        static int ReadInt()
        {
            do
            {
                string input = Console.ReadLine();

                if (input.Trim().Length == 0)
                    return 0;
                else if (int.TryParse(input, out int result) && int.Parse(input) > -1)
                    return result;
                
                Console.WriteLine("You must enter a value >= 0");

            } while (true);
        }
        
        /// <summary>Reads a boolean from Console</summary>
        /// <returns>The boolean value</returns>
        static bool ReadYesNo()
        {
            do
            {
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y': return true;
                        case 'N': return false;
                    }
                }

                Console.WriteLine("Enter either Y or N");

            } while (true);
        }
        
        static string movieTitle;
        static string movieDescription;
        static int movieLength;
        static bool movieOwned;
    }
}
