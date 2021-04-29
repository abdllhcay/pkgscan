using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

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

            var padLength = packageList.Max(x => x.Name.Length) + 1;

            // if (options.Verbose)
            // {
            //     Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", "#", "PACKAGE", "DESCRIPTION", "VERSION", "LATEST", "SIZE", "LAST UPDATE");
            // }
            // else
            // {
            //     Console.WriteLine("{0} {1} {2}", "#", "PACKAGE", "VERSION".PadLeft(padLength, ' '));

            //     foreach (var package in packageList)
            //     {
            //         Console.WriteLine("{0} {1} {2}", packageList.IndexOf(package) + 1, package.Name, package.CurrentVersion.PadLeft(padLength - package.Name.Length + 5, ' '));
            //     }
            // }

            var packageManager = new PackageManager();

            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", "PACKAGE", "AUTHOR", "VERSION", "LATEST", "SIZE", "PUBLISHED", "LAST UPDATE");

            foreach (var package in packageList)
            {
                var packageInfo = await packageManager.GetPackageInfoAsync(package.Name, package.CurrentVersion);

                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}",
                    packageInfo.Name,
                    packageInfo.Author,
                    packageInfo.Version,
                    packageInfo.Version,
                    packageInfo.Size,
                    packageInfo.PublishDate,
                    packageInfo.LastUpdate);
            }

        }
    }
}