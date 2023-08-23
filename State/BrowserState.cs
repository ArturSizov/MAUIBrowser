﻿using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class BrowserState : BindableObject
    {
        public ObservableCollection<TabInfoModel> Tabs { get; } = new();
        public ObservableCollection<string> Links { get; set; } = new();

        public TabInfoModel CurrentTab;
    }
}
