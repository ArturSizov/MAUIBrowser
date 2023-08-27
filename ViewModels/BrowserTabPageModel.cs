﻿using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BrowserTabPageModel : BindableObject
    {
        #region Private property 
        private BrowserState state;
        #endregion

        #region Public property 
        public string Url { get; set; } = string.Empty;
        public string EntryUrl { get; set; } = string.Empty;
        #endregion

        public BrowserTabPageModel(BrowserState state)
        {
            this.state = state;
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
            await state.Insert(new HistoryModel
            {
                Date = DateTime.Now,
                Url = Url
            });
            OnPropertyChanged(nameof(EntryUrl));
            OnPropertyChanged(nameof(Url));
        });

        /// <summary>
        /// Refresh entry command
        /// </summary>
        public ICommand AddressEntryCompleted => new Command<WebNavigatedEventArgs>(async(args) =>
        {
            if (args.Source is not UrlWebViewSource source)
                return;
            if (args is null)
                return;

            var result = state.Histories.Any(h => h.Url == args.Url);
            if (!result)
            {
                Url = source.Url;
                EntryUrl = Url;

               await state.Insert(new HistoryModel
                {
                    Date = DateTime.Now,
                    Url = Url
                });
            }
            args = null;
            OnPropertyChanged(nameof(EntryUrl));
        });
        
        #endregion
    }
}
