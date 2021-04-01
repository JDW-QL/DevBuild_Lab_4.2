using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Week4_Lab_4._2
{
    class MoviePlaylist //MoviePlaylist class. Manages lists of type Movie
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
        public static int EditMenu(MoviePlaylist cur) // Displays Editor menu and returns user choice
        {
            Console.Clear();
            Console.WriteLine("\n                                        Welcome to the Playlist Editor v1.0 \n");

            Console.WriteLine("                                          Title                   Category");
            Console.WriteLine("                             ==========================================================");
            Console.WriteLine("                             |                                                        |");

            for (int i = 1; i < cur.PlayList.Count + 1; i++) // prints out playlist
            {
                Console.WriteLine(String.Format($"                             |       {i + ". ",4}{cur.PlayList[i - 1].Title,-25}{cur.PlayList[i - 1].Category,-20}|"));
            }
            Console.WriteLine("                             |                                                        |");
            Console.WriteLine("                             ==========================================================\n");
            int choice = 0;
            bool fPass = true;
            while (choice < 1 || choice > 8) // input validation 1-8
            {
                if (fPass == false)
                    Console.WriteLine("\nInvalid entry. Please make a selection between 1 and 8.");
                Console.WriteLine("                                     1: Add a Movie     2: Remove a Movie");
                Console.WriteLine("                                     3: Edit a Title    4: Edit a Category");
                Console.WriteLine("                                     5: Sort by Title   6: Sort by Category");
                Console.Write("                                     7: Clear Playlist  8: Exit Playlist:        ");
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            Console.WriteLine();
            return choice;


        }
        public static void EditPlaylist(MoviePlaylist currentPlaylist, int type)  // Playlist Editor. Processes users choice. Makes changes to playlist.
        {
            int choice = 0;
            int tempNum;
            string temp, temp2;
            bool fPass;
            while (choice != 8) // back to main menu when user selects 8
            {
                fPass = true;
                tempNum = 0;
                choice = EditMenu(currentPlaylist); // playlist is sent to EditMenu. choice is returned
                switch (choice) // Repeats until choice is false
                {
                    case 1: // Add a Movie
                        Console.Write("Please enter the name of the movie to add: ");
                        temp = Console.ReadLine();
                        Console.Write($"Please enter the category of the movie {temp}: ");
                        temp2 = Console.ReadLine();
                        currentPlaylist.PlayList.Add(new Movie(temp, temp2));
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
                        Console.Write($"\n What would you like to change {currentPlaylist.PlayList[tempNum - 1].Title} to: ");
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
                    case 5: // Sort by Title
                        currentPlaylist.PlayList.Sort((x,y) => x.Title.CompareTo(y.Title));
                        break;
                    case 6: // Sort by Category
                        currentPlaylist.PlayList.Sort((x, y) => x.Category.CompareTo(y.Category));
                        break;
                    case 7: // Clear Playlist
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
                    case 8: // Exit Edior
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
    class Movie // Movie class 
    {
        public string Title { get; set; } // Title getter, _title is private
        public string Category { get; set; } // Category getter, _category is private

        public static int x; // static variable that dictates how and where to save playlist after changes are made
        public static string currentFile; // User given filepath
        public static string fullFilePath; // file location indicator

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

        public Movie() // default constructor 
        {
            Title = "[Unknown]";
            Category = "[Unknown]";
        }

        public static void EditPlaylistMenu(MoviePlaylist currentPlaylist) // sends playlist and file location indicator to EditPlaylistMenu
        {
            MoviePlaylist.EditPlaylist(currentPlaylist, x);
        }
        public static void SetFilePath(string x) // sets fillFilePath to string x 
        {
            fullFilePath = x;
        }

    }

    class MainClass
    {
        public static MoviePlaylist CreateTextPlaylist(string fPath) // Reads in playlist from user given filepath
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
        public static MoviePlaylist CreateNewPlaylist(string newPath) // Creates new empty playlist in project directory with user given name
        {

            Movie.SetFilePath(newPath);
            List<Movie> textList = new List<Movie>();
            Movie.x = 3;
            Movie.currentFile = newPath;
            MoviePlaylist textPlaylist = new MoviePlaylist(textList);
            return textPlaylist;
        }

        public static MoviePlaylist CreateTextPlaylist() // Reads in playlist from MoviesText.txt in project directory.
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
        public static MoviePlaylist CreateDefaultPlaylist() // Creates hardcoded Default Playlist
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
        public static MoviePlaylist PlaylistLocation() // Retrieves playlist or creates a new one if user requests
        {
            int choice = 0;
            bool first = true;

            while (choice < 1 || choice > 4) // Input validation 1 - 4
            {
                if (!first)
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid Input. Please enter an integer 1-4.");
                }
                Console.WriteLine("How would you like to retrieve your playlist?\n");
                Console.WriteLine("1: Default Playlist");
                Console.WriteLine("2: Text file in project directory");
                Console.WriteLine("3: Enter filepath for .txt");
                Console.WriteLine("4. Create a new Playlist\n");
                Int32.TryParse(Console.ReadLine(), out choice);
                first = false;
            }
            switch (choice)
            {
                case 1: // Loads Hardcoded Default Playlist. This does NOT save after program use.
                    return CreateDefaultPlaylist();
                case 2:  // Pulls MoviesText.txt Playlist located in project directory. This DOES save after program use.
                    return CreateTextPlaylist();
                case 3: // Pulls a .txt Playlist from the users designated filepath. This DOES save after program use.
                    Console.Write("\nPlease enter the full filepath for the text file: ");
                    string filePath = Console.ReadLine();
                    if (!File.Exists(filePath)) // If .txt in filepath does not exist, loads Default Playlist.
                    {
                        Console.WriteLine("Invalid file path. Loading up the Default Playlist.");
                        Thread.Sleep(2000);
                        return CreateDefaultPlaylist();
                    }
                    return CreateTextPlaylist(filePath); // Sends file path entered by user to CreateTextPlaylist method.
                case 4:// Asks the user for a name for new Playlist .txt file. This is created in project directory. This DOES save after program use.
                    Console.Write("\nPlease enter neme of the new Playlist: ");
                    string fileName = Console.ReadLine();
                    fileName = $"C:\\Users\\JWeiner\\source\\repos\\Week4_Lab_4.2\\Week4_Lab_4.2\\bin\\Debug\\net5.0\\" + fileName + ".txt";
                    if (File.Exists(fileName)) // if file by user entered name already exists in this location, loads Default Playlist.
                    {
                        Console.Clear();
                        Console.WriteLine("\nThis Playlist already exists in project directory.");
                        Console.WriteLine("Please load this playlist or pick a new file name.");
                        Console.Write("Loading up the Default Playlist.");
                        for (int i = 0; i < 7; i++)
                        {
                            Thread.Sleep(1000);
                            Console.Write(".");
                        }
                       
                        return CreateDefaultPlaylist();
                    }
                    return CreateNewPlaylist(fileName); // Sends full filepath/name to CreateNewPlayList method.
                default:
                    return CreateDefaultPlaylist(); // Program has a cow if this isnt here, even though default cannot be accessed.
            }
        }
        public static void MainMenu() // Main menu for the program. Calls methods according to user input
        {
            int choice;
            string catChoice;
            bool firstPass;
            bool change;
            MoviePlaylist currentPlaylist; // current list of Movies we are working with
            List<string> mCats = new List<string>(); // Temporary list of unique Movie categories within the current list
            currentPlaylist = PlaylistLocation(); // Access the list of Movies the user wishes to work with

            do
            {
                choice = 0;
                firstPass = true;
                change = false;
                Console.Clear();
                Console.WriteLine("Welcome to the Movie List Application!\n");
                Console.WriteLine($"There are {currentPlaylist.PlayList.Count} movies in the list\n");
                mCats = MoviePlaylist.DisplayCategories(currentPlaylist); // displays unique categories sorted and returns list to mCats


                while (choice < 1 || choice > mCats.Count + 3) // Input validation. Int 1 - ( number of unique categories + 3 )
                {
                    if (firstPass == false)
                        Console.WriteLine($"\nInvalid input. Please enter an integer between 1 and {mCats.Count + 3}.");
                    Console.Write("Please make a selection: ");
                    Int32.TryParse(Console.ReadLine(), out choice);
                    firstPass = false;
                }
                if (choice == mCats.Count + 1) // Launch Playlist Editor
                {
                    Console.Clear();
                    Movie.EditPlaylistMenu(currentPlaylist);
                    change = true;
                }
                else if (choice == mCats.Count + 2) // Change Playlist
                {
                    Console.Clear();
                    currentPlaylist = PlaylistLocation();
                    change = true;
                }
                else if (choice == mCats.Count + 3) // Exit Program
                {
                    break;
                }
                else
                {
                    catChoice = mCats[choice - 1]; // translates number entered by user into correct category
                    MoviePlaylist.DisplayMoviesInCat(catChoice, currentPlaylist); // Displays all movies in playlist 
                }                                                                // matching users selection in alphabetical order
            } while (change == true || Repeat()); // If user went to Editor or changes playlist, they are NOT asked if they want to continue 
        }                                        // Calls method to see if user would like to continue, returns as true if yes, false if no ( back to Main ) 
        class Program
        {
            static void Main(string[] args) // Main includes welcome greeting and end program message. Calls Main Menu in between
            {

                Console.WriteLine("Welcome to the Movie List Application!\n"); // Program greeting message
                MainMenu();
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n                                 Thank you for using the Movie List Application!\n\n\n\n\n\n"); // end program message
                Console.WriteLine("                                                ****************");
                Console.WriteLine("                                                *              *");
                Console.WriteLine("                                                *   Goodybye!  *");
                Console.WriteLine("                                                *              *");
                Console.WriteLine("                                                ****************\n\n\n\n\n");
            }

        }
    }
}
