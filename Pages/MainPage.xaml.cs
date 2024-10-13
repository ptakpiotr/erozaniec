using ERozaniec.Data;
using ERozaniec.Misc;
using ERozaniec.Models;
using Microsoft.Maui.Controls.Shapes;

namespace ERozaniec.Pages;

public partial class MainPage : ContentPage
{
    private readonly IList<BeadModel> _beads = Enumerable.Range(1, 50)
        .Select(v => new BeadModel() { Order = v, IsCompleted = false })
        .ToList();

    private readonly Color[] _colors = [Color.FromHex("#2f3e46"),
        Color.FromHex("#354f52"), Color.FromHex("#52796f"),
        Color.FromHex("#84a98c"), Color.FromHex("#cad2c5")
    ];

    private readonly IDatabaseService? _databaseService;
    private readonly PartsHttpService? _partsHttpService;
    private IList<PartModel> _chosenParts = [];

    public MainPage()
    {
        _databaseService = new DatabaseService("app.db3");
        _partsHttpService = new(new());

        InitializeComponent();

        BuildBeads(true);
        SetChosenPartBasedOnPreferences();
        InitParts();
    }

    private void SetBeadsFromMemory()
    {
        int lastChosenBead = Preferences.Get("CurrentNumber", 0);

        for (int i = 0; i < lastChosenBead; i++)
        {
            _beads[i].IsCompleted = true;
        }
    }

    private void ResetBeads(bool shouldReRender = false)
    {
        foreach (BeadModel bead in _beads)
        {
            bead.IsCompleted = false;
        }

        if (shouldReRender)
        {
            BeadsContainer.Children.Clear();

            foreach (BeadModel bead in _beads)
            {
                BeadsContainer.Children.Add(BuildEllipse(bead));
            }
        }
    }

    private void BuildBeads(bool isInitial = false)
    {
        BeadsContainer.Children.Clear();

        if (isInitial)
        {
            SetBeadsFromMemory();
        }

        foreach (BeadModel bead in _beads)
        {
            BeadsContainer.Children.Add(BuildEllipse(bead));
        }
    }

    private void InitParts()
    {
        if (_partsHttpService is not null && _databaseService is not null)
        {
            IList<Models.PartModel>? parts = _partsHttpService.GetPartsAsync().GetAwaiter().GetResult();

            if (parts is not null)
            {
                _databaseService.AddManyPartsAsync(parts).GetAwaiter().GetResult();
            }
        }
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
        }
    }

    private Ellipse BuildEllipse(BeadModel bead)
    {
        return new()
        {
            Fill = bead.IsCompleted ? Brush.YellowGreen :
                new SolidColorBrush(_colors[((bead.Order - 1) / 10) % 5]),
            WidthRequest = 30,
            HeightRequest = 30
        };
    }

    private async void OnSwipedNext(object sender, SwipedEventArgs e)
    {
        BeadModel? bead = _beads.GetFirstNotCompletedBead();

        if (bead is not null)
        {
            bead.IsCompleted = true;

            BuildBeads();

            int order = bead.Order % 10;

            if (order == 1)
            {
                int msgIdx = ((bead.Order - 1) / 10) % 5;

                PartModel? part = _chosenParts.FirstOrDefault(c => c.Order == msgIdx);

                if (part is not null)
                {
                    await DisplayAlert(part.Name, part.Description, "OK", "Anuluj");
                }
            }
        }
    }

    private void OnSwipedPrev(object sender, SwipedEventArgs e)
    {
        BeadModel? bead = _beads.GetLastCompletedBead();

        if (bead is not null)
        {
            bead.IsCompleted = false;

            BuildBeads();
        }
    }

    private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && _databaseService is not null)
        {
            RosaryPart part = (RosaryPart)picker.SelectedIndex;
            Preferences.Set("ChosenPart", picker.SelectedIndex);
            ResetBeads(true);
            _chosenParts = await _databaseService.GetPartsAsync(part)
                .ConfigureAwait(false) ?? [];
        }
    }

    private void ResetBtn_Clicked(object sender, EventArgs e)
    {
        ResetBeads(true);
    }
}