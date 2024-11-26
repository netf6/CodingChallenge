using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge
{
    public class Sampler
    {
        // ---- 1.
        /*
          Write a function that accepts two Paths and returns the portion of the first Path that is not
          common with the second, which is to say portion of the first path starting from where the two
          paths diverged.

          For example, RelativeToCommonBase("/home/daniel/memes", "/home/daniel/work") should
          produce "/home/daniel".
        */
        public static String RelativeToCommonBase(String path1, String path2)
        {
            string result = "";

            try 
            {
                // check if the paths are null
                if (path1 == null || path1 == "")
                {
                    result = "path 1 is null";
                }
                if (path2 == null || path2 == "")
                {
                    result = "path 1 is null";
                }

                //remove any double slashes
                path1 = path1.Replace("//", "/");
                path2 = path2.Replace("//", "/");

                //remove any trailing or leading spaces
                path1 = path1.Trim();
                path2 = path2.Trim();

                //check for < > : " / | ? * which are invalid characters in a path 
                if (path1.Contains("<") || path1.Contains(">") || path1.Contains(":") || path1.Contains("\"") || path1.Contains("|") || path1.Contains("?") || path1.Contains("*"))
                {
                    result = "Invalid character in path 1";
                    return result;
                }
                if (path2.Contains("<") || path2.Contains(">") || path2.Contains(":") || path2.Contains("\"") || path2.Contains("|") || path2.Contains("?") || path2.Contains("*"))
                {
                    result = "Invalid character in path 2";
                    return result;
                }

                //if paths do start with a slash, add one
                if (!path1.StartsWith("/"))
                {
                    path1 = "/" + path1;
                }
                if (!path2.StartsWith("/"))
                {
                    path2 = "/" + path2;
                }

                //if path 1 is the same as path 2 - best case - minimise processing
                if (path1.Equals(path2))
                {
                    result = path1;
                    return result;
                }

                // split the paths into folders - assuming windows paths
                String[] path1folders = path1.Split('/');
                String[] path2folders = path2.Split('/');

                // find the common base
                int i = 0;
                foreach (String folder in path1folders)
                {
                    // check if the folder is the same as the folder in the second path, disregarding case (assuming Windows system)                   
                    if (folder.Equals(path2folders[i],StringComparison.OrdinalIgnoreCase))
                    {
                        result += folder + "/";
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                //remove the last slash
                result = result.Substring(0,result.Length - 1);

                if (result == "")
                {
                    result = "There is no common base.";
                }
            }
            catch (Exception e)
            {
                result = "An error occurred: " + e.Message;
            }

            return result;
        }

        /// <summary>
        /// Function to calculate the Levenshtein distance.
        /// This distance measures the "edit distance," or how many edits(insertions, deletions, or substitutions)
        /// are needed to change one word into another.
        /// Credit to ChatGPT for the suggestion of using this algorithm.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static int GetLevenshteinDistance(string a, string b)
        {
            // create a 2D array to store the distances
            int[,] dp = new int[a.Length + 1, b.Length + 1];

            // initialize the first row and column
            for (int i = 0; i <= a.Length; i++)
            {
                dp[i, 0] = i;
            }
            // initialize the first row and column
            for (int j = 0; j <= b.Length; j++)
            {
                dp[0, j] = j;
            }
            // fill in the rest of the array
            for (int i = 1; i <= a.Length; i++)
            {
                // iterate through the second word
                for (int j = 1; j <= b.Length; j++)
                {
                    // if the characters are the same, the cost is 0
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    // calculate the minimum cost
                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }
            // return the bottom-right value of the array
            return dp[a.Length, b.Length];
        }

            

        // ---- 2.
        /*
          Write a function that accepts a string as the first parameter, and a
          list of strings as the second parameter, and returns a string from the
          list that is "most like" the first string. The choice of algorithm 
          is yours.
        */
        public static String ClosestWord(String word, String[] possibilities)
        {
            // if the list of possibilities is empty, return an empty string
            string result = possibilities[0];

            // if the list of possibilities is empty, return that information
            if (possibilities.Length == 0)
            {
                result = "The list of possibilities is empty.";
                return result;
            }

            // if the list of possibilities has only one word, return that word
            if (possibilities.Length == 1)
            {
                result = possibilities[0];
                return result;
            }

            int minDistance = GetLevenshteinDistance(word, result);

            // find the word with the smallest Levenshtein distance
            foreach (var targetWord in possibilities)
            {
                Console.WriteLine("comparing: " + targetWord);

                int distance = GetLevenshteinDistance(word, targetWord);

                // if the distance is smaller than the current minimum distance, update the minimum distance and the most similar word
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = targetWord;
                }
            }

            return result;
        }

        // ----3.
        /*
            Pretend there is a vehicle traveling along a path. The path is represented
            by a list of x, y points and a unix timestamp at that point 
            (the PointIntime struct).  

            eg. 
            1 PointInTime(1,1, 1732595439) 
            2 PointInTime(2,2, 1732595499) (+60 sec)
            3 PointInTime(3,3, 1732595559) (+60 sec)       
            3 PointInTime(4,4, 1732595619) (+60 sec)  
          
            The vehicle travels
            in straight lines between those points and passes through each point at
            the corresponding timestamp. 
        
            Given this list of points and timestamps,
            and a time seconds (relative to the first timestamp), write a function
            that returns the instantaneous speed of the fake vehicle at that timestamp.
        
            formulas:
            distance = speed * time => speed = distance / time  
            distance between points = SQRT((X2 - X1)^2 + (Y2 - Y1)^2)

            If the time is less than the first timestamp, return -1.
            If the time is greater than the last timestamp, return -2.
         */
        public static double SpeedAtTime(double atTime, PointInTime[] path)
        {
            double speed = -4;
            try
            {
                // check where timestamp falls in the list of points

                if (path.Length == 0)
                {
                    speed = -3;
                    return speed;
                }

                // add the time to the first timestamp
                atTime = path[0].Timestamp + atTime;
                Console.WriteLine("atTime: " + atTime + " (" + DateTimeOffset.FromUnixTimeSeconds((long)atTime).ToString("yyyy-MM-dd HH:mm:ss:fff") + ")"); 

                int i = 0;
                int max = path.Length - 1;
                foreach (PointInTime point in path)
                {
                    //avoid out of bounds
                    if (i == max)
                    {
                        break;
                    }

                    // check if the time is less than the first timestamp - out of bounds
                    if (atTime < path[0].Timestamp)
                    {
                        speed = -1;
                        return speed;
                    }
                    // check if the time is greater than the last timestamp - out of bounds
                    if (atTime > path[max].Timestamp)
                    {
                        speed = -2;
                        return speed;
                    }

                    // check if the time is between two timestamps
                    if (atTime >= point.Timestamp && atTime <= path[i + 1].Timestamp)
                    {
                        // calculate the distance between the two points
                        double distance =
                            Math.Sqrt(
                                (path[i + 1].X - point.X) * (path[i + 1].X - point.X) // -a * -a = a^2 - no need to check for negative values
                                    +
                                (path[i + 1].Y - point.Y) * (path[i + 1].Y - point.Y)
                                );                                
                        
                        // calculate the time between the two points
                        double time = path[i + 1].Timestamp - point.Timestamp;

                        // check if the time is 0 -  avoid division by 0
                        if (time == 0)
                        {
                            speed = -1;
                            return speed;
                        }
                        // calculate the speed
                        speed = distance / time;
                        
                        return speed;
                    }

                    i++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return -1;
            }
            return speed;
        }

        /*
          Struct to represent a point in time with x, y coordinates and a timestamp.
        */
        public struct PointInTime
        {            
            public PointInTime(double x, double y, double timestamp)
            {
                X = x;
                Y = y;
                Timestamp = timestamp;
            }

            public double X { get; }
            public double Y { get; }
            public double Timestamp { get; }

            public override string ToString() => $"({X}, {Y}, {Timestamp})";
        }
    }
}
