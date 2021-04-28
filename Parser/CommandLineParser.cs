using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CommandLine;

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
                Console.WriteLine("The specified directory is not found.");
                Environment.Exit(0);
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

        private void RunShowOptions(ShowOptions opts)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"list {ProjectPath} package",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();

            // while (!proc.StandardOutput.EndOfStream)
            // {
            //     string line = proc.StandardOutput.ReadLine();
            //     // do something with line
            // }

            // string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
        }

        private void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}