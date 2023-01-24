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
    }

    public class ApplicationConfigurationSingleton : Singleton<ApplicationConfiguration> { }
}
