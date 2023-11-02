using System;

namespace OddOrEven
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initializations
            string input;
            bool condition = true;
            int value;

            Console.WriteLine("Enter a number: ");

            // Input validation
            do
            {
                input = Console.ReadLine();
                // If number could be parsed, exit loop.
                if (Int32.TryParse(input, out value))
                {
                    condition = false;
                }
                else
                {
                    Console.WriteLine("No number detected. Try again.");
                }

            } while (condition);

            // Logic
            if (value % 2 == 0)
            {
                Console.WriteLine($"{value} is even.");
            }
            else
            {
                Console.WriteLine($"{value} is odd.");
            }
        }
    }
}