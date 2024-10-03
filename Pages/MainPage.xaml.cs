using ERozaniec.Misc;
using ERozaniec.Models;
using Microsoft.Maui.Controls.Shapes;

namespace ERozaniec.Pages;

public partial class MainPage : ContentPage
{
    private readonly IList<BeadModel> _beads = Enumerable.Range(1, 50)
        .Select(v => new BeadModel() { Order = v, IsCompleted = false })
        .ToList();

    public MainPage()
    {
        InitializeComponent();
        BuildBeads();
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
            Fill = bead.IsCompleted ? Brush.DarkGreen : Brush.DarkRed,
            WidthRequest = 30,
            HeightRequest = 30,
        };
    }

    private async void OnSwipedNext(object sender, SwipedEventArgs e)
    {
        BeadModel? beam = _beads.GetFirstNotCompletedBead();

        if (beam is not null)
        {
            beam.IsCompleted = true;

            BuildBeads();

            if (beam.Order % 10 == 0)
            {
                await DisplayAlert("Koniec dziesiπtka", "<<TEKST TAJEMNICY>>", "Przejdü dalej", "Anuluj");
            }
        }
    }

    private void OnSwipedPrev(object sender, SwipedEventArgs e)
    {
        BeadModel? beam = _beads.GetLastCompletedBead();

        if (beam is not null)
        {
            beam.IsCompleted = false;

            BuildBeads();
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Console.WriteLine("ABCCC");
    }
}