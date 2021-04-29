using System;

using Pkgscan.Parser;

namespace Pkgscan
{
    class Program
    {

        static void Main(string[] args)
        {
            // var val1 = "asdasdad1";
            // var val2 = "asdasdadasdasdad2";
            // var val3 = "hjhkfd";
            // var val4 = "fdfddododod";
            // Console.WriteLine("{0} {1} {2} {3}", "TITLE", "TITLE2".PadLeft(val1.Length + 1, ' '),
            //     "TITLE3".PadLeft(val2.Length, ' '), "TITLE4".PadLeft(val3.Length, ' '));
            // Console.WriteLine("{0} {1} {2} {3}", val1, val2, val3, val4);

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
