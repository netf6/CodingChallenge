using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodingChallenge
{
    internal class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {

                // menu - selection of functions to run
                while (true)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Select which function to run: ");
                    Console.WriteLine("1. Find the common base of two different path strings");
                    Console.WriteLine("2. Find the closest word");
                    Console.WriteLine("3. Find instantaneous speed of a vehicle");
                    Console.WriteLine("4. Exit");

                    string input = Console.ReadLine();
                    //if input is not a number, ask for input again
                    if (!int.TryParse(input, out int n))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    int choice = Convert.ToInt32(input);

                    switch (choice)
                    {
                        case 1:
                            FindCommonBase();
                            break;
                        case 2:
                            FindClosestWord();
                            break;
                        case 3:
                            FindSpeed();
                            break;
                        case 4:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }

                    if (choice == 4)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// Find the common base of two different path strings.
        /// </summary>
        public static void FindCommonBase()
        {
            try
            {
                Console.WriteLine("Let's find the common base of two different path strings...");

                Console.WriteLine("Enter the first path string (eg. /home/daniel/memes): ");
                String path1 = Console.ReadLine();

                Console.WriteLine("Enter the second path string (eg. /home/daniel/work): ");
                String path2 = Console.ReadLine();

                String result = Sampler.RelativeToCommonBase(path1, path2);

                if (result == "path 1 is null" || result == "path 2 is null")
                {
                    Console.WriteLine("Paths cannot be blank.");
                }
                else if (result == "Invalid character in path 1.")
                {
                    Console.WriteLine("Invalid character in path 1.");
                }
                else if (result == "Invalid character in path 2.")
                {
                    Console.WriteLine("Invalid character in path 2.");
                }
                else if (result == "There is no common base.")
                {
                    Console.WriteLine("There is no common base.");
                }
                else
                {
                    Console.WriteLine("The common base of the two paths is: " + Sampler.RelativeToCommonBase(path1, path2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// Find the closest word.
        /// </summary>
        public static void FindClosestWord()
        {
            try
            {
                while (true)
                {                    
                    Console.Clear();
                    Console.WriteLine("Let's find a word that is most like the word you enter...");
                    Console.WriteLine("Enter a word (Press 'Esc' to exit):");

                    string wordToCompare = Console.ReadLine();

                    //while the word is blank, ask for input again
                    while (wordToCompare == "")
                    {
                        Console.WriteLine("Word cannot be blank. Please enter a word:");
                        wordToCompare = Console.ReadLine();
                    }

                    Console.WriteLine("Would you like to compare \"" + wordToCompare + "\" to: " 
                        + Environment.NewLine + " (1) use a pre-loaded dictionary, or " 
                        + Environment.NewLine + " (2) enter your own list of strings? "
                        + Environment.NewLine + " Enter 1 or 2 (3 to exit):");
                    string selection = Console.ReadLine();

                    //todo: add validation for input
                    int choice = Convert.ToInt32(selection);

                    String result = "";

                    switch (choice)
                    {
                        case 1:
                            String[] dictionary = { "apple", "banana", "blueberry", "cherry", "date", "elderberry", "fig", "grape", "honeydew", "jackfruit", "kiwi", "lemon", "mango", "nectarine", "orange", "papaya", "quince", "raspberry", "strawberry", "tangerine", "vanilla", "watermelon", "yellow watermelon", "zucchini" };
                            result = Sampler.ClosestWord(wordToCompare, dictionary);
                            
                            //print the result
                            if (result == "")
                            {
                                Console.WriteLine("The word that is most like \"" + wordToCompare + "\" is: " + "No word found.");
                            }
                            else
                            {
                                Console.WriteLine("The word that is most like \"" + wordToCompare + "\" is: " + result);
                            }

                            Console.WriteLine("Press any key to continue...");
                            Console.ReadLine();
                            
                            break;
                        case 2:                            
                            Console.WriteLine("Enter a list of strings separated by commas:");
                            string inputList = Console.ReadLine();

                            if (inputList == "")
                            {
                                Console.WriteLine("List cannot be blank.");
                                break;
                            }
                            
                            String[] customList = inputList.Split(',');

                            if (customList.Length == 0)
                            {
                                Console.WriteLine("List cannot be blank.");
                                break;
                            }

                            result = Sampler.ClosestWord(wordToCompare, customList);

                            //print the result
                            Console.WriteLine("The word that is most like \"" + wordToCompare + "\" is: " + result);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadLine();

                            break;
                        case 3:
                            Console.Clear();
                            break;
                        default:                            
                            Console.WriteLine("Invalid choice");                            
                            break;
                    }

                    //exit the loop if the user chooses to exit
                    if (choice == 3)
                    {
                        break;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// Get the points in time from PointsInTime.xml
        /// </summary>
        /// <returns></returns>
        public static List<Sampler.PointInTime> GetPointsInTime()
        {
            Console.WriteLine("Reading PointsInTime.xml and loading predefined points...");
            List<Sampler.PointInTime> pointsInTime = new List<Sampler.PointInTime>();
            try
            {
                XDocument doc = XDocument.Load("PointsInTime.xml");
                foreach (XElement element in doc.Descendants("PointInTime"))
                {
                    double x = Convert.ToDouble(element.Element("X").Value);
                    double y = Convert.ToDouble(element.Element("Y").Value);
                    double timestamp = Convert.ToDouble(element.Element("Time").Value);
                    Console.WriteLine("X: " + x + " Y: " + y + " Time: " + timestamp + " (" + DateTimeOffset.FromUnixTimeSeconds((long)timestamp).ToString("yyyy-MM-dd HH:mm:ss:fff") +")");

                    pointsInTime.Add(new Sampler.PointInTime(x, y, timestamp));
                }

                Console.WriteLine("Predefined points loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while reading PointsInTime.xml: " + e.Message);
            }
            return pointsInTime;
        }


        /// <summary>
        /// Find the instantaneous speed of a Vehicle using Sampler.SpeedAtTime using km/h
        /// </summary>
        public static void FindSpeed()
        {
            try
            {
                Console.Clear();
                //get the points in time from PointsInTime.xml
                Sampler.PointInTime[] pointsInTime = GetPointsInTime().ToArray();
                
                Console.WriteLine("Let's find the instantaneous speed of a vehicle...");
                Console.WriteLine("PointsInTime.xml contains a list of predefined points");
                
                Console.WriteLine("Enter a count in seconds from when the vehicle set off: (eg 30) ");
                String time = Console.ReadLine();

                // if the time is blank, ask for input again
                while (time == "")
                {
                    Console.WriteLine("Time cannot be blank. Please enter a time in seconds after the first starting point:");
                    time = Console.ReadLine();
                }

                //if the time is not a number, ask for input again
                if (!double.TryParse(time, out double n))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }

                double speed = Sampler.SpeedAtTime(Convert.ToDouble(time), pointsInTime);
                if (speed == -1)
                {
                    Console.WriteLine("Time entered is before the setoff point.");
                    return;
                }
                
                if (speed == -2)
                {
                    Console.WriteLine("Time entered is after the last arrival point.");
                    return;
                }

                if (speed == -3)
                {
                    Console.WriteLine("There are no predefined Points - ensure PointsInTime.xml is populated and accessible in the local directory.");
                    return;
                }

                if (speed == -4)
                {
                    Console.WriteLine("There are not enough points to calculate speed.");
                    return;
                }

                Console.WriteLine("The instantaneous speed of the vehicle is: " + speed + " units/s");
                
          
                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }


    }
}
