using System.Collections.ObjectModel;
using SalaSportMAUI.Models;
using SalaSportMAUI.Services;

namespace SalaSportMAUI.Views;

public partial class AppointmentsPage : ContentPage
{
    private readonly AppointmentsService _appointmentsService;

    public ObservableCollection<AppointmentModel> Appointments { get; set; }

    public AppointmentsPage()
    {
        InitializeComponent();

        _appointmentsService = new AppointmentsService();
        Appointments = new ObservableCollection<AppointmentModel>();
        BindingContext = this;

        // Defaults for pickers
        StartDatePicker.Date = DateTime.Today;
        StartTimePicker.Time = DateTime.Now.TimeOfDay;

        // Duration default = 60 (index 1 in [30,60,90,120])
        DurationPicker.SelectedIndex = 1;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!CurrentMember.IsSelected)
        {
            await Shell.Current.GoToAsync("//Start");
            return;
        }

        await LoadAppointmentsForMember(CurrentMember.MemberId);
    }

    private async Task LoadAppointmentsForMember(int memberId)
    {
        var appointments = await _appointmentsService.GetAppointmentsForMemberAsync(memberId);

        Appointments.Clear();
        foreach (var a in appointments)
            Appointments.Add(a);
    }

    private async void OnAddAppointmentClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(TrainerIdEntry.Text, out int trainerId))
        {
            await DisplayAlert("Error", "Invalid Trainer ID", "OK");
            return;
        }

        // Build start datetime from date + time pickers
        var date = StartDatePicker.Date;
        var time = StartTimePicker.Time;
        var start = date.Add(time);

        // Duration minutes (default 60)
        var minutes = 60;
        if (DurationPicker.SelectedItem is string s && int.TryParse(s, out var parsed))
            minutes = parsed;

        var end = start.AddMinutes(minutes);

        if (end <= start)
        {
            await DisplayAlert("Error", "End time must be after start time.", "OK");
            return;
        }

        var appointment = new AppointmentModel
        {
            MemberId = CurrentMember.MemberId,
            TrainerId = trainerId,
            StartTime = start,
            EndTime = end,
            Status = 1,
            Notes = NotesEntry.Text
        };

        await _appointmentsService.AddAppointmentAsync(appointment);

        TrainerIdEntry.Text = "";
        NotesEntry.Text = "";

        await LoadAppointmentsForMember(CurrentMember.MemberId);
    }

    private async void OnAppointmentSelected(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as AppointmentModel;
        if (selected == null) return;

        ((CollectionView)sender).SelectedItem = null;

        var action = await DisplayActionSheet("Appointment", "Cancel", null, "Edit notes", "Delete");

        if (action == "Delete")
        {
            var ok = await DisplayAlert("Delete", "Delete this appointment?", "Yes", "No");
            if (!ok) return;

            await _appointmentsService.DeleteAppointmentAsync(selected.AppointmentId);
            await LoadAppointmentsForMember(CurrentMember.MemberId);
            return;
        }

        if (action == "Edit notes")
        {
            var newNotes = await DisplayPromptAsync("Edit notes", "Enter notes:", initialValue: selected.Notes ?? "");
            if (newNotes == null) return;

            selected.Notes = newNotes;
            await _appointmentsService.UpdateAppointmentAsync(selected);
            await LoadAppointmentsForMember(CurrentMember.MemberId);
        }
    }
}
