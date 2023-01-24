using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public static object? GetDefinedValue<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            var attrib = value.GetValueAttribute<TEnum, EnumValueAttribute>();
            if (attrib == null)
                return null;
            return attrib.Value;
        }

        public static T? GetDefinedValue<TEnum, T>(this TEnum value) where TEnum : struct, Enum
        {
            var attrib = value.GetValueAttribute<TEnum, EnumValueAttribute>();
            if (attrib == null)
                return default;
            return (T?)attrib.Value;
        }

        //public static object? GetLanguageValue(this GameTdb.DatabaseLanguage value)
        //{
        //    var attrib = value.GetValueAttribute<GameTdb.DatabaseLanguage, EnumValueAttribute>();
        //    if (attrib == null)
        //        return null;
        //    return attrib.Value;
        //}

        //public static T? GetLanguageValue<T>(this GameTdb.DatabaseLanguage value)
        //{
        //    var attrib = value.GetValueAttribute<GameTdb.DatabaseLanguage, EnumValueAttribute>();
        //    if (attrib == null)
        //        return default;
        //    return (T?)attrib.Value;
        //}

        public static TEnum[] GetValues<TEnum>(this TEnum value) where TEnum: struct, Enum
        {
            var flags = new List<TEnum>();
            foreach (TEnum flag in Enum.GetValues(typeof(TEnum)))
                if (Convert.ToInt32(flag) != 0 && value.HasFlag(flag))
                    flags.Add(flag);
            return flags.ToArray();
        }
    }
}
