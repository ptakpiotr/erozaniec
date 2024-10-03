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

    private readonly Color[] _colors = [Color.FromHex("#1E0705"),
        Color.FromHex("#3B0D09"), Color.FromHex("#761A12"),
        Color.FromHex("#F59992"), Color.FromHex("#FACCC9")
    ];
    private readonly IDatabaseService _databaseService;
    private IList<PartModel> _chosenParts = [];

    public MainPage(IDatabaseService databaseService)
    {
        InitializeComponent();
        BuildBeads();
        _databaseService = databaseService;
    }

    private void BuildBeads()
    {
        BeadsContainer.Children.Clear();

        foreach (BeadModel bead in _beads)
        {
            BeadsContainer.Children.Add(BuildEllipse(bead));
        }
    }

    private Ellipse BuildEllipse(BeadModel bead)
    {
        return new()
        {
            Fill = bead.IsCompleted ? Brush.YellowGreen :
                new SolidColorBrush(_colors[((bead.Order - 1) / 10) % 5]),
            WidthRequest = 30,
            HeightRequest = 30,
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

            if (order == 0)
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
        if (sender is Picker picker)
        {
            RosaryPart part = (RosaryPart)picker.SelectedIndex;
            _chosenParts = await _databaseService.GetPartsAsync(part)
                .ConfigureAwait(false) ?? [];
        }
    }
}