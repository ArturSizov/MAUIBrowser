using SQLite;

namespace MAUIBrowser.DataAccessLayer.DAO
{
    public class FastLinkInfoDAO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Url { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
    }
}
