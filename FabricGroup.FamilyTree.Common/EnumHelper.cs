using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FabricGroup.FamilyTree.Common
{
    public static class EnumHelper
    {
        private static void ValidateEnumType<T>()
            where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }
        }

        public static string GetDescription<T>(this T value)
            where T : struct
        {
            ValidateEnumType<T>();

            string desc = value.ToString();

            var info = value.GetType().GetField(desc);
            var attrs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                desc = attrs[0].Description;
            }

            return desc;
        }

        public static T Parse<T>(string value, bool ignoreCase)
            where T : struct
        {
            ValidateEnumType<T>();

            if (!IsValid<T>(value))
            {
                throw new ArgumentException("Invalid value.");
            }

            var result = (T)Enum.Parse(typeof(T), value, ignoreCase);
            return result;
        }

        public static T Parse<T>(string value)
            where T : struct
        {
            ValidateEnumType<T>();

            return Parse<T>(value, false);
        }

        public static bool IsValid<T>(string value)
            where T : struct
        {
            ValidateEnumType<T>();

            return Enum.IsDefined(typeof(T), value);
        }

        public static T ToEnum<T>(this string value)
            where T : struct
        {
            ValidateEnumType<T>();

            return Parse<T>(value);
        }

        public static Dictionary<int, string> ToDictionary<T>()
            where T : struct
        {
            ValidateEnumType<T>();

            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => (int)(object)t, t => t.ToString());
        }

        public static Dictionary<string, string> NameDescriptionToDictionary<T>()
            where T : struct
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => t.ToString(), t => GetDescription(t));
        }
    }
}
