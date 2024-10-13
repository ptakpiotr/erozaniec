using ERozaniec.Data;
using ERozaniec.Models;

namespace ERozaniec.Pages;

public partial class RosaryPage : ContentPage
{
    private readonly IDatabaseService? _databaseService;
    private IList<PartModel> _chosenParts = [];

    public RosaryPage()
    {
        string path = Path.Join(FileSystem.AppDataDirectory, "app.db3");

        _databaseService = new DatabaseService(new(path));
        InitializeComponent();
        SetChosenPartBasedOnPreferences();
    }

    private void SetChosenPartBasedOnPreferences()
    {
        int chosenPart = Preferences.Get("ChosenPart", int.MinValue);
        Console.WriteLine(chosenPart);
        if (chosenPart > int.MinValue && _databaseService is not null)
        {
            this.PartPicker.SelectedIndex = chosenPart;

            RosaryPart part = (RosaryPart)chosenPart;
            _chosenParts = _databaseService.GetPartsAsync(part)
                .GetAwaiter().GetResult() ?? [];

            SetPartsInfo();
        }
    }

    private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && _databaseService is not null)
        {
            RosaryPart part = (RosaryPart)picker.SelectedIndex;
            Preferences.Set("ChosenPart", picker.SelectedIndex);
            _chosenParts = await _databaseService.GetPartsAsync(part)
                .ConfigureAwait(false) ?? [];

            SetPartsInfo();
        }
    }

    private void SetPartsInfo()
    {
        if (_chosenParts.Count == 5)
        {
            Console.WriteLine("HERE 1");
            t1.Text = _chosenParts[0].Name;
            t2.Text = _chosenParts[1].Name;
            t3.Text = _chosenParts[2].Name;
            t4.Text = _chosenParts[3].Name;
            t5.Text = _chosenParts[4].Name;
            Console.WriteLine("HERE 2");
        }
    }
}