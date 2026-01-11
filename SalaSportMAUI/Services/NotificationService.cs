using System.Collections.Concurrent;
using SalaSportMAUI.Models;
using Microsoft.Maui.ApplicationModel;

namespace SalaSportMAUI.Services;

public static class NotificationService
{
#if WINDOWS
    // Pentru Windows: workaround (merge doar cât timp aplicația e deschisă)
    private static readonly ConcurrentDictionary<int, Timer> _timers = new();
#endif

    public static void ScheduleAppointmentReminder(AppointmentModel appt)
    {
        if (appt == null || appt.AppointmentId <= 0)
            return;

        // nu are sens pentru trecut
        if (appt.StartTime <= DateTime.Now)
            return;

        CancelAppointmentReminder(appt.AppointmentId);

        var notifyAt = appt.StartTime.AddDays(-1);

        // dacă e prea aproape / deja trecut, pune în 5 secunde (ca să vezi că funcționează)
        if (notifyAt <= DateTime.Now.AddSeconds(5))
            notifyAt = DateTime.Now.AddSeconds(5);

#if ANDROID || IOS || MACCATALYST
        // Plugin-ul suportă doar iOS/Android; aici e OK
        var request = new Plugin.LocalNotification.NotificationRequest
        {
            NotificationId = appt.AppointmentId,
            Title = "Appointment reminder",
            Description = $"Tomorrow at {appt.StartTime:dd.MM.yyyy HH:mm} (Trainer #{appt.TrainerId})",
            Schedule = new Plugin.LocalNotification.NotificationRequestSchedule
            {
                NotifyTime = notifyAt
            }
        };

        // IMPORTANT: Show/Cancel sunt sync (NU await)
        Plugin.LocalNotification.LocalNotificationCenter.Current.Show(request);

#elif WINDOWS
        // Windows App SDK nu suportă scheduled toasts -> timer cât timp aplicația rulează
        var due = notifyAt - DateTime.Now;
        if (due < TimeSpan.Zero) due = TimeSpan.Zero;

        var timer = new Timer(_ =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Appointment reminder",
                        $"Tomorrow at {appt.StartTime:dd.MM.yyyy HH:mm} (Trainer #{appt.TrainerId})",
                        "OK");
                }
            });

            CancelAppointmentReminder(appt.AppointmentId);

        }, null, due, Timeout.InfiniteTimeSpan);

        _timers[appt.AppointmentId] = timer;
#endif
    }

    public static void CancelAppointmentReminder(int appointmentId)
    {
#if ANDROID || IOS || MACCATALYST
        Plugin.LocalNotification.LocalNotificationCenter.Current.Cancel(appointmentId);

#elif WINDOWS
        if (_timers.TryRemove(appointmentId, out var t))
            t.Dispose();
#endif
    }
}
