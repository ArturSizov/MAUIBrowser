using MAUIBrowser.Abstractions;
using MAUIBrowser.Models;
using System.Collections.ObjectModel;

namespace MAUIBrowser.State
{
    public class BrowserState : BindableObject, IRepository<HistoryModel>
    {

        #region Private property 
        private IHistoryData<HistoryModel> data;

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

        public BrowserState(IHistoryData<HistoryModel> data)
        {
            this.data = data;
            data.DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "history.db");
            Task.Run(async() => Histories = new ObservableCollection<HistoryModel>(await GetAllHistory())).Wait();
        }

        #region Methods

        // Get all history
        private async Task<IList<HistoryModel>> GetAllHistory()
        {
            return new ObservableCollection<HistoryModel>(await data.GetAllAsync()).ToList();
        }

        /// <summary>
        /// Insert new history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task Insert(HistoryModel history)
        {
            Histories.Add(history);
            await data.InsertAsync(history);
        }

        /// <summary>
        /// Remove one history
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public async Task Remove(HistoryModel history)
        {
            Histories.Remove(history);
            await data.RemoveAsync(history);
        }

        /// <summary>
        /// Remove all histories
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAll()
        {
            foreach (var item in Histories)
            {
                await data.RemoveAsync(item);
            }
            Histories.Clear();
        }

        #endregion
    }
}
