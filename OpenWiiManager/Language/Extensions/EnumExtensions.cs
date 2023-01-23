using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenWiiManager.Language.Attributes;

namespace OpenWiiManager.Language.Extensions
{
    public static class EnumExtensions
    {
        public static T? GetValueAttribute<TEnum, T>(this TEnum enumeration) where T : Attribute where TEnum : struct
        {
            T? attribute =
              enumeration
                .GetType()
                ?.GetMember(enumeration.ToString() ?? "")
                ?.Where(member => member.MemberType == MemberTypes.Field)
                ?.FirstOrDefault()
                ?.GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .SingleOrDefault();

            return attribute;
        }

        public static string? GetLanguageValue(this GameTdb.DatabaseLanguage value)
        {
            var attrib = value.GetValueAttribute<GameTdb.DatabaseLanguage, EnumValueAttribute>();
            if (attrib == null)
                return null;
            return attrib.Value;
        }
    }
}
