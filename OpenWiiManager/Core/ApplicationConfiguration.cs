using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Core
{
    public class ApplicationConfiguration : SerializableStateHolder
    {
        protected override string FilePath => ApplicationEnviornment.ConfigFilePath;

        [StateSerialization]
        private string? isoPath;

        [SettingsCategory("General",
            label: "Game directory",
            description: "The directory where all of your Wii ISO files live"
        )]
        public string? IsoPath { get => isoPath; set { isoPath = value; Serialize(); } }
    }

    public class ApplicationConfigurationSingleton : Singleton<ApplicationConfiguration> { }
}
