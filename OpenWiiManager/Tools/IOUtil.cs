using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
