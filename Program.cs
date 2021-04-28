using System;

using Pkgscan.Parser;

namespace Pkgscan
{
    class Program
    {

        static void Main(string[] args)
        {
            var commandLineParser = new CommandLineParser();
            commandLineParser.Initialize(args);
        }

        static void PrintHelpMessage()
        {
            string helpMessage = "\nUsage: pkgscan PATH COMMAND [OPTIONS]\n\nPath:\n  Path to project directory\nCommands:\n  show\tPrint packages to screen. Type -h for more options.";

            Console.WriteLine(helpMessage);
        }
    }
}
