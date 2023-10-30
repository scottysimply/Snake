namespace BottlesOfMilk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numBottles = 99;
            for (int bottle = numBottles; bottle > 0; bottle--)
            {
                // Interpolated strings my beloved.
                Console.WriteLine($"{bottle} bottle{(bottle == 1 ? "" : "s")} of milk on the wall, {bottle} bottle{(bottle == 1 ? "" : "s")} of milk.");
                Console.WriteLine($"Take one down, pass it around. {(bottle - 1 > 0 ? bottle - 1 : "No more")} bottle{(bottle - 1 == 1 ? "" : "s")} of milk on the wall.");
            }
            Console.ReadKey();
        }
    }
}