using System;
using System.Collections.Generic;

namespace FSL.CsharpUsefullExtensionsMethods
{
    public static class FSLEnumExtension
    {
        public static IEnumerable<Enum> ToEnumerable(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value) && Convert.ToInt64(value) != 0)
                    yield return value;
        }

        public static int ToInt32(this Enum input)
        {
            var values = Enum.GetValues(input.GetType());
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (value.Equals(input))
                {
                    return Convert.ToInt32(value);
                }
            }

            return 0;
        }

        public static T ToEnum<T>(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return (T)Enum.Parse(typeof(T), input);
            }

            return default(T);
        }
    }
}
