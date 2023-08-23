﻿using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using MAUIBrowser.Pages;
using MAUIBrowser.State;
using System.Windows.Input;

namespace MAUIBrowser.ViewModels
{
    public class BottomControlsViewModel : BindableObject
    {
        #region Private property 
        private BrowserState browserState = new();
        private ITabsPopupService popupService;
        private IWebViewServices web;
        #endregion

        #region Public property 
        public BrowserState BrowserState
        {
            get => browserState; 
            set
            {
                browserState = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public BottomControlsViewModel(ITabsPopupService popupService, IWebViewServices web, BrowserState browserState)
        {
            this.popupService = popupService;
            this.web = web;
            BrowserState = browserState;
        }

        #region Commands 
        public ICommand GoBackCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            web.GoBack();
        });

        public ICommand GoForwardCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            web.GoForward();
        });

        public ICommand OpenHomeCommand => new Command(() =>
        {
            if (Application.Current?.MainPage is not ContentPage contentPage)
                return;

            contentPage.Content = new HomePanelView();
            BrowserState.CurrentTab = null;
        });

        public ICommand OpenTabsCommand => new Command(async () =>
        {
            if (Application.Current?.MainPage == null)
                return;

            await popupService.ShowAsync();
        });
        #endregion
    }
}
