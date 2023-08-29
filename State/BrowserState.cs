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

        public FastLinksState FastLinksState { get; }
        public HistoryState HistoryState { get; }

        public SearchEngineState SearchEngineState { get; }

        #endregion

        public BrowserState(FastLinksState fastLinksState, HistoryState historyState, SearchEngineState searchEngineState)
        {
            FastLinksState = fastLinksState;
            HistoryState = historyState;
            SearchEngineState = searchEngineState;
        }
    }
}
