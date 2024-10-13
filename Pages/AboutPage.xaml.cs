namespace ERozaniec.Pages;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizer_TappedGithub(object sender, TappedEventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://github.com/ptakpiotr");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch
        {
        }
    }

    private async void TapGestureRecognizer_TappedOpoka(object sender, TappedEventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://opoka.org.pl/biblioteka/M/MR/rozaniec_md.html");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch
        {
        }
    }
}