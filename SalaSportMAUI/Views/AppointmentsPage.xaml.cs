using SalaSportMAUI.Models;
using SalaSportMAUI.Services;
using System.Collections.ObjectModel;

namespace SalaSportMAUI.Views;

public partial class AppointmentsPage : ContentPage
{
    private readonly AppointmentsService _service;

    public ObservableCollection<AppointmentModel> Appointments { get; set; }

    public AppointmentsPage()
    {
        InitializeComponent();

        _service = new AppointmentsService();
        Appointments = new ObservableCollection<AppointmentModel>();

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Appointments.Clear();

        var appointments = await _service.GetAppointmentsAsync();

        foreach (var appointment in appointments)
        {
            Appointments.Add(appointment);
        }
    }
}
