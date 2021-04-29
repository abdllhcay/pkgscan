using System;
using System.Threading.Tasks;
using Pkgscan.Parser;

namespace Pkgscan
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var commandLineParser = new CommandLineParser();
            await commandLineParser.Initialize(args);
        }
    }
}
