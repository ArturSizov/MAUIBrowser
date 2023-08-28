using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class BrowserState : BindableObject
    {

        #region Private property 
        private IHistoryDataProvider<HistoryModel> historyDataProvider;
        private IFastLinksDataProvider<FastLinkModel> fastLinksDataProvider;

        #endregion

        #region Public property 
        public ObservableCollection<TabInfoModel> Tabs { get; } = new();
        public ObservableCollection<HistoryModel> Histories { get; set; } = new();

        public TabInfoModel? CurrentTab;
        public ObservableCollection<SearchEngineModel> SearchEngines { get; set; } = new()
        {
                new()
                {
                    Image = "google.png",
                    SearchQuery = "https://www.google.com/search?q="
                },
                new()
                {
                    Image = "rambler.png",
                    SearchQuery = "https://nova.rambler.ru/search?query="
                },
                new()
                {
                    Image = "yandex.png",
                    SearchQuery = "https://ya.ru/search/?text="
                }
        };
        public ObservableCollection<FastLinkModel> Links { get; set; } = new()
        {
            new()
            {
                Title = "Google",
                Url = "https://google.com",
            },
            new()
            {
                Title = "github",
                Url = "https://github.com",
            },
            new()
            {
                Title = "Яндекс",
                Url = "https://ya.ru",
            },
            new()
            {
                Title = "Рамблер",
                Url = "https://rambler.ru",
            },
            new()
            {
                Title = "Дзен",
                Url = "https://dzen.ru",
            },
            new()
            {
                Title = "Пикабу",
                Url = "https://pikabu.ru",
            },
            new()
            {
                Title = "ВКонтакте",
                Url = "https://vk.ru",
            }
        };
        #endregion

        public BrowserState(IHistoryDataProvider<HistoryModel> historyDataProvider, IFastLinksDataProvider<FastLinkModel> fastLinksDataProvider)
        {
            this.historyDataProvider = historyDataProvider;
            this.fastLinksDataProvider = fastLinksDataProvider;

            Task.Run(async() => Histories = new ObservableCollection<HistoryModel>(await GetAllHistoryAsync())).Wait();
            //Task.Run(async () => Links = new ObservableCollection<FastLinkModel>(await GetAllFastLinksAsync())).Wait();
        }

        #region Methods

        // Get all history
        private async Task<IList<HistoryModel>> GetAllHistoryAsync()
        {
            return new ObservableCollection<HistoryModel>(await historyDataProvider.ReadAllAsync()).ToList();
        }

        // Get all history
        private async Task<IList<FastLinkModel>> GetAllFastLinksAsync()
        {
            return new ObservableCollection<FastLinkModel>(await fastLinksDataProvider.ReadAllAsync()).ToList();
        }

        /// <summary>
        /// Insert new history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task InsertAsync(HistoryModel history)
        {
            Histories.Add(history);
            await historyDataProvider.CreateAsync(history);
        }

        /// <summary>
        /// Remove one history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task RemoveAsync(HistoryModel history)
        {
            Histories.Remove(history);
            await historyDataProvider.DeleteAsync(history);
        }

        /// <summary>
        /// Remove all histories
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAllAsync()
        {
            await historyDataProvider.DeleteAllAsync();
            Histories.Clear();
        }

        #endregion
    }
}
