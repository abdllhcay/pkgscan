using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

using ConsoleTables;

using Pkgscan.Common;
using Pkgscan.Models;
using Pkgscan.Options;
using Pkgscan.Services;

namespace Pkgscan.Commands
{
    public static class ShowCommand
    {
        public static async Task RunAsync(ShowOptions options, string projectPath)
        {
            var projectFiles = Directory.GetFiles(projectPath, "*.csproj");

            if (!projectFiles.Any())
            {
                Process.Terminate("Could not found a project file in the specified directory.");
            }

            var packageList = new List<Package>();

            foreach (var file in projectFiles)
            {
                var doc = new XmlDocument();
                doc.Load(file);
                var nodes = doc.DocumentElement.SelectNodes("/Project/ItemGroup/PackageReference");

                foreach (XmlNode node in nodes)
                {
                    var packageName = node.Attributes["Include"]?.Value;
                    var packageVersion = node.Attributes["Version"]?.Value;

                    packageList.Add(new Package
                    {
                        CurrentVersion = packageVersion,
                        Name = packageName
                    });
                }
            }

            var packageManager = new PackageManager();

            var table = new ConsoleTable("PACKAGE", "AUTHOR", "VERSION", "LATEST", "SIZE", "PUBLISHED", "LAST UPDATE");

            foreach (var package in packageList)
            {
                var packageInfo = await packageManager.GetPackageInfoAsync(package.Name, package.CurrentVersion);

                table.AddRow(packageInfo.Name,
                    packageInfo.Author,
                    packageInfo.Version,
                    packageInfo.Version,
                    packageInfo.Size,
                    packageInfo.PublishDate,
                    packageInfo.LastUpdate);
            }

            table.Write(Format.Minimal);
        }
    }
}