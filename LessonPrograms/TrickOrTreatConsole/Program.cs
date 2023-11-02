using System;

namespace TrickOrTreatConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Happy Halloween! Say the magic words to get free candy!");
            string input = Console.ReadLine();
            Random rand = new();
            if (input.ToLower().Replace(" ", "") == "trickortreat")
            {
                int value = rand.Next(0, 2);
                if (value == 1)
                {
                    Console.WriteLine("You got some candy!");
                }
                else
                {
                    Console.WriteLine("You got a trick. Look under *where*?");
                }
            }
        }
    }
}
