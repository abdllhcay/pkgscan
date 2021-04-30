using CommandLine;

namespace Pkgscan.Options
{
    [Verb("export", HelpText = "Export package list. Type -h for more options.")]
    public class ExportOptions
    {
        [Option('p', "project", HelpText = "Project file or path.")]
        public string ProjectPath { get; set; }

        [Option('c', "csv", Default = false, HelpText = "Export package list in CSV format.")]
        public bool Csv { get; set; }

        [Option('j', "json", Default = false, HelpText = "Export package list in Json format.")]
        public bool Json { get; set; }
    }
}