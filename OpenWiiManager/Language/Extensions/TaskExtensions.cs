using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Extensions
{
    public static class TaskExtensions
    {
        public static void ThrowIfFaulted(this Task t)
        {
            if (t.IsFaulted && t.Exception != null)
            {
                if (t.Exception is AggregateException && t.Exception.InnerException != null)
                    throw t.Exception.InnerException;
                else
                    throw t.Exception;
            }
        }
    }
}
