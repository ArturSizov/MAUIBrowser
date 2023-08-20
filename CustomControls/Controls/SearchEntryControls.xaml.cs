using Prism.Commands;
using System.Windows.Input;

namespace MAUIBrowser.CustomControls.Controls;

public partial class SearchEntryControls : Grid
{
	public SearchEntryControls()
	{
		InitializeComponent();
	}

    #region Public property
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
    public new bool IsVisible { get => (bool)GetValue(IsVisibleProperty); set { SetValue(IsVisibleProperty, value); } }
    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set { SetValue(PlaceholderProperty, value); } }
    public int CountPersons { get => (int)GetValue(CountPersonsProperty); set { SetValue(CountPersonsProperty, value); } }

    #endregion

    #region BindableProperty
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SearchEntryControls), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: TextPropertyPropertyChanget);

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(SearchEntryControls));

    public static readonly new BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(SearchEntryControls), true, BindingMode.TwoWay,
       propertyChanged: IsVisiblePropertyChanget);

    public static readonly BindableProperty CountPersonsProperty = BindableProperty.Create(nameof(CountPersons), typeof(int), typeof(SearchEntryControls));

    #endregion

    #region Events
    private void TxtEntry_Focused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(Text)) lblPlaceholder.IsVisible = true;
        else lblPlaceholder.IsVisible = false;
//#if ANDROID
//        Platform.CurrentActivity.ShowKeyboard(Platform.CurrentActivity.CurrentFocus); //Open keyboard
//#endif
    }

    private void TxtEntry_Unfocused(object sender, FocusEventArgs e)
    {
        lblPlaceholder.IsVisible = true;
        IsVisible = false;
        TxtEntry.IsEnabled = true;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        TxtEntry.Focus();
        TxtEntry.IsEnabled = false;
        IsVisible = false;
    }

    #endregion

    #region Methods
    private static void TextPropertyPropertyChanget(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SearchEntryControls)bindable;
        var text = control.TxtEntry.Text = newValue as string;

        if (string.IsNullOrEmpty(text))
        {
            control.lblPlaceholder.IsVisible = true;
        }
        else control.lblPlaceholder.IsVisible = false;
    }
    private static void IsVisiblePropertyChanget(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SearchEntryControls)bindable;

        if ((bool)newValue)
        {
            control.searchBorder.IsVisible = true;
            control.lblPlaceholder.IsVisible = true;
            control.TxtEntry.Focus();
            control.TxtEntry.IsEnabled = true;
            control.TxtEntry.IsVisible = true;
        }
        else
        {
            control.TxtEntry.Text = null;
            control.searchBorder.IsVisible = false;
            control.lblPlaceholder.IsVisible = false;
            control.TxtEntry.IsEnabled = false;
        }
    }
    #endregion

    #region Commands
    public ICommand ReturnCommand => new DelegateCommand(() =>
    {
        if (CountPersons == 0)
        {
            TxtEntry.Text = null;
            searchBorder.IsVisible = false;
            lblPlaceholder.IsVisible = false;
            TxtEntry.IsEnabled = false;
            IsVisible = false;

        }
        else
        {
//#if ANDROID
//        Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus); //Close keyboard

//#endif
            if (string.IsNullOrEmpty(TxtEntry.Text))
            {
                lblPlaceholder.IsVisible = true;
                IsVisible = false;
                TxtEntry.IsEnabled = true;
            }
        }
    });
    #endregion
}