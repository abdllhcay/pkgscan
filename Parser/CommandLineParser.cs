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
            await CommandLine.Parser.Default.ParseArguments<ShowOptions>(args)
                .MapResult(
                    (ShowOptions opts) => RunShowOptions(opts, args[0].Trim()),
                    errs => Task.FromResult(0)
                );
        }

        // private void RunMainOptions(MainOptions opts)
        // {
        //     var a = 10;
        // }

        private async Task RunShowOptions(ShowOptions options, string projectPath)
        {
            if (!Directory.Exists(projectPath))
            {
                Process.Terminate("The specified directory is not found.");
            }

            await ShowCommand.RunAsync(options, projectPath);
        }
    }
}