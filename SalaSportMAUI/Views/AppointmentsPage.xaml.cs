using System.Collections.ObjectModel;
using SalaSportMAUI.Models;
using SalaSportMAUI.Services;

namespace SalaSportMAUI.Views;

public partial class AppointmentsPage : ContentPage
{
    private readonly AppointmentsService _appointmentsService;

    public ObservableCollection<AppointmentModel> Appointments { get; } = new();

    public AppointmentsPage()
    {
        InitializeComponent();

        _appointmentsService = new AppointmentsService();
        BindingContext = this;

        StartDatePicker.Date = DateTime.Today;
        StartTimePicker.Time = DateTime.Now.TimeOfDay;
        DurationPicker.SelectedIndex = 1; // 60
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
        foreach (var a in appointments.OrderBy(x => x.StartTime))
        {
            Appointments.Add(a);
            NotificationService.ScheduleAppointmentReminder(a);
        }
    }

    private async void OnAddAppointmentClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(TrainerIdEntry.Text, out int trainerId))
        {
            await DisplayAlert("Error", "Invalid Trainer ID", "OK");
            return;
        }

        var start = StartDatePicker.Date.Add(StartTimePicker.Time);

        var minutes = 60;
        if (DurationPicker.SelectedItem is string s && int.TryParse(s, out var parsed))
            minutes = parsed;

        var end = start.AddMinutes(minutes);

        if (end <= start)
        {
            await DisplayAlert("Error", "End time must be after start time.", "OK");
            return;
        }

        var newAppointment = new AppointmentModel
        {
            MemberId = CurrentMember.MemberId,
            TrainerId = trainerId,
            StartTime = start,
            EndTime = end,
            Status = 1,
            Notes = NotesEntry.Text
        };

        var created = await _appointmentsService.AddAppointmentAsync(newAppointment);
        if (created != null)
            NotificationService.ScheduleAppointmentReminder(created);

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

            NotificationService.CancelAppointmentReminder(selected.AppointmentId);
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
