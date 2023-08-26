using System.Net;

namespace MAUIBrowser.Auxiliary
{
    public static class WebViewSourceBuilder
    {
        /// <summary>
        /// Create new url
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string Create(string request)
        {
            if (Uri.TryCreate(request, UriKind.Absolute, out _))
                return request;
            if(request is not null)
                request = request.Replace(" ", "+");

            if (request.Split('.').Length == 1 && request.Split(' ').Length == 1)
                return $"https://www.google.com/search?q={request}";

            var testUrl = $"https://{request}";
            if (Uri.TryCreate(testUrl, UriKind.Absolute, out var uri) && UrlExists(uri))
                return testUrl;

            return $"https://www.google.com/search?q={request}";
        }

        private static bool UrlExists(Uri uri)
        {
            try
            {
                var dns = Dns.GetHostEntry(uri.Host);
                return dns != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
