using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBrowser.ViewModels
{
    public class HomePanelViewModel
    {
        #region Public property 
        public string Title => "MAUI Browser";
        public string Url { get; set; } = "https://www.google.com/";
        #endregion
    }
}
