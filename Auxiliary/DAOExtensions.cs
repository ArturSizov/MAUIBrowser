using MAUIBrowser.DataAccessLayer.DAO;
using MAUIBrowser.Models;

namespace MAUIBrowser.Auxiliary
{
	public static class DAOExtensions
	{
		public static FastLinkModel ToModel(this FastLinkInfoDAO dao)
			=> new()
			{
				Id = dao.Id,
				Title = dao.Title,
				Url = dao.Url
			};

		public static FastLinkInfoDAO ToDAO(this FastLinkModel model)
			=> new()
			{
				Id = model.Id,
				Title = model.Title,
				Url = model.Url
			};

		public static HistoryModel ToModel(this HistoryInfoDAO dao)
			=> new()
			{
				Id = dao.Id,
				Title = dao.Title,
				Url = dao.Url,
				Date = dao.Date
			};

		public static HistoryInfoDAO ToDAO(this HistoryModel model)
			=> new()
			{
				Id = model.Id,
				Title = model.Title,
				Url = model.Url,
				Date = model.Date
			};
	}
}
