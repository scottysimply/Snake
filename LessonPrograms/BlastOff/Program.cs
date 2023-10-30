namespace BlastOff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine($"{i}...");
            }
            Console.WriteLine("Blastoff!");
            Console.ReadKey();
        }
    }
}