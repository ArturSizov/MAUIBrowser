using MAUIBrowser.Abstractions;
using MAUIBrowser.Auxiliary;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class HomePanelViewModel : BindableObject
    {
        #region Private property 
        private IWebViewServices web;
        private BrowserState state;
        private string url = string.Empty;
        #endregion

        #region Public property 
        public string Url
        {
            get => url; 
            set
            {
                url = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public HomePanelViewModel(BrowserState state, IWebViewServices web)
        {
            this.web = web;
            this.state = state;
        }
        #region Commands

        /// <summary>
        /// Enter address command
        /// </summary>
        public ICommand EnterAddressCommand => new Command<string>((url) =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            var target = WebViewSourceBuilder.Create(url);

            var tab = new TabInfoModel
            {
                Url = target,
                Title = url,
                Content = new BrowserTabPage(web) 
                {
                    BindingContext = new BrowserTabPageModel(state)
                    {
                        Url =  target
                    }
                }
            };

            if (state.CurrentTab != null)
            {
                var index = state.Tabs.IndexOf(state.CurrentTab);

                if (index != -1)
                    state.Tabs[index] = tab;
            }

            else
                state.Tabs.Add(tab);


            state.CurrentTab = tab;

            contentPage.Content = tab.Content;

        });
        #endregion
    }
}
