using System.Collections.Generic;
using System.IO;

using CommandLine;

using Pkgscan.Commands;
using Pkgscan.Helpers;
using Pkgscan.Options;

namespace Pkgscan.Parser
{
    public class CommandLineParser
    {
        private string ProjectPath { get; set; }

        public void Initialize(string[] args)
        {
            ProjectPath = args[0].Trim();

            if (!Directory.Exists(this.ProjectPath))
            {
                Process.Terminate("The specified directory is not found.");
            }

            CommandLine.Parser.Default.ParseArguments<ShowOptions>(args)
            //  .WithParsed<MainOptions>(RunMainOptions)
             .WithParsed<ShowOptions>(RunShowOptions)
             .WithNotParsed(HandleParseError);
        }

        // private void RunMainOptions(MainOptions opts)
        // {
        //     var a = 10;
        // }

        private void RunShowOptions(ShowOptions options)
        {
            ShowCommand.Run(options, this.ProjectPath);
        }

        private void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}