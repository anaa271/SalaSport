using SalaSportMAUI.Models;
using System.Collections.ObjectModel;

namespace SalaSportMAUI.Views;

public partial class TrainersPage : ContentPage
{
    public ObservableCollection<TrainerModel> Trainers { get; set; }

    public TrainersPage()
    {
        InitializeComponent();

        Trainers = new ObservableCollection<TrainerModel>
        {
            new TrainerModel { TrainerId = 1, FullName = "Ion Popescu" },
            new TrainerModel { TrainerId = 2, FullName = "Maria Ionescu" }
        };

        BindingContext = this;
    }
}
