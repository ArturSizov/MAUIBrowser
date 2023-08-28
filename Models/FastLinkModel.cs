using SQLite;

namespace MAUIBrowser.Models
{
    public class FastLinkModel : BindableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string IconSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Url) || !Uri.TryCreate(Url, UriKind.Absolute, out var uri) || uri == null)
                    return "uranium.png";

                return $"{uri.Scheme}://{uri.Host}/favicon.ico";
            }
        }
    }
}
