using System;
using System.Threading.Tasks;

using CommandLine;
using CommandLine.Text;

using Pkgscan.Commands;
using Pkgscan.Options;

namespace Pkgscan.Parser
{
    public class CommandLineParser
    {
        public async Task Initialize(string[] args)
        {
            var parser = new CommandLine.Parser(x => x.HelpWriter = null)
                .ParseArguments<ShowOptions, ExportOptions>(args);

            await parser.MapResult(
                    (ShowOptions opts) => RunShowOptions(opts),
                    (ExportOptions opts) => RunExportOptions(opts),
                    errs => DisplayHelp(parser)
                );
        }

        private async Task RunShowOptions(ShowOptions options)
        {
            await ShowCommand.RunAsync(options);
        }

        private async Task RunExportOptions(ExportOptions options)
        {
            await ExportCommand.RunAsync(options);
        }

        private static Task<int> DisplayHelp<TOptions>(ParserResult<TOptions> parserResult)
        {
            var helpText = HelpText.AutoBuild(parserResult, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "pkgscan 1.0.1";
                h.Copyright = "";
                h.AddPreOptionsLine("\nUsage: pkgscan [COMMAND] [OPTION]");

                return h;
            }, e => e, verbsIndex: true);

            Console.WriteLine(helpText);

            return Task.FromResult(0);
        }
    }
}