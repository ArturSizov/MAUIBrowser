using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class HistoryPopupViewModel : BindableObject
    {
        #region Private property 
        private IHistoryPopupService setPopup;
        private IWebViewService<WebView> web;
        private HistoryModel selectHistory;
        #endregion

        #region Public property 
        public BrowserState BrowserState { get; }
        public HistoryModel SelectHistory
        {
            get => selectHistory;
            set
            {
                selectHistory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public HistoryPopupViewModel(IHistoryPopupService setPopup, BrowserState browserState, IWebViewService<WebView> web)
        {
            this.web = web;
            this.setPopup = setPopup;
            BrowserState = browserState;
        }

        #region Commands 

        /// <summary>
        /// Close Popup command
        /// </summary>
        public ICommand CloseCommand => new Command(async() =>
        {
           await setPopup.CloseAsync();
        });

        /// <summary>
        /// History select command
        /// </summary>
        public ICommand HistorySelectedCommand => new Command(async() =>
        {
            if (SelectHistory == null || Application.Current?.MainPage is not ContentPage contentPage)
                return;

            contentPage.Content = new BrowserTabPage(web)
            {
                BindingContext = new BrowserTabPageModel(BrowserState)
                {
                    Url = SelectHistory.Url,
                    EntryUrl = SelectHistory.Url,
                    Title = SelectHistory.Title
                }
            };

            var tap = new TabInfoModel
            {
                Title = SelectHistory.Title,
                Url = SelectHistory.Url
            };

            BrowserState.Tabs.Add(tap);

            SelectHistory = null;
            await setPopup.CloseAsync();
        });

        /// <summary>
        /// Delete history command
        /// </summary>
        public ICommand DeleteHistoryCommand => new Command<HistoryModel>(async (tab) =>
        {
            if (tab == null)
                return;

           await BrowserState.RemoveAsync(tab);
        });

        /// <summary>
        /// Delete history command
        /// </summary>
        public ICommand DeleteAllHistoryCommand => new Command(async () =>
        {
            await BrowserState.RemoveAllAsync();
            await setPopup.CloseAsync();
        });
        #endregion
    }
}
