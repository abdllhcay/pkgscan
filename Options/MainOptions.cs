using CommandLine;

namespace Pkgscan.Options
{
    [Verb("path", isDefault: true, HelpText = "Path to project directory.")]
    public class MainOptions
    {
        [Option(Required = true)]
        public string ProjectPath { get; set; }
    }
}