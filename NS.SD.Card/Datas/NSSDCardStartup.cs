using NS.SD.Card.Models;
using System.Collections.Generic;

namespace NS.SD.Card.Datas
{
    public sealed class NSSDCardStartup
    {
        public Dictionary<string, GitHubRepo> NSCoreGitHubRepos { get; set; }
        public Dictionary<string, GitHubRepo> NSHomebrewGitHubRepos { get; set; }
        public Dictionary<string, GitHubRepo> NSSysModGitHubRepos { get; set; }
        public List<NSGitHubAsset> GitHubAssets { get; set; }

        private NSSDCardStartup()
        {
            GitHubAssets = new List<NSGitHubAsset>();
            SeedCoreGitHubRepoData();
            SeedHomebrewGitHubRepoData();
            SeedSysModGitHubRepoData();
        }

        private static NSSDCardStartup _instance;

        public static NSSDCardStartup GetInstance()
        {
            if (_instance == null)
                _instance = new NSSDCardStartup();
            return _instance;
        }

        private void SeedCoreGitHubRepoData()
        {
            NSCoreGitHubRepos = new Dictionary<string, GitHubRepo>()
            {
                {
                    "Atmosphere (Horizon OS)",
                    new GitHubRepo
                    {
                        RepoOwner = "atmosphere-nx",
                        RepoName = "atmosphere",
                    }
                },
                {
                    "Hekate (bootloader)",
                    new GitHubRepo
                    {
                        RepoOwner = "ctcaer",
                        RepoName = "hekate"
                    }
                }
            };
        }

        private void SeedHomebrewGitHubRepoData()
        {
            NSHomebrewGitHubRepos = new Dictionary<string, GitHubRepo>()
            {
                {
                    "DBI (install NSP, NSZ, XCI and XCZ)",
                    new GitHubRepo
                    {
                        RepoOwner = "rashevskyv",
                        RepoName = "dbi"
                    }
                },
                {
                    "Goldleaf (a multi-purpose homebrew tool)",
                    new GitHubRepo
                    {
                        RepoOwner = "XorTroll",
                        RepoName = "Goldleaf"
                    }
                },
                {
                    "HB App Store (Homebrew App Store)",
                    new GitHubRepo
                    {
                        RepoOwner = "fortheusers",
                        RepoName = "hb-appstore"
                    }
                },
                {
                    "Breeze (game cheating tool)",
                    new GitHubRepo
                    {
                        RepoOwner = "tomvita",
                        RepoName = "Breeze-Beta"
                    }
                },
                {
                    "JKSV (JK's Save Manager Switch Edition)",
                    new GitHubRepo
                    {
                        RepoOwner = "J-D-K",
                        RepoName = "JKSV"
                    }
                },
                {
                    "FTPD (FTP Server for Switch)",
                    new GitHubRepo
                    {
                        RepoOwner = "mtheall",
                        RepoName = "ftpd"
                    }
                },
                {
                    "NXThemesInstaller (the Switch theme injector)",
                    new GitHubRepo
                    {
                        RepoOwner = "exelix11",
                        RepoName = "SwitchThemeInjector"
                    }
                },
                {
                    "NX-Shell (a multi-purpose file manager)",
                    new GitHubRepo
                    {
                        RepoOwner = "joel16",
                        RepoName = "NX-Shell"
                    }
                }
            };
        }

        private void SeedSysModGitHubRepoData()
        {
            NSSysModGitHubRepos = new Dictionary<string, GitHubRepo>()
            {
                {
                    "* nx-ovlloader (Load Switch overlay OVLs (NROs))",
                    new GitHubRepo
                    {
                        RepoOwner = "WerWolv",
                        RepoName = "nx-ovlloader"
                    }
                },
                {
                    "Tesla-Menu",
                    new GitHubRepo
                    {
                        RepoOwner = "WerWolv",
                        RepoName = "Tesla-Menu"
                    }
                },
                {
                    "Ultrahand",
                    new GitHubRepo
                    {
                        RepoOwner = "ppkantorski",
                        RepoName = "Ultrahand-Overlay"
                    }
                },
                {
                    "EdiZon",
                    new GitHubRepo
                    {
                        RepoOwner = "WerWolv",
                        RepoName = "EdiZon"
                    }
                },
                {
                    "EdiZon-Overlay",
                    new GitHubRepo
                    {
                        RepoOwner = "proferabg",
                        RepoName = "EdiZon-Overlay"
                    }
                },
                {
                    "Status Monitor Overlay",
                    new GitHubRepo
                    {
                        RepoOwner = "masagrator",
                        RepoName = "Status-Monitor-Overlay"
                    }
                },
                {
                    "sys-clk",
                    new GitHubRepo
                    {
                        RepoOwner = "retronx-team",
                        RepoName = "sys-clk"
                    }
                },
                {
                    "SysDVR",
                    new GitHubRepo
                    {
                        RepoOwner = "exelix11",
                        RepoName = "SysDVR"
                    }
                },
                {
                    "MissionControl",
                    new GitHubRepo
                    {
                        RepoOwner = "ndeadly",
                        RepoName = "MissionControl"
                    }
                },
                {
                    "sys-con",
                    new GitHubRepo
                    {
                        RepoOwner = "o0Zz",
                        RepoName = "sys-con"
                    }
                }
            };
        }
    }
}
