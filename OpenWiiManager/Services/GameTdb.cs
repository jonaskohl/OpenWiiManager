using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl;
using Flurl.Http;
using OpenWiiManager.Checking;
using OpenWiiManager.Core;
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Language.Types;
using OpenWiiManager.Tools;

namespace OpenWiiManager.Services
{
    public class GameTdb
    {
        const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/109.0";

        //const string URL_WII_RSS_FEED = "https://www.gametdb.com/Main/LatestGames?action=rss";
        //const string URL_WII_DATABASE = "https://www.gametdb.com/wiitdb.zip"; //?WIIWARE=1&GAMECUBE=1";

        private XDocument? wiiTdbDatabase;

        public enum DatabaseLanguage
        {
            [EnumValue("ORIG")]
            Original,

            [EnumValue("EN")]
            English,

            [EnumValue("JA")]
            Japanese,

            [EnumValue("FR")]
            French,

            [EnumValue("DE")]
            German,

            [EnumValue("ES")]
            Spanish,

            [EnumValue("IT")]
            Italian,

            [EnumValue("NL")]
            Dutch,

            [EnumValue("PT")]
            Portuguese,

            [EnumValue("RU")]
            Russian,

            [EnumValue("KO")]
            Korean,

            [EnumValue("ZHCN")]
            Chinese,

            [EnumValue("ZHTW")]
            Taiwanese,
        }

        public Task<XElement?> LookupWiiTitleInfoAsync(string id)
        {
            TryLoadDatabase();

            return Task.Run(() =>
            {
                return wiiTdbDatabase?
                    .Root?
                    .Elements("game")
                    .FirstOrDefault(g => g.Element("id")?.Value == id);
            });
        }

        private void TryLoadDatabase()
        {
            if (wiiTdbDatabase != null)
                return;

            if (!File.Exists(ApplicationEnviornment.GameDatabaseFilePath))
                return;

            wiiTdbDatabase = XDocument.Load(ApplicationEnviornment.GameDatabaseFilePath);
        }

        public async Task DownloadDatabase(IProgress<(byte, long, long)>? progress = null, DatabaseLanguage language = DatabaseLanguage.Original)
        {
            var resp = await ApplicationConfigurationSingleton.Instance.WiiDbZipUrl
                .WithHeaders(new { User_Agent = USER_AGENT })
                //.SetQueryParam("LANG", language.GetDefinedValue() ?? "")
                .SetQueryParam("__owmCacheBuster", Guid.NewGuid().ToString("N"))
                .GetStreamAsync();

            var totalLength = resp.Length;

            var tempFileName = ApplicationEnviornment.GetTempFileName();
            using (var fstream = File.OpenWrite(tempFileName))
            {
                if (progress == null)
                    await resp.CopyToAsync(fstream);
                else
                    await resp.CopyToAsync(fstream, new Progress<long>(l =>
                    {
                        progress.Report((0x00, l, totalLength));
                    }));
                await fstream.FlushAsync();
                fstream.Close();
            }
            resp.Close();

            Debug.WriteLine(tempFileName);
            // HACK
            Thread.Sleep(10);

            //IOUtil.EnsureDirectoryExists(ApplicationEnviornment.LocalUserDataDirectory);

            using (var archive = ZipFile.OpenRead(tempFileName))
            {
                var entry = archive.Entries.Where(e => e.FullName == "/wiitdb.xml" || e.FullName == "wiitdb.xml").FirstOrDefault();
                RuntimeAssertions.NotNull(entry, "Could not find database inside of downloaded ZIP archive! Maybe it is corrupt?");
                Debug.WriteLine($"Entry {entry.FullName} found! Will extract to {ApplicationEnviornment.GameDatabaseFilePath}");

                using (var zipStream = entry!.Open())
                {
                    using (var xmlStream = File.OpenWrite(ApplicationEnviornment.GameDatabaseFilePath))
                    {
                        if (progress == null)
                            await zipStream.CopyToAsync(xmlStream);
                        else
                        {
                            var zipTotalLength = entry.Length;
                            await zipStream.CopyToAsync(xmlStream, new Progress<long>(l =>
                            {
                                progress.Report((0x01, l, zipTotalLength));
                            }), bufferSize: 0xFFFF);
                        }
                    }
                }
            }

            File.Delete(tempFileName);
        }

        public void UnloadDatabase()
        {
            wiiTdbDatabase = null;
        }

        public async Task<bool> NeedsUpdate()
        {
            var lastUpdateTime = ApplicationStateSingleton.Instance.LastFeedUpdate;
            var feed = await GetWiiRssFeed();
            var needsUpdate = feed.LastUpdatedTime > lastUpdateTime;
            ApplicationStateSingleton.Instance.LastFeedUpdate = feed.LastUpdatedTime;
            return needsUpdate;
        }

        private async Task<SyndicationFeed> GetWiiRssFeed()
        {
            using var reader = XmlReader.Create(ApplicationConfigurationSingleton.Instance.WiiDbRssUrl);
            var feed = await Task.Run(() => SyndicationFeed.Load(reader));
            reader.Close();
            return feed;
        }
    }

    public class GameTdbSingleton : Singleton<GameTdb> { }
}
