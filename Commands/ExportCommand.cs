using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using CsvHelper;

using Pkgscan.Options;
using Pkgscan.Services;

namespace Pkgscan.Commands
{
    public static class ExportCommand
    {
        public static async Task RunAsync(ExportOptions options)
        {
            var packageService = new PackageService();
            var packageInfoList = await packageService.GetPackageInfoList(options.ProjectPath);

            var outputDir = String.IsNullOrEmpty(options.OutputPath) ?
                Directory.GetCurrentDirectory() :
                options.OutputPath;

            var fileName = Path.GetFileName(options.ProjectPath) + "_packages";

            if (options.Json)
            {
                var jsonOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                using FileStream createStream = File.Create($"{outputDir}/{fileName}.json");
                await JsonSerializer.SerializeAsync(createStream, packageInfoList, jsonOptions);
            }
            else if (options.Csv)
            {
                using (var writer = new StreamWriter($"{outputDir}/{fileName}.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(packageInfoList);
                }
            }
        }
    }
}