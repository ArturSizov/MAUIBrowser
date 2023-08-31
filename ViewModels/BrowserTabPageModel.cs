using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BrowserTabPageModel : BindableObject
    {
        private readonly IBrowserStateManager<HistoryModel> _historyManager;
        private string entryUrl = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string EntryUrl { get => entryUrl; set => entryUrl = value; }

        public BrowserTabPageModel(IBrowserStateManager<HistoryModel> historyManager)
        {
			_historyManager = historyManager;
			EntryUrl = Url; 
            OnPropertyChanged(nameof(Url));
            OnPropertyChanged(nameof(EntryUrl));
        }

        #region Commands 
        /// <summary>
        /// Enter entry address command
        /// </summary>
        public ICommand EnterAddressCommand => new Command(async () =>
        {
            var url = WebViewSourceBuilder.Create(EntryUrl);
            Url = url;
            EntryUrl = url;

            await _historyManager.CreateAsync(new HistoryModel
            {
                Date = DateTime.Now,
                Url = Url,
                Title = Title
            });

            OnPropertyChanged(nameof(EntryUrl));
            OnPropertyChanged(nameof(Url));
        });

        /// <summary>
        /// Refresh entry command
        /// </summary>
        public ICommand AddressEntryCompleted => new Command<WebNavigatedEventArgs>(async(args) =>
        {
            if (args == null || args.Source is not UrlWebViewSource source)
                return;

            EntryUrl = source.Url;

            Url = EntryUrl;

			await _historyManager.CreateAsync(new HistoryModel
			{
				Date = DateTime.Now,
				Url = source.Url,
				Title = Title
			});

            args = null;

            OnPropertyChanged(nameof(EntryUrl));
        });
        #endregion
    }
}
