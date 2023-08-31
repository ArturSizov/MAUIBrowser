using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class HistoryPopupViewModel : BindableObject
    {
        private IHistoryPopupService _historyPopupService;
        private IWebViewService<WebView> _webViewService;
        private HistoryModel? selectHistory;
        public IBrowserStateManager<HistoryModel> HistoryManager { get; }
        private BrowserState _state;

        public HistoryModel? SelectHistory
        {
            get => selectHistory;
            set
            {
                selectHistory = value;
                OnPropertyChanged();
            }
        }

        public HistoryPopupViewModel(IBrowserStateManager<HistoryModel> historyManager, IHistoryPopupService historyPopupService, IWebViewService<WebView> webViewService, BrowserState state)
        {
            _webViewService = webViewService;
            _historyPopupService = historyPopupService;
			HistoryManager = historyManager;
            _state = state;
        }

        #region Commands 

        /// <summary>
        /// Close Popup command
        /// </summary>
        public ICommand CloseCommand => new Command(async() =>
        {
           await _historyPopupService.CloseAsync();
        });

        /// <summary>
        /// History select command
        /// </summary>
        public ICommand HistorySelectedCommand => new Command(async() =>
        {
            if (SelectHistory == null || Application.Current?.MainPage is not ContentPage contentPage)
                return;

            contentPage.Content = new BrowserTabPage(_webViewService)
            {
                BindingContext = new BrowserTabPageModel(HistoryManager)
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

            _state.Tabs.Add(tap);

            SelectHistory = null;
            await _historyPopupService.CloseAsync();
        });

        /// <summary>
        /// Delete history command
        /// </summary>
        public ICommand DeleteHistoryCommand => new Command<HistoryModel>(async (item) =>
        {
            if (item == null)
                return;

           await HistoryManager.DeleteAsync(item);
        });

        /// <summary>
        /// Delete history command
        /// </summary>
        public ICommand DeleteAllHistoryCommand => new Command(async() =>
        {
            await HistoryManager.DeleteAllAsync();
            await _historyPopupService.CloseAsync();
        });
        #endregion
    }
}
