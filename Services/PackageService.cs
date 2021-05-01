using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

using Pkgscan.Common;
using Pkgscan.Models;

namespace Pkgscan.Services
{
    public class PackageService
    {
        public async Task<IEnumerable<PackageInfo>> GetPackageInfoList(string projectPath)
        {
            var projectFile = this.GetProjectFile(projectPath);

            var doc = new XmlDocument();
            doc.Load(projectFile);
            var nodes = doc.DocumentElement.SelectNodes("/Project/ItemGroup/PackageReference");

            var packageList = new List<Package>();

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

            var packageManager = new PackageManager();
            var packageInfoList = new List<PackageInfo>();

            foreach (var package in packageList)
            {
                var packageInfo = await packageManager.GetPackageInfoAsync(package.Name, package.CurrentVersion);
                packageInfoList.Add(packageInfo);
            }

            return packageInfoList;
        }

        private string GetProjectFile(string path)
        {
            var projectFile = String.Empty;
            var attr = File.GetAttributes(path);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var projectFiles = Directory.GetFiles(path, "*.csproj");

                if (!projectFiles.Any())
                {
                    Process.Terminate("Could not found a project file in the specified directory.");
                }

                projectFile = projectFiles.First();
            }
            else
            {
                var extension = Path.GetExtension(path);

                if (!extension.Equals(".csproj"))
                {
                    Process.Terminate("You must provide a proper project file.");
                }

                projectFile = path;
            }

            return projectFile;
        }
    }
}