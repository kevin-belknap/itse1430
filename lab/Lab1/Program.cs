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
    class Program
    {
        static bool showAgain;
        static Movies movie;

        static void Main(string[] args)
        {
            showAgain = true;
            
            while (showAgain == true)
            {
                DisplayMenu();
                GetUserInput();
            }
        }

        static void DisplayMenu()
        {
            string[] movieList = { "1. List Movies", "2. Add Movies", "3. Remove Movie", "4. Quit" };

            for (int i = 0; i < movieList.Count(); i++)
            {
                Console.WriteLine(movieList[i]);
            }
        }

        static void GetUserInput()
        {
            string userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ListMovie();
                    break;
                case "2":
                    AddMovie();
                    break;
                case "3":
                    RemoveMovie();
                    break;
                case "4":
                    showAgain = false;
                    break;
                default:
                    Console.WriteLine("Please enter a valid input\n\n");
                    break;
            }
        }

        static void ListMovie()
        {
            string ownedLabel;

            Console.Write("\n");

            if (movie != null)
            {
                if (movie.Owned)
                {
                    ownedLabel = "Owned\n";
                }
                else
                {
                    ownedLabel = "Not Owned\n";
                }

                Console.Write(movie.Title + "\n");
                if (movie.Description.Trim().Length > 0)
                {
                    Console.Write(movie.Description + "\n");
                }
                else
                {
                    Console.Write("N/A\n");
                }
                Console.Write("Run length - " + movie.Length + " mins\n");
                Console.Write("Status = " + ownedLabel + "\n\n");
            }
            else
            {
                Console.Write("No movies available\n\n");
            }
        }

        static void AddMovie()
        {
            string movieTitle = "";
            string movieDescription = "";
            int movieLength = -1;
            Nullable<bool> movieOwned = null;
            string userOwns = "";

            string movieLengthResponse = "";

            while (movieTitle.Trim().Length == 0)
            {
                Console.Write("Enter a Title\n");
                movieTitle = Console.ReadLine();

                if (movieTitle.Trim().Length == 0)
                {
                    Console.Write("You must enter a value\n");
                }
            }

            Console.Write("Enter an optional description\n");
            movieDescription = Console.ReadLine();

            while (movieLength < 0)
            {
                Console.Write("\nEnter the optional length (in minutes)\n");
                try
                {
                    movieLengthResponse = Console.ReadLine();

                    movieLength = int.Parse(movieLengthResponse);

                    if (movieLength < 0)
                    {
                        Console.Write("You must enter a value >= 0\n");
                    }
                }
                catch
                {
                    if (movieLengthResponse == "")
                    {
                        movieLength = 0;
                    }
                    else
                    {
                        Console.Write("You must enter a value >= 0\n");
                    }
                }
            }

            while (!movieOwned.HasValue)
            {
                Console.Write("Do you own this movie? (Y/N)\n");
                userOwns = Console.ReadLine();

                if (userOwns.ToUpper() == "Y")
                {
                    movieOwned = true;
                }
                else if (userOwns.ToUpper() == "N")
                {
                    movieOwned = false;
                }
                else
                {
                    Console.Write("Please enter Y or N\n");
                }
            }

            movie = new Movies(
               movieTitle,
               movieDescription,
               movieLength,
               movieOwned.Value
            );
        }

        static void RemoveMovie()
        {
            Console.Write("Are you sure you want to remove the movie?(Y/N)\n");
            string removeResponse = Console.ReadLine();

            while (removeResponse.ToUpper() != "Y" && removeResponse.ToUpper() != "N")
            {
                Console.Write("\nPlease enter Y or N\n\n");
                removeResponse = Console.ReadLine();
            }

            if (removeResponse.ToUpper() == "Y")
            {
                movie = null;
            }

            Console.Write("\n");
        }
    }
}
