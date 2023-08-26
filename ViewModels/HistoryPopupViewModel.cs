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
        private IHistoryPopupServices setPopup;
        private IWebViewServices web;
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

        public HistoryPopupViewModel(IHistoryPopupServices setPopup, BrowserState browserState, IWebViewServices web)
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
                    EntryUrl = SelectHistory.Url
                }
            };
            SelectHistory = null;
            await setPopup.CloseAsync();
        });

        /// <summary>
        /// Delete history command
        /// </summary>
        public ICommand DeleteHistoryCommand => new Command<HistoryModel>((tab) =>
        {
            if (tab == null)
                return;

            BrowserState.Histories.Remove(tab);
        });

        /// <summary>
        /// Delete history command
        /// </summary>
        public ICommand DeleteAllHistoryCommand => new Command(async () =>
        {
            BrowserState.Histories?.Clear();
            await setPopup.CloseAsync();
        });
        #endregion
    }
}
