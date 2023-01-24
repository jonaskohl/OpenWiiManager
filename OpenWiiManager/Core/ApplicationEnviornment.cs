using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Core
{
    public static class ApplicationEnviornment
    {
        public static string LocalUserDataDirectory => Environment.ExpandEnvironmentVariables(@"%localappdata%\Jonas Kohl\OWM\");
        public static string RoamingUserDataDirectory => Environment.ExpandEnvironmentVariables(@"%appdata%\Jonas Kohl\OWM\");
        public static string TempDataDirectory => Environment.ExpandEnvironmentVariables(@"%temp%\Jonas Kohl\OWM\");

        public static string GameDatabaseFilePath => Path.Join(LocalUserDataDirectory, "wiidb.xml");
        public static string StateFilePath => Path.Join(LocalUserDataDirectory, "state.xml");
        public static string ConfigFilePath => Path.Join(RoamingUserDataDirectory, "owm.xml");

        public static string GetTempFileName()
        {
            if (!Directory.Exists(TempDataDirectory))
                Directory.CreateDirectory(TempDataDirectory);

            var guid = Guid.NewGuid().ToString("N");

            return Path.Join(TempDataDirectory, guid);
        }
    }
}
