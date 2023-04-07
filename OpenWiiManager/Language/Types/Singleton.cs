using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Types
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T? _instance;

        private static T GetInstance()
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }

        public static T Instance => GetInstance();

        public static void EnsureInstance()
        {
            GetInstance();
        }
    }
}
