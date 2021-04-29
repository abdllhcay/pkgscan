namespace Pkgscan.Models
{
    public class Package
    {
        public string Name { get; set; }
        public string CurrentVersion { get; set; }
        public string Description { get; set; }
        public string LatestVersion { get; set; }
        public string Size { get; set; }
        public string LastUpdate { get; set; }
    }
}