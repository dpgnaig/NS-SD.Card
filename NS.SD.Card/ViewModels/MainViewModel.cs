using NS.SD.Card.Datas;
using NS.SD.Card.Extensions;
using NS.SD.Card.Features;
using NS.SD.Card.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NS.SD.Card.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private NSSDCardStartup _data;
        private NSDownloader _downloader;

        public string MainViewName { get; set; }

        public Visibility SpinnerVisibility { get; set; }
        public NSType SelectedItem { get; set; }
        public ObservableCollection<NSType> NSTypeList { get; set; }

        public ObservableCollection<KeyValuePair<string, GitHubRepo>> RepoGitHubCores { get; set; }

        public ObservableCollection<KeyValuePair<string, List<string>>> GitHubReleases { get; set; }


        public ICommand SelectionChangedCommand { get; set; }

        private void Startup()
        {
            _data = NSSDCardStartup.GetInstance();
            _downloader = new NSDownloader();
            RepoGitHubCores = _data.NSCoreGitHubRepos.ToObservableCollection();
            NSTypeList = new ObservableCollection<NSType>(Enum.GetValues(typeof(NSType)).Cast<NSType>());

            SpinnerVisibility = Visibility.Hidden;
            SelectedItem = NSTypeList.First();
            OnSelectionChanged(null);
            
        }

        private void DefineCommand()
        {
            SelectionChangedCommand = new RelayCommand<object>(OnSelectionChanged);
        }

        public MainViewModel()
        {
            Startup();
            DefineCommand();
        }


        private async void OnSelectionChanged(object parameter)
        {
            if (GitHubReleases != null && GitHubReleases.Count > 0)
                GitHubReleases.Clear();

            Dictionary<string, List<string>> dicNSGitHubRelease = new Dictionary<string, List<string>>();
            SpinnerVisibility = Visibility.Visible;
            switch (SelectedItem)
            {
                case NSType.CORE:
                    foreach (var item in _data.NSCoreGitHubRepos)
                    {
                        var list = await _downloader.GetListReleasesAsync(item.Value.RepoOwner, item.Value.RepoName);
                        dicNSGitHubRelease.Add(item.Key, list.SelectMany(x => x.ReleaseAssets.Select(r => r.AssetName)).ToList());
                    }
                    break;
                case NSType.HOMEBREW:
                    foreach (var item in _data.NSHomebrewGitHubRepos)
                    {
                        var list = await _downloader.GetListReleasesAsync(item.Value.RepoOwner, item.Value.RepoName);
                        dicNSGitHubRelease.Add(item.Key, list.SelectMany(x => x.ReleaseAssets.Select(r => r.AssetName)).ToList());
                    }
                    break;
                case NSType.SYSMOD:
                    foreach (var item in _data.NSSysModGitHubRepos)
                    {
                        var list = await _downloader.GetListReleasesAsync(item.Value.RepoOwner, item.Value.RepoName);
                        dicNSGitHubRelease.Add(item.Key, list.SelectMany(x => x.ReleaseAssets.Select(r => r.AssetName)).ToList());
                    }
                    break;
                default:
                    break;
            }
            GitHubReleases = dicNSGitHubRelease.ToObservableCollection();
            SpinnerVisibility = Visibility.Hidden;
        }
    }
}
