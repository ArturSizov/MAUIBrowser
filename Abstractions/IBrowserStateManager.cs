using System.Collections.ObjectModel;

namespace MAUIBrowser.Abstractions
{
	/// <summary>
	/// Browser state manager interface
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IBrowserStateManager<T> : IBasicCRUD<T>
	{
		/// <summary>
		/// Observable items
		/// </summary>
		ObservableCollection<T> Items { get; set; }
	}
}
