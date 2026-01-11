using SalaSportMAUI.Models;
using SalaSportMAUI.Services;
using System.Collections.ObjectModel;

namespace SalaSportMAUI.Views;

public partial class TrainersPage : ContentPage
{
    public ObservableCollection<TrainerModel> Trainers { get; set; }

    private readonly TrainersService _trainersService;

    public TrainersPage()
    {
        InitializeComponent();

        _trainersService = new TrainersService();
        Trainers = new ObservableCollection<TrainerModel>();

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var items = await _trainersService.GetTrainersAsync();
        Trainers.Clear();

        foreach (var item in items)
        {
            Trainers.Add(item);
        }
    }
}
