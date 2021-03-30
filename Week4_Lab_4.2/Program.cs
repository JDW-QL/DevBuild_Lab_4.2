using System;
using System.Collections.Generic;

namespace Week4_Lab_4._2
{
    class Movie
    {
        public string Title { get; } // Title getter, _title is private
        public string Category { get; } // Category getter, _category is private

        public Movie(string mTitle, string mCategory) // Constructor sets Title and Category
        {
            Title = mTitle;
            Category = mCategory;
        }

        public static List<string> DisplayCategories(List<Movie> mList) // Displays a list of all unique movie categories, returns this list to main 
        {
            List<string> mCats = new List<string>(); // list of unique categories
            foreach (var scan in mList) // goes through each movie and adds category to mCats list if it isn't on it yet
            {
                if (!mCats.Contains(scan.Category))
                    mCats.Add(scan.Category);
            }
            mCats.Sort(); // Alphabetize mCats List
            int count = 1;
            foreach (string i in mCats) // Displays each uniqe category(sorted) with a number count in front for the user to choose from in main
            {
                Console.WriteLine(count + ". " + i);
                count++;
            }
            Console.WriteLine();
            return mCats; // Returns list of unique categories in alpha order
        }

        public static void DisplayMoviesInCat(string selection, List<Movie> mList) // Displays all movies in playlist that matches users selection in alphabetical order
        {
            int count;
            List<string> moviesInCat = new List<string>();

            Console.WriteLine("\nMovies matching this category: ");

            foreach (var scan in mList) // if category matches selection, add to moviesInCat list
            {
                if (scan.Category == selection)
                    moviesInCat.Add(scan.Title);
            }

            moviesInCat.Sort();
            count = 1;
            foreach (string mScan in moviesInCat) // displays moviesInCat list is alphabetical order
            {
                Console.WriteLine($"{count}. {mScan}");
                count++;
            }
        }
    }

    class MainClass
    {
        public static bool Repeat() // Asks user to continue, returns true(y), false(n)
        {
            string yn = "";
            while (yn != "y" && yn != "n")
            {
                Console.Write("\nWould you like to continue? (y/n): ");
                yn = Console.ReadLine().ToLower();
            }
            if (yn == "n")
                return false;
            return true;
        }
        class Program
        {
            static void Main(string[] args)
            {
                int choice;                            //initialization block
                string catChoice;
                bool firstPass, cont;
                List<string> mCats = new List<string>();
                List<Movie> playlist = new List<Movie>();
                
                playlist.Add(new Movie("Toy Story", "Animated"));      // Literal movie enteries. Dynamic additions accecptable
                playlist.Add(new Movie("Up", "Animated"));            // New movies with new categories can be hardcoded and program
                playlist.Add(new Movie("Finding Nemo", "Animated")); // will adjust numbers/menus accordingly

                playlist.Add(new Movie("Moonlight", "Drama"));
                playlist.Add(new Movie("Spotlight", "Drama"));
                playlist.Add(new Movie("Roma", "Drama"));

                playlist.Add(new Movie("Get Out", "Horror"));
                playlist.Add(new Movie("A Quiet Place", "Horror"));
                playlist.Add(new Movie("Candyman", "Horror"));

                playlist.Add(new Movie("Gravity", "SciFi"));
                playlist.Add(new Movie("Arrival", "SciFi"));
                playlist.Add(new Movie("Ex Machina", "SciFi"));

                Console.WriteLine("Welcome to the Movie List Application!"); // Program greeting message

                do // loops until user decides to no longer continue
                {
                    Console.WriteLine($"There are {playlist.Count} movies in the list\n");
                    mCats = Movie.DisplayCategories(playlist); // displays unique categories sorted and returns list to mCats

                    choice = 0;
                    firstPass = true;
                    while (choice < 1 || choice > mCats.Count) // Input validation
                    {
                        if (firstPass == false)
                            Console.WriteLine($"\nInvalid input. Please enter an integer between 1 and {mCats.Count}.");
                        Console.Write("What category are you interested in? ");
                        Int32.TryParse(Console.ReadLine(), out choice);
                        firstPass = false;
                    }

                    catChoice = mCats[choice - 1]; // turns number entered by user into correct category
                    Movie.DisplayMoviesInCat(catChoice, playlist); // Displays all movies in playlist matching users selection in alphabetical order
                    cont = Repeat(); // callse method to see if user would like to continue, returns as true if yes, false if no   
                    Console.Clear(); // Clear screen
                } while (cont); // loops while user wishes to continue

                Console.WriteLine("Thank you for using the Movie List Application!"); // end program message
                Console.WriteLine("\nGoodybye!\n");
            }
        }
    }
}
