using System;
using System.IO;
using System.Messaging;

namespace Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter 'C' key to close the app");
                Console.WriteLine("Enter 'S' key to save the text to the 'text.txt' file");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.C)
                {
                    return;
                }

                if (key.Key == ConsoleKey.S)
                {
                    File.AppendAllText(@"C:\__WORK\EmbraceFailures\01-simple.txt", "Some text saved to a file");
                    //File.AppendAllText(@"C:\__WORK\EmbraceFailures\1\01-simple.txt", "Some text saved to a file");
                    Console.WriteLine("Text saved to the file");
                }
            }
        }
    }
}
