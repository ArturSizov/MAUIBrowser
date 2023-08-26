using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class BrowserState : BindableObject
    {
        #region Public property 
        public ObservableCollection<TabInfoModel> Tabs { get; } = new();
        public ObservableCollection<HistoryModel> Histories { get; set; } = new();

        public TabInfoModel CurrentTab;

        public HistoryModel CurrentHistory;

        #endregion

        #region Methods
        /// <summary>
        /// Add browsing history
        /// </summary>
        /// <param name="infoModel"></param>
        public void AddHistory(HistoryModel history)
        {
            if(history is not null)
            {
               Histories.Add(history);
            }
        }
        #endregion
    }
}
