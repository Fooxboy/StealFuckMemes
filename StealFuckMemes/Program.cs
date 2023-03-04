namespace StealFuckMemes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var memes = new StealMemesService();

            new Thread(async()=> await memes.StartSteal()).Start();

            Console.ReadLine();
        }
    }
}