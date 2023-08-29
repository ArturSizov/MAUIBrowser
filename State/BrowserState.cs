using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    /// <summary>
    /// Main browser state
    /// </summary>
    public class BrowserState : BindableObject
    {
        /// <summary>
        /// Opened tabs
        /// </summary>
        public ObservableCollection<TabInfoModel> Tabs { get; } = new();

        /// <summary>
        /// Current tab
        /// </summary>
        public TabInfoModel? CurrentTab { get; set; } 
    }
}
