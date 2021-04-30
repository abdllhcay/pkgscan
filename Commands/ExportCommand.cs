using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

using Pkgscan.Options;
using Pkgscan.Services;

namespace Pkgscan.Commands
{
    public static class ExportCommand
    {
        public static async Task RunAsync(ExportOptions options, string projectPath)
        {
            var packageService = new PackageService();
            var packageInfoList = await packageService.GetPackageInfoList(projectPath);

            var outputDir = String.IsNullOrEmpty(options.OutputPath) ?
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) :
                options.OutputPath;

            var fileName = Path.GetFileName(projectPath) + "_packages";

            if (options.Json)
            {
                var jsonOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                using FileStream createStream = File.Create($"{outputDir}/{fileName}.json");
                await JsonSerializer.SerializeAsync(createStream, packageInfoList, jsonOptions);
            }
        }
    }
}