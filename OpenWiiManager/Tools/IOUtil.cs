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
    }
}
