using SQLite;

namespace MAUIBrowser.DataAccessLayer.DAO
{
    public class HistoryInfoDAO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;   

        public DateTime Date { get; set; }  

        public string Url { get; set; } = string.Empty;
    }
}
