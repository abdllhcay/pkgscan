using System;
using System.Threading.Tasks;

using ConsoleTables;

using Pkgscan.Options;
using Pkgscan.Services;

namespace Pkgscan.Commands
{
    public static class ShowCommand
    {
        public static async Task RunAsync(ShowOptions options)
        {
            var packageService = new PackageService();
            var packageInfoList = await packageService.GetPackageInfoList(options.ProjectPath);

            var table = new ConsoleTable("PACKAGE", "AUTHOR", "VERSION", "LATEST", "SIZE", "PUBLISHED", "LAST UPDATE");

            foreach (var info in packageInfoList)
            {
                table.AddRow(info.Name,
                    info.Author,
                    info.Version,
                    info.LatestVersion,
                    info.Size,
                    info.PublishDate,
                    info.LastUpdate);
            }

            Console.WriteLine();
            table.Write(Format.Minimal);
        }
    }
}