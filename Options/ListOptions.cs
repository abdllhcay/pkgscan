using CommandLine;

namespace Pkgscan.Options
{
    [Verb("list", HelpText = "Print packages to standard output. Type -h for more options.")]
    public class ListOptions
    {
        [Option('p', "project", HelpText = "Project file or path.")]
        public string ProjectPath { get; set; }

        [Option('v', "verbose", Default = false, HelpText = "Display verbose output.")]
        public bool Verbose { get; set; }
    }
}