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
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Tools;

namespace OpenWiiManager
{
    public static class GameTdb
    {
        const string URL_WII_RSS_FEED = "https://www.gametdb.com/Main/LatestGames?action=rss";
        const string URL_WII_DATABASE = "https://www.gametdb.com/wiitdb.zip?WIIWARE=1&GAMECUBE=1";

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

        public static async Task DownloadDatabase(DatabaseLanguage language = DatabaseLanguage.Original)
        {
            var resp = await URL_WII_DATABASE
                .SetQueryParam("LANG", language.GetLanguageValue() ?? "")
                .SetQueryParam("__owmCacheBuster", Guid.NewGuid().ToString("N"))
                .GetStreamAsync();

            var tempFileName = ApplicationEnviornment.GetTempFileName();
            using (var fstream = File.OpenWrite(tempFileName))
            {
                await resp.CopyToAsync(fstream);
                await fstream.FlushAsync();
            }
            resp.Close();

            IOUtil.EnsureDirectoryExists(ApplicationEnviornment.LocalUserDataDirectory);

            using (var archive = ZipFile.OpenRead(tempFileName))
            {
                var entry = archive.Entries.Where(e => e.FullName == "/wiitdb.xml" || e.FullName == "wiitdb.xml").FirstOrDefault();
                RuntimeAssertions.NotNull(entry, "Could not find database inside of downloaded ZIP archive! Maybe it is corrupt?");
                Debug.WriteLine($"Entry found! Will extract to {ApplicationEnviornment.GameDatabaseFilePath}");

                using (var zipStream = entry!.Open())
                using (var xmlStream = File.OpenWrite(ApplicationEnviornment.GameDatabaseFilePath))
                    await zipStream.CopyToAsync(xmlStream);
            }

            File.Delete(tempFileName);
        }

        public static async Task<bool> NeedsUpdate()
        {
            var lastUpdateTime = ApplicationState.LastFeedUpdate;
            var feed = await GetWiiRssFeed();
            var needsUpdate = feed.LastUpdatedTime > lastUpdateTime;
            ApplicationState.LastFeedUpdate = feed.LastUpdatedTime;
            return needsUpdate;
        }

        private static async Task<SyndicationFeed> GetWiiRssFeed()
        {
            using var reader = XmlReader.Create(URL_WII_RSS_FEED);
            var feed = await Task.Run(() => SyndicationFeed.Load(reader));
            reader.Close();
            return feed;
        }
    }
}
