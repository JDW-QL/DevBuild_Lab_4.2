using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Week4_Lab_4._2
{
    class MoviePlaylist
    {
        public List<Movie> PlayList { get; set; } // Playlist getter, _playlist is private

        public MoviePlaylist(List<Movie> playlist) // constructor takes in List of Movie types
        {
            PlayList = playlist;
        }


        public static List<string> DisplayCategories(MoviePlaylist playlist) // Displays a list of all unique movie categories, returns this list to main 
        {
            List<string> mCats = new List<string>(); // list of unique categories
            foreach (var scan in playlist.PlayList) // goes through each movie and adds category to mCats list if it isn't on it yet
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
            Console.WriteLine($"\n{count}. Edit current Playlist");
            Console.WriteLine($"{count + 1}. Change Playlist");
            Console.WriteLine($"{count + 2}. Exit Program");
            Console.WriteLine();
            return mCats; // Returns list of unique categories in alpha order
        }

        public static void DisplayMoviesInCat(string selection, MoviePlaylist mList) // Displays all movies in playlist that matches users selection in alphabetical order
        {
            int count;
            List<string> moviesInCat = new List<string>();

            Console.WriteLine("\nMovies matching this category: ");

            foreach (var scan in mList.PlayList) // if category matches selection, add to moviesInCat list
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
        public static int EditMenu(MoviePlaylist cur)
        {
            Console.Clear();
            Console.WriteLine("\n                                        Welcome to the Playlist Editor v1.0 \n");

            Console.WriteLine("                                          Title                   Category");
            Console.WriteLine("                             ==========================================================");
            Console.WriteLine("                             |                                                        |");

            for (int i = 1; i < cur.PlayList.Count + 1; i++)
            {
                Console.WriteLine(String.Format($"                             |       {i+". ",4}{cur.PlayList[i - 1].Title,-25}{cur.PlayList[i - 1].Category,-20}|"));
            }
            Console.WriteLine("                             |                                                        |");
            Console.WriteLine("                             ==========================================================\n");
            int choice = 0;
            bool fPass = true;
            while (choice < 1 || choice > 6)
            {
                if (fPass == false)
                    Console.WriteLine("\nInvalid entry. Please make a selection between 1 and 4.");
                Console.WriteLine("                                     1: Add a Movie     2: Remove a Movie");
                Console.WriteLine("                                     3: Edit a Title    4: Edit a Category");
                Console.Write("                                     5: Clear Playlist  6: Exit Playlist editor:    ");
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            Console.WriteLine();
            return choice;


        }
        public static void EditPlaylist(MoviePlaylist currentPlaylist, int type)
        {
            int choice = 0;
            int tempNum;
            string temp, temp2;
            bool fPass;
            while (choice != 6)
            {
                fPass = true;
                tempNum = 0;
                choice = EditMenu(currentPlaylist);
                switch (choice)
                {
                    case 1: // Add a Movie
                        Console.Write("Please enter the name of the movie to add: ");
                        temp = Console.ReadLine();
                        Console.Write($"Please enter the category of the movie {temp}: ");
                        temp2 = Console.ReadLine();
                        currentPlaylist.PlayList.Add(new Movie(temp,temp2));
                        break;

                    case 2: // Remove a Movie
                        while (tempNum < 1 || tempNum > currentPlaylist.PlayList.Count + 1)
                        {
                            if (!fPass)
                                Console.WriteLine($"Invalid number. Please enter a number between 1 and {currentPlaylist.PlayList.Count}");
                            Console.Write("Please enter the number for the movie you wish to remove: ");
                            fPass = false;
                            Int32.TryParse(Console.ReadLine(), out tempNum);
                        }
                        currentPlaylist.PlayList.RemoveAt(tempNum - 1);
                        break;
                    case 3: // Edit Title
                        while (tempNum < 1 || tempNum > currentPlaylist.PlayList.Count + 1)
                        {
                            if (!fPass)
                                Console.WriteLine($"Invalid number. Please enter a number between 1 and {currentPlaylist.PlayList.Count}");
                            Console.Write("Please enter the number for the title you wish to change: ");
                            fPass = false;
                            Int32.TryParse(Console.ReadLine(), out tempNum);
                        }
                        Console.Write($"\n What would you like to change {currentPlaylist.PlayList[tempNum -1].Title} to: ");
                        temp = Console.ReadLine();
                        currentPlaylist.PlayList[tempNum - 1].Title = temp;
                        break;
                    case 4: // Edit Category
                        while (tempNum < 1 || tempNum > currentPlaylist.PlayList.Count + 1)
                        {
                            if (!fPass)
                                Console.WriteLine($"Invalid number. Please enter a number between 1 and {currentPlaylist.PlayList.Count}");
                            Console.Write("Please enter the number for the category you wish to change: ");
                            fPass = false;
                            Int32.TryParse(Console.ReadLine(), out tempNum);
                        }
                        Console.Write($"\n What would you like to change {currentPlaylist.PlayList[tempNum - 1].Category} to: ");
                        temp = Console.ReadLine();
                        currentPlaylist.PlayList[tempNum - 1].Category = temp;
                        break;
                    case 5: // Clear Playlist
                        Console.WriteLine("Are you sure you would like to clear this playlist? This cannot be undone.");
                        Console.WriteLine("Note: The default playlist can be retrieved again from the change playlist menu.");
                        Console.Write("\nPlease type \"Clear Playlist\" to confirm or anything else to decline: ");
                        temp = Console.ReadLine();
                        Console.Clear();
                        if (temp == "Clear Playlist")
                        {
                            currentPlaylist.PlayList.Clear();
                            Console.WriteLine("\nClearing active playlist......");
                        }
                        else
                            Console.WriteLine("\nThe active playlist has NOT been cleared");
                        Thread.Sleep(2000);
                        break;
                    case 6: // Exit Edior
                        break;
                }

            }
            TextWriter progressText;
            if (type == 2)
            {
                progressText = new StreamWriter("MoviesText.txt");

                foreach (Movie x in currentPlaylist.PlayList)
                {
                    progressText.WriteLine(x.Title + "," + x.Category);
                }
                progressText.Close();
            }
            else if (type == 3)
            {
                progressText = new StreamWriter(Movie.fullFilePath);

                foreach (Movie x in currentPlaylist.PlayList)
                {
                    progressText.WriteLine(x.Title + "," + x.Category);
                }
                progressText.Close();
            }
        }
    }
    class Movie
    {
        public string Title { get; set; } // Title getter, _title is private
        public string Category { get; set; } // Category getter, _category is private

        public static int x;
        public static string currentFile;
        public static string fullFilePath;

        public Movie(string mTitle, string mCategory) // Constructor sets Title and Category
        {
            Title = mTitle;
            Category = mCategory;
        }

        public Movie(string mTitle) // default category constructor 
        {
            Title = mTitle;
            Category = "[Unknown]";
        }

        public Movie() // default category constructor 
        {
            Title = "[Unknown]";
            Category = "[Unknown]";
        }

        public static void EditPlaylistMenu(MoviePlaylist currentPlaylist)
        {
            MoviePlaylist.EditPlaylist(currentPlaylist, x);
        }
        public static void SetFilePath(string x)
        {
            fullFilePath = x;
        }

    }

    class MainClass
    {
        public static MoviePlaylist CreateTextPlaylist(string fPath)
        {
            string[] lines;
            string[] words;
            Movie.SetFilePath(fPath);

            List<Movie> textList = new List<Movie>();
            lines = File.ReadAllLines(fPath);
            Movie.x = 3;
            Movie.currentFile = fPath;

            foreach (string line in lines)
            {
                words = line.Split(',');
                textList.Add(new Movie(words[0], words[1]));
            }

            MoviePlaylist textPlaylist = new MoviePlaylist(textList);
            return textPlaylist;
        }
        public static MoviePlaylist CreateTextPlaylist()
        {
            List<Movie> textList = new List<Movie>();
            string[] lines = File.ReadAllLines("MoviesText.txt");
            string[] words;
            Movie.x = 2;

            foreach (string line in lines)
            {
                words = line.Split(',');
                textList.Add(new Movie(words[0], words[1]));
            }

            MoviePlaylist textPlaylist = new MoviePlaylist(textList);
            return textPlaylist;
        }
        public static MoviePlaylist CreateDefaultPlaylist()
        {
            Movie.x = 1;
            List<Movie> defaultList = new List<Movie>();   //  new MoviePlaylist

            defaultList.Add(new Movie("Toy Story", "Animated"));
            defaultList.Add(new Movie("Up", "Animated"));
            defaultList.Add(new Movie("Finding Nemo", "Animated"));

            defaultList.Add(new Movie("Moonlight", "Drama"));
            defaultList.Add(new Movie("Spotlight", "Drama"));
            defaultList.Add(new Movie("Roma", "Drama"));

            defaultList.Add(new Movie("Get Out", "Horror"));
            defaultList.Add(new Movie("A Quiet Place", "Horror"));
            defaultList.Add(new Movie("Candyman", "Horror"));

            defaultList.Add(new Movie("Gravity", "SciFi"));
            defaultList.Add(new Movie("Arrival", "SciFi"));
            defaultList.Add(new Movie("Ex Machina", "SciFi"));

            defaultList.Add(new Movie("Jack@$$", "Comedy"));
            defaultList.Add(new Movie("Team America", "Comedy"));
            defaultList.Add(new Movie("Anchor Man", "Comedy"));

            defaultList.Add(new Movie("Fantasia"));
            defaultList.Add(new Movie());

            MoviePlaylist defaultPlaylist = new MoviePlaylist(defaultList);
            return defaultPlaylist;
        }
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
        public static MoviePlaylist PlaylistLocation()
        {
            int choice = 0;
            bool first = true;

            while (choice < 1 || choice > 3)
            {
                if (!first)
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid Input. Please enter an integer 1-3.");
                }
                Console.WriteLine("How would you like to retrieve your playlist?\n");
                Console.WriteLine("1: Default List");
                Console.WriteLine("2: Text file in bin");
                Console.WriteLine("3: Enter filepath for .txt\n");
                Int32.TryParse(Console.ReadLine(), out choice);
                first = false;
            }
            switch (choice)
            {
                case 1:
                    return CreateDefaultPlaylist();
                case 2:
                    return CreateTextPlaylist();
                default:
                    Console.Write("\nPlease enter the full filepath for the text file: ");
                    string filePath = Console.ReadLine();
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("Invalid file path. Loading up the Default Playlist.");
                        Thread.Sleep(2000);
                        return CreateDefaultPlaylist();
                    }
                        return CreateTextPlaylist(filePath);
            }
        }
        public static void MainMenu()
        {
            int choice;
            string catChoice;
            bool firstPass;
            bool change;
            MoviePlaylist currentPlaylist;
            List<string> mCats = new List<string>();
            currentPlaylist = PlaylistLocation();

            do
            {
                choice = 0;
                firstPass = true;
                change = false;
                Console.Clear();
                Console.WriteLine("Welcome to the Movie List Application!\n"); // Program greeting message
                Console.WriteLine($"There are {currentPlaylist.PlayList.Count} movies in the list\n");
                mCats = MoviePlaylist.DisplayCategories(currentPlaylist); // displays unique categories sorted and returns list to mCats


                while (choice < 1 || choice > mCats.Count + 3) // Input validation
                {
                    if (firstPass == false)
                        Console.WriteLine($"\nInvalid input. Please enter an integer between 1 and {mCats.Count + 3}.");
                    Console.Write("Please make a selection: ");
                    Int32.TryParse(Console.ReadLine(), out choice);
                    firstPass = false;
                }
                if (choice == mCats.Count + 1)
                {
                    Console.Clear();
                    Movie.EditPlaylistMenu(currentPlaylist);
                    change = true;
                }
                else if (choice == mCats.Count + 2)
                {
                    Console.Clear();
                    currentPlaylist = PlaylistLocation();
                    change = true;
                }
                else if (choice == mCats.Count + 3)
                {
                    break;
                }
                else
                {
                    catChoice = mCats[choice - 1]; // turns number entered by user into correct category
                    MoviePlaylist.DisplayMoviesInCat(catChoice, currentPlaylist); // Displays all movies in playlist matching users selection in alphabetical order
                }
            } while (change == true || Repeat()); // Calls method to see if user would like to continue, returns as true if yes, false if no   

        }
        class Program
        {
            static void Main(string[] args)
            {

                Console.WriteLine("Welcome to the Movie List Application!\n"); // Program greeting message
                MainMenu();
                Console.Clear();
                Console.WriteLine("Thank you for using the Movie List Application!"); // end program message
                Console.WriteLine("\nGoodybye!\n");
            }

        }
    }
}
