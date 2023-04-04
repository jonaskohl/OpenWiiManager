using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Core
{
    public static class HashCache
    {
        private static readonly Dictionary<string, string> Crc32Hashes = new();
        private static readonly Dictionary<string, string> MD5Hashes = new();
        private static readonly Dictionary<string, string> SHA1Hashes = new();

        public static string? GetCrc32(string gameId)
        {
            lock (Crc32Hashes)
            {
                if (!Crc32Hashes.ContainsKey(gameId)) return null;
                return Crc32Hashes[gameId];
            }
        }
        public static void CacheCrc32(string gameId, string hash)
        {
            lock (Crc32Hashes)
            {
                Crc32Hashes[gameId] = hash;
            }
        }

        public static string? GetMD5(string gameId)
        {
            lock (MD5Hashes)
            {
                if (!MD5Hashes.ContainsKey(gameId)) return null;
                return MD5Hashes[gameId];
            }
        }
        public static void CacheMD5(string gameId, string hash)
        {
            lock (MD5Hashes)
            {
                MD5Hashes[gameId] = hash;
            }
        }

        public static string? GetSHA1(string gameId)
        {
            lock (SHA1Hashes)
            {
                if (!SHA1Hashes.ContainsKey(gameId)) return null;
                return SHA1Hashes[gameId];
            }
        }
        public static void CacheSHA1(string gameId, string hash)
        {
            lock (SHA1Hashes)
            {
                SHA1Hashes[gameId] = hash;
            }
        }
    }
}
