using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceParameters(this string str, IEnumerable<KeyValuePair<string, string>> values)
        {
            foreach (var e in values)
                str = str.Replace($"{{{e.Key}}}", e.Value);
            return str;
        }
        public static string ReplaceParameters(this string str, object values)
        {
            var props = values.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Instance);
            foreach (var p in props)
                str = str.Replace($"{{{p.Name}}}", p.GetValue(values)?.ToString());
            return str;
        }
    }
}
