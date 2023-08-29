using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MAUIBrowser.State
{
    public class FastLinksState
    {
        #region Private property 
        private IFastLinksDataProvider<FastLinkModel> fastLinksDataProvider;
        #endregion

        #region Public property 
        public ObservableCollection<FastLinkModel> Links { get; set; }
        //{
        //    new()
        //    {
        //        Title = "Google",
        //        Url = "https://google.com",
        //    },
        //    new()
        //    {
        //        Title = "github",
        //        Url = "https://github.com",
        //    },
        //    new()
        //    {
        //        Title = "Яндекс",
        //        Url = "https://ya.ru",
        //    },
        //    new()
        //    {
        //        Title = "Рамблер",
        //        Url = "https://rambler.ru",
        //    },
        //    new()
        //    {
        //        Title = "Дзен",
        //        Url = "https://dzen.ru",
        //    },
        //    new()
        //    {
        //        Title = "Пикабу",
        //        Url = "https://pikabu.ru",
        //    },
        //    new()
        //    {
        //        Title = "ВКонтакте",
        //        Url = "https://vk.ru",
        //    }
        //};
        #endregion

        public FastLinksState(IFastLinksDataProvider<FastLinkModel> fastLinksDataProvider)
        {
            this.fastLinksDataProvider = fastLinksDataProvider;
            Task.Run(async() => Links = new ObservableCollection<FastLinkModel>(await GetAllFastLinksAsync())).Wait();
        }

        #region Methods 
        // Get all history
        private async Task<IList<FastLinkModel>> GetAllFastLinksAsync()
        {
            return new ObservableCollection<FastLinkModel>(await fastLinksDataProvider.ReadAllAsync()).ToList();
        }

        /// <summary>
        /// Add new link
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task InsertAsync(FastLinkModel link)
        {
            Links.Add(link);
            await fastLinksDataProvider.CreateAsync(link);
        }

        /// <summary>
        /// Remove one link
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task RemoveAsync(FastLinkModel link)
        {
            Links.Remove(link);
            await fastLinksDataProvider.DeleteAsync(link);
        }
        #endregion
    }
}
