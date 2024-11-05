using NS.SD.Card.Models;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NS.SD.Card.Features
{
    public class NSDownloader
    {
        private readonly HttpClient _httpClient;
        private GitHubClient _gitHubClient;
        private const string TOKEN = "";

        public NSDownloader()
        {
            _httpClient = new HttpClient();
            _gitHubClient = new GitHubClient(new ProductHeaderValue("NS"))
            {
                Credentials = new Credentials(TOKEN)
            };
        }

        public async Task DownloadFileAsync(string fileName, string stringUrl, NSType type)
        {
            using (var response = await _httpClient.GetAsync(stringUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = await _httpClient.GetByteArrayAsync(stringUrl);
                    var totalBytes = fileBytes.Length;
                    var bytesWritten = 0;
                    var bufferSize = 8192;
                    var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                    using (var fileStream = new FileStream(filePath, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int bytesRead = 0;
                        while (bytesRead < totalBytes)
                        {
                            int bytesToWrite = Math.Min(bufferSize, totalBytes - bytesRead);
                            await fileStream.WriteAsync(fileBytes, bytesRead, bytesToWrite);
                            bytesWritten += bytesToWrite;
                            bytesRead += bytesToWrite;

                            var progress = Math.Round((double)bytesWritten / totalBytes * 100, 2);
                        }
                    }
                }

            }
        }

        public async Task DownloadFileAsync(NSGitHubAsset asset, NSType type)
        {
            using (var response = await _httpClient.GetAsync(asset.DownloadUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = await _httpClient.GetByteArrayAsync(asset.DownloadUrl);
                    var totalBytes = fileBytes.Length;
                    var bytesWritten = 0;
                    var bufferSize = 8192;
                    var filePath = Path.Combine(Environment.CurrentDirectory, asset.AssetName);

                    using (var fileStream = new FileStream(filePath, System.IO.FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int bytesRead = 0;
                        while (bytesRead < totalBytes)
                        {
                            int bytesToWrite = Math.Min(bufferSize, totalBytes - bytesRead);
                            await fileStream.WriteAsync(fileBytes, bytesRead, bytesToWrite);
                            bytesWritten += bytesToWrite;
                            bytesRead += bytesToWrite;

                            var progress = Math.Round((double)bytesWritten / totalBytes * 100, 2);
                        }
                    }
                }
            }
        }

        public async Task<IEnumerable<NSGitHubRelease>> GetListReleasesAsync(string owner, string repo)
        {
            var listGitHubReleases = new List<NSGitHubRelease>();
            var releases = await _gitHubClient.Repository.Release.GetAll(owner, repo);
            if (releases.Count > 0)
            {
                listGitHubReleases = releases.Select(r => new NSGitHubRelease
                {
                    TagName = r.TagName,
                    ReleaseAssets = r.Assets.Where(a => Regex.IsMatch(a.Name, "\\.(nro|zip|bin|ovl)"))
                                            .Select(a => new NSGitHubAsset
                                            {
                                                AssetName = a.Name,
                                                DownloadUrl = a.BrowserDownloadUrl
                                            }).ToList()
                }).ToList();
            }
            return listGitHubReleases;
        }

        public async Task DownloadAssetsAsync(List<NSGitHubAsset> listAssets, NSType type)
        {
            if (listAssets.Count() > 0)
            {
                foreach (var item in listAssets)
                    await DownloadFileAsync(item, type);
            }
        }
    }
}
