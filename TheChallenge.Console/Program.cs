using System;

namespace YbHackaton.Infront
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter image file path: ");
            string imageFilePath = Console.ReadLine();

            ImagePrediction.MakePredictionRequestImage(imageFilePath).Wait();

            Console.WriteLine("\n\n\nHit ENTER to continue with text sentiment...");
            Console.ReadLine();
        }
    }
}
