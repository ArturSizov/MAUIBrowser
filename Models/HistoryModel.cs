using SQLite;

namespace MAUIBrowser.Models
{
    public class HistoryModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
