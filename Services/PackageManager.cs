using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using NuGet.Common;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

using Pkgscan.Common;
using Pkgscan.Models;

namespace Pkgscan.Services
{
    public class PackageManager
    {
        private readonly SourceCacheContext _cache;
        private readonly ILogger _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly SourceRepository _repository;

        public PackageManager()
        {
            _cache = new SourceCacheContext();
            _logger = NullLogger.Instance;
            _cancellationToken = CancellationToken.None;
            _repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        }

        public async Task<IEnumerable<NuGetVersion>> GetPackageVersionsAsync(string packageName)
        {
            var resource = await _repository.GetResourceAsync<FindPackageByIdResource>();
            return await resource.GetAllVersionsAsync(packageName, _cache, _logger, _cancellationToken);


        }

        public async Task<PackageInfo> GetPackageInfoAsync(string packageName, string version)
        {
            var versions = await this.GetPackageVersionsAsync(packageName);

            var registrationResource = await _repository.GetResourceAsync<RegistrationResourceV3>();
            var packageIdentity = new PackageIdentity(packageName, NuGetVersion.Parse(version));

            var packageMetadata = await registrationResource.GetPackageMetadata(packageIdentity, _cache,
                _logger, _cancellationToken);

            var catalogItemUrl = packageMetadata.Value<string>("@id");
            var httpSourceResource = await _repository.GetResourceAsync<HttpSourceResource>();

            var catalogItem = await httpSourceResource.HttpSource.GetJObjectAsync(new HttpSourceRequest(catalogItemUrl, _logger),
                _logger, _cancellationToken);

            var author = catalogItem.Value<string>("authors");
            var name = catalogItem.Value<string>("id");
            var publishDate = catalogItem.Value<DateTime>("published");
            var lastUpdate = catalogItem.Value<DateTime>("lastEdited");
            var size = catalogItem.Value<long>("packageSize");
            var description = catalogItem.Value<string>("description");
            var lastVersion = versions.Last();

            return new PackageInfo
            {
                Author = author,
                Name = name,
                LastUpdate = lastUpdate.ToRelativeTime(),
                PublishDate = publishDate.ToString("MM/dd/yyyy"),
                Size = size.ToReadableSize(),
                Description = description,
                Version = version,
                LatestVersion = lastVersion.ToString()
            };
        }
    }
}