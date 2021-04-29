using System.IO;
using System.Threading.Tasks;

using CommandLine;

using Pkgscan.Commands;
using Pkgscan.Common;
using Pkgscan.Options;

namespace Pkgscan.Parser
{
    public class CommandLineParser
    {
        private string ProjectPath { get; set; }

        public async Task Initialize(string[] args)
        {
            ProjectPath = args[0].Trim();

            if (!Directory.Exists(this.ProjectPath))
            {
                Process.Terminate("The specified directory is not found.");
            }

            await CommandLine.Parser.Default.ParseArguments<ShowOptions>(args)
                .MapResult(
                    (ShowOptions opts) => RunShowOptions(opts),
                    errs => Task.FromResult(0)
                );
        }

        // private void RunMainOptions(MainOptions opts)
        // {
        //     var a = 10;
        // }

        private async Task RunShowOptions(ShowOptions options)
        {
            await ShowCommand.RunAsync(options, this.ProjectPath);
        }
    }
}