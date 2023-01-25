using OpenWiiManager.Checking;
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Language.Types;
using OpenWiiManager.Tools;
using System.Reflection;
using System.Xml.Linq;

namespace OpenWiiManager.Core
{
    public class ApplicationState : SerializableStateHolder
    {
        public ApplicationState() : base() { }

        protected override string FilePath => ApplicationEnviornment.StateFilePath;

        [StateSerialization]
        private DateTimeOffset lastFeedUpdate;

        [StateSerialization]
        private Point mainWindowPos;

        [StateSerialization]
        private Size mainWindowSize;

        [StateSerialization]
        private int mainWindowSplitDistance;

        [StateSerialization]
        private bool isFirstRun = true;

        public DateTimeOffset LastFeedUpdate { get => lastFeedUpdate; set { lastFeedUpdate = value; Serialize(); } }
        public Point MainWindowPos { get => mainWindowPos; set { mainWindowPos = value; Serialize(); } }
        public Size MainWindowSize { get => mainWindowSize; set { mainWindowSize = value; Serialize(); } }
        public int MainWindowSplitDistance { get => mainWindowSplitDistance; set { mainWindowSplitDistance = value; Serialize(); } }
        public bool IsFirstRun { get => isFirstRun; set { isFirstRun = value; Serialize(); } }
    }

    public class ApplicationStateSingleton : Singleton<ApplicationState> { }
}
