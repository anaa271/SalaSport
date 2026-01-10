using SalaSportMAUI.Models;
using System.Collections.ObjectModel;

namespace SalaSportMAUI.Views;

public partial class AppointmentsPage : ContentPage
{
    public ObservableCollection<AppointmentModel> Appointments { get; set; }

    public AppointmentsPage()
    {
        InitializeComponent();

        Appointments = new ObservableCollection<AppointmentModel>
        {
            new AppointmentModel
            {
                AppointmentId = 1,
                Date = DateTime.Today,
                TrainerName = "Ion Popescu",
                Status = "Confirmed"
            },
            new AppointmentModel
            {
                AppointmentId = 2,
                Date = DateTime.Today.AddDays(1),
                TrainerName = "Maria Ionescu",
                Status = "Pending"
            }
        };

        BindingContext = this;
    }
}
