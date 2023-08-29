using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class BrowserState : BindableObject
    {
        #region Public property 
        public ObservableCollection<TabInfoModel> Tabs { get; } = new();

        public TabInfoModel? CurrentTab;
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

        public FastLinksState FastLinksState { get; }
        public HistoryState HistoryState { get; }

        #endregion

        public BrowserState(FastLinksState fastLinksState, HistoryState historyState)
        {
            FastLinksState = fastLinksState;
            HistoryState = historyState;
        }
    }
}
