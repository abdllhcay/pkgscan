using CommandLine;

namespace Pkgscan.Options
{

    [Verb("show", HelpText = "Print packages. Type -h for more options.")]
    public class ShowOptions
    {
        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
    }
}