using System;
using System.IO;
using System.Threading.Tasks;

using ConsoleTables;

using Pkgscan.Common;
using Pkgscan.Options;
using Pkgscan.Services;

namespace Pkgscan.Commands
{
    public static class ListCommand
    {
        public static async Task RunAsync(ListOptions options)
        {
            var projectPath = options.ProjectPath;

            if (!Directory.Exists(projectPath) && !File.Exists(projectPath))
            {
                Process.Terminate("The specified directory or file is not found.");
            }

            var packageService = new PackageService();
            var packageInfoList = await packageService.GetPackageInfoList(projectPath);

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