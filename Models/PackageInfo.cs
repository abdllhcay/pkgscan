using System;

namespace Pkgscan.Models
{
    public class PackageInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
        public string LastUpdate { get; set; }
        public long Size { get; set; }
    }
}