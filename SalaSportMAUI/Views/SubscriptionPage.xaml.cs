using SalaSportMAUI.Models;
using SalaSportMAUI.Services;
using System.Collections.ObjectModel;

namespace SalaSportMAUI.Views;

public partial class SubscriptionPage : ContentPage
{
    public ObservableCollection<SubscriptionModel> Subscriptions { get; set; }

    private readonly SubscriptionsService _subscriptionsService;

    public SubscriptionPage()
    {
        InitializeComponent();

        _subscriptionsService = new SubscriptionsService();
        Subscriptions = new ObservableCollection<SubscriptionModel>();

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!CurrentMember.IsSelected)
        {
            await Shell.Current.GoToAsync("//Start");
            return;
        }

        await LoadSubscriptionsForMember(CurrentMember.MemberId);
    }

    private async Task LoadSubscriptionsForMember(int memberId)
    {
        Subscriptions.Clear();

        var items = await _subscriptionsService
            .GetSubscriptionsForMemberAsync(memberId);

        foreach (var s in items)
            Subscriptions.Add(s);
    }
}
