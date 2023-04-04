using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Language.Types;

namespace OpenWiiManager.Tools
{
    public static class IOUtil
    {
        public static void EnsureDirectoryExists(string d)
        {
            if (!Directory.Exists(d))
                Directory.CreateDirectory(d);
        }

        public static string GetLongestCommonPrefix(string[] s)
        {
            int k = s[0].Length;
            for (int i = 1; i < s.Length; i++)
            {
                k = Math.Min(k, s[i].Length);
                for (int j = 0; j < k; j++)
                    if (s[i][j] != s[0][j])
                    {
                        k = j;
                        break;
                    }
            }
            return s[0].Substring(0, k);
        }

        public static async Task<string> GetFileSHA1Async(string filename, IProgress<long> progress)
        {
            using var stream = new BufferedStream(File.OpenRead(filename), 1048576);
            var sha = SHA1.Create();
            var checksum = await sha.ComputeHashAsync(stream, progress: progress);
            return BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower();
        }

        public static async Task<string> GetFileMD5Async(string filename, IProgress<long> progress)
        {
            using var stream = new BufferedStream(File.OpenRead(filename), 1048576);
            var sha = MD5.Create();
            var checksum = await sha.ComputeHashAsync(stream, progress: progress);
            return BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower();
        }

        public static async Task<string> GetFileCRC32Async(string filename, IProgress<long> progress)
        {
            using var stream = new BufferedStream(File.OpenRead(filename), 1048576);
            var checksum = await Crc32Hash.CalculateAsync(stream, progress);
            return checksum.ToString("x8");
        }
    }
}
