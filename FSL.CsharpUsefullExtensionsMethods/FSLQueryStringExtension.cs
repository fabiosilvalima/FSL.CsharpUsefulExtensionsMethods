namespace FSL.CsharpUsefulExtensionsMethods
{
    public static class FSLQueryStringExtension
    {
        public static string GetSecureQueryString(string qs)
        {
            var newQs = "";
            if (!string.IsNullOrEmpty(qs))
            {
                newQs = System.Web.HttpUtility.UrlDecode(qs);
                if (System.Text.RegularExpressions.Regex.Match(newQs, "[<';>]").Success)
                {
                    newQs = "";
                }
            }
            return newQs;
        }

        public static string GetSecure(this System.Collections.Specialized.NameValueCollection obj, string name)
        {
            return GetSecureQueryString(obj[name]);
        }

        public static string ToString(this System.Collections.Specialized.NameValueCollection obj, bool secure)
        {
            return secure ? GetSecureQueryString(obj.ToString()) : obj.ToString();
        }
    }
}
