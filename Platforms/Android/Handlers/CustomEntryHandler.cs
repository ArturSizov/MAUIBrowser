using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;

namespace MAUIBrowser.Platforms.Android.Handlers
{
    public class CustomEntryHandler : EntryHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            platformView.Background = null;
            base.ConnectHandler(platformView);
        }
    }
}
