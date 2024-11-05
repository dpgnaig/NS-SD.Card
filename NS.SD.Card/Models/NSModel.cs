using System.Collections.Generic;

namespace NS.SD.Card.Models
{
    public class GitHubRepo
    {
        public string RepoOwner { get; set; }
        public string RepoName { get; set; }
    }

    public class NSGitHubAsset
    {
        public string AssetName { get; set; }
        public string DownloadUrl { get; set; }
    }

    public class NSGitHubRelease
    {
        public string TagName { get; set; }
        public List<NSGitHubAsset> ReleaseAssets { get; set; } = new List<NSGitHubAsset>();
    }
}
