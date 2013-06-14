using System.Text.RegularExpressions;

namespace DC.Utils
{
    public class StrUtils
    {
        #region ToUpper
        public static string ToUpper(string input)
        {
            return input.ToUpper();
        }

        public static string ToUpper(string input, bool clean)
        {
            return Clean(input).ToUpper();
        }
        #endregion

        #region ToLower
        public static string ToLower(string input)
        {
            return input.ToLower();
        }

        public static string ToLower(string input, bool clean)
        {
            return Clean(input).ToLower();
        }
        #endregion

        #region ToCamel
        public static string ToCamelCase(string input, bool toLower)
        {
            return ToCamelCase(input).ToLower();
        }

        public static string ToCamelCase(string input)
        {
            var reg = new Regex(@"(^[a-z0-9]{1}|\s[a-z0-9]{1}|_[a-z0-9]{1}|-[a-z0-9]{1})"); // To camel case

            if (!reg.IsMatch(input)) return input;

            var cleaner = new Regex(@"[\s_-]+");

            var titleCalse = reg.Replace(input, ToCamelCasePart);

            return cleaner.Replace(titleCalse, "");

        }

        public static string ToCamelJson(string input)
        {
            var tc = ToCamelCase(input);

            if (string.IsNullOrEmpty(tc)) return "";

            return string.Format("{0}{1}", tc.Substring(0, 1).ToLower(), tc.Substring(1));
        }

        private static string ToCamelCasePart(Match match)
        {
            return match.Value.ToUpper();
        }
        #endregion

        #region Clean
        public static string Clean(string value)
        {
            return Clean(value, @"[\s_-]+");
        }

        public static string Clean(string value, string pattern)
        {
            var cleaner = new Regex(pattern);

            return cleaner.Replace(value, "");
        }
        #endregion
    }
}
