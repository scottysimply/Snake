using System;

namespace DoTheMath
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assignments
            byte a = 25;
            short b = 32000;
            int c = 1000000;
            byte x = 5;
            byte y = 10;
            byte z = 2;
            float myFloat = 1.5f;

            // I don't know if I actually saved any time here... it was just easier to write this instead of including a semicolon and newline.
            Print(x + y, myFloat + myFloat, myFloat + a, c - b, x * a, y * b, c / b, a / x, a % x, c % y);

        }
        // DRY
        static void Print(params object[] input)
        {
            foreach (object obj in input)
            {
                Console.WriteLine(obj);
            }
        }
    }
}