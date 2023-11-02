using System;

namespace FrootLoops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] fruits = {"Apple", "Banana", "Kiwi", "Dragonfruit", "Banana", "Banana", "Peach", "Pear",
                               "Apple", "Peach", "Dragonfruit", "Dragonfruit", "Peach", "Guava", "Cherry",
                               "Apple", "Dragonfruit", "Guava", "Banana", "Pear"};

            foreach (string fruit in fruits)
            {
                Console.WriteLine(fruit);
            }
            Console.ReadKey();
        }
    }
}