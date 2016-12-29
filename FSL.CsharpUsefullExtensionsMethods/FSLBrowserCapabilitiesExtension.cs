namespace FSL.CsharpUsefullExtensionsMethods
{
    public static class FSLBrowserCapabilitiesExtension
    {
        public static bool IsSafari(this System.Web.HttpBrowserCapabilitiesBase browser)
        {
            return browser.Browser.ToLower().Equals("safari");
        }
        
        public static bool IsFirefox(this System.Web.HttpBrowserCapabilitiesBase browser)
        {
            return browser.Browser.ToLower().Equals("firefox");
        }
        
        public static bool IsIE(this System.Web.HttpBrowserCapabilitiesBase browser)
        {
            return browser.Browser.ToLower().Equals("ie");
        }
    }
}
