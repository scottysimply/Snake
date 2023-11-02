using System;

namespace FlippingOut
{
    class FlippingOut
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Console.WriteLine("How many times should the coin be flipped?");
            int times = Convert.ToInt32(Console.ReadLine());
            int tails_count = 0;
            for (int i = 0; i < times; i++)
            {
                int value = rand.Next(0, 2);
                Console.WriteLine(value == 0 ? "Heads" : "Tails");
                tails_count += value;
            }
            Console.WriteLine($"{times - tails_count} heads, {tails_count} tails.");
        }
    }
}