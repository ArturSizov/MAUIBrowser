using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class SearchEngineState
    {
        public ObservableCollection<SearchEngineModel> SearchEngines { get; set; } = new()
        {
                new()
                {
                    Image = "google.png",
                    SearchQuery = "https://www.google.com/search?q="
                },
                new()
                {
                    Image = "rambler.png",
                    SearchQuery = "https://nova.rambler.ru/search?query="
                },
                new()
                {
                    Image = "yandex.png",
                    SearchQuery = "https://ya.ru/search/?text="
                }
        };
    }
}
