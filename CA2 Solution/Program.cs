using System.ComponentModel.Design;

namespace CA2_Solution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] bookIds = new string[10];
            double[] reviewScores = new double[10];

            string bookId;
            double reviewScore;
            int count = 0;

            Console.WriteLine("Please enter a Book Id:");
            bookId = Console.ReadLine();
            while ((bookId != "-999") && (count < 10))
            {
                if (IsValidBookId(bookId))
                {
                    Console.WriteLine("Please enter a Review Score:");
                    reviewScore = GetValidReviewScore();

                    bookIds[count] = bookId;
                    reviewScores[count]= reviewScore;
                    count++;

                    Console.WriteLine($"{reviewScore} is the score for:  {bookId}");
                }
                else
                {
                    Console.WriteLine("Invalid Book ID");
                }
                Console.WriteLine("Please enter a Book Id:");
                bookId = Console.ReadLine();
            }

            DisplayReport(bookIds, reviewScores, count);

          
        }

        static bool IsValidBookId(string bookId)
        {
         
            bookId = bookId.Trim();
            if ((bookId != null) && (bookId.Length == 9) && (bookId[0] == 'B') && (int.TryParse(bookId.Substring(1, 8), out int number)))
            {
                return true;
            }

            return false;

        }
        static double GetValidReviewScore()
        {
            double reviewScore = 0;
            while (!double.TryParse(Console.ReadLine(), out reviewScore) || ((reviewScore < 0) || (reviewScore > 5)))
            {
                Console.WriteLine("Please enter a valid review score between 0 and 5");
            }
            return reviewScore;
        }

        static string GetRatingMessage(double reviewScore)
        {
          
            string[] messages = { "poor", "fair", "good", "very good", "excellent" };

            int index = GetRatingIndex(reviewScore);

            if (index != -1)
            {
                return messages[index];
            }

            return "invalid rating";

        }
        static int GetRatingIndex(double reviewScore)
        {
            int index = -1;
            switch (reviewScore)
            {
                case double rS when (rS >=0 && rS <= 1): index = 0; break;
                case double rS when (rS > 1 && rS <= 2): index = 1; break;
                case double rS when (rS > 2 && rS <= 3): index = 2; break;
                case double rS when (rS > 3 && rS <= 4): index = 3; break;
                case double rS when (rS > 4 && rS <= 5): index = 4; break;
                default: index = -1;break;
            }
            return index;
        }

        static void DisplayReport(string[] bookIds, double[] reviewScores, int count)
        {
            string[] ratingMessage = { "0-1", "1-2", "2-3", "3-4", "4-5" };
            double[] totals = new double[5];
            int[] scoreCounts = new int[5];

            double totalScores = 0;
            double highestScore = reviewScores[0];
            int highestIndex = 0;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"{bookIds[i]}   {reviewScores[i]}");

                    int index = GetRatingIndex(reviewScores[i]);
                    totals[index] += reviewScores[i];
                    scoreCounts[index]++;

                    totalScores += reviewScores[i];
                    if (reviewScores[i] > highestScore)
                    {
                        highestScore = reviewScores[i];
                        highestIndex = i;
                    }


                }

                for (int i = 0; i < totals.Length; i++)
                {
                    if (scoreCounts[i] != 0)
                    {
                        Console.WriteLine("{ratingMessage[i]}   {ratingCount[i]}  { totals[i]/ratingCounts[i]}");
                    }
                    else
                    {
                        Console.WriteLine("{ratingMessage[i]} had no entries");
                    }
                }

                Console.WriteLine($"Highest rating: {reviewScores[highestIndex]}");
                Console.WriteLine($"Average rating: {totalScores / count}");

            }
            else
            {
                Console.WriteLine(" No reviews posted.");
            }

        }
    }
    
}
