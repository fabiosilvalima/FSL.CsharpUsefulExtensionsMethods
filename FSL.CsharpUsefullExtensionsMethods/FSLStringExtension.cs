using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace FSL.CsharpUsefulExtensionsMethods
{
    public static class FSLStringExtension
    {
        public static string Remove(this string input, string strToRemove)
        {
            if (input.IsNullOrEmpty())
            {
                return null;
            }

            return input.Replace(strToRemove, "");
        }

        public static string Left(this string input, int minusRight = 1)
        {
            if (input.IsNullOrEmpty() || input.Length <= minusRight)
            {
                return null;
            }

            return input.Substring(0, input.Length - minusRight);
        }

        public static string GetHashAlgorithm(this string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
        {
            return culture.IsNullOrEmpty() ? new CultureInfo(culture) : defaultCulture;
        }
        
        public static string ToCamelCasing(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
            }

            return value;
        }
                
        public static double? ToDouble(this string value, string culture = "en-US")
        {
            try
            {
                return double.Parse(value, new CultureInfo(culture));
            }
            catch
            {
                return null;
            }
        }
        
        public static bool? ToBoolean(this string value)
        {
            bool valor = false;
            if (bool.TryParse(value, out valor))
            {
                return valor;
            }

            return null;
        }
        
        public static int? ToInt32(this string value)
        {
            int valor = 0;
            if (int.TryParse(value, out valor))
            {
                return valor;
            }

            return null;
        }
        
        public static long? ToInt64(this string value)
        {
            long valor = 0;
            if (long.TryParse(value, out valor))
            {
                return valor;
            }

            return null;
        }

        public static Guid? ToGuid(this string value)
        {
            Guid valor = Guid.Empty;
            if (Guid.TryParse(value, out valor))
            {
                return valor;
            }

            return null;
        }

        public static Guid ToGuid(this string value, Guid defaultValue)
        {
            Guid valor = Guid.Empty;
            if (Guid.TryParse(value, out valor))
            {
                return valor;
            }

            return defaultValue;
        }

        public static string AddQueyString(this string url, string queryStringKey, string queryStringValue)
        {
            var queryString = "";
            var segments = url.Split('?');
            if (segments.Length > 1)
            {
                queryString = "&";
            }
            else
            {
                queryString = "?";
            }

            return url + queryString + queryStringKey + "=" + queryStringValue;
        }
        
        public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
        {
            return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
        }

        public static string FillLeftWithZeros(this string value, int decimalDigits)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var sb = new StringBuilder();
                sb.Append(value);
                var s = value.Split(',');
                for (var i = s[s.Length - 1].Length; i < decimalDigits; i++)
                {
                    sb.Append("0");
                }

                value = sb.ToString();
            }

            return value;
        }

        public static string FormatWithDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits)
        {
            if (value.IsNullOrEmpty()) return value;

            if (!value.IndexOf(",").Equals(-1))
            {
                var s = value.Split(',');
                if (s.Length.Equals(2) && s[1].Length > 0)
                {
                    value = s[0] + "," + s[1].Substring(0, s[1].Length >= decimalDigits.Value ? decimalDigits.Value : s[1].Length);
                }
            }

            return decimalDigits.HasValue ? value.FillLeftWithZeros(decimalDigits.Value) : value;
        }

        public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
        {
            if (removeCurrencySymbol)
            {
                value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
            }

            return value;
        }
    }
}
