using SalaSportMAUI.Models;
using System.Collections.ObjectModel;

namespace SalaSportMAUI.Views;

public partial class SubscriptionPage : ContentPage
{
    public ObservableCollection<SubscriptionModel> Subscriptions { get; set; }

    public SubscriptionPage()
    {
        InitializeComponent();

        Subscriptions = new ObservableCollection<SubscriptionModel>
        {
            new SubscriptionModel
            {
                SubscriptionId = 1,
                Type = "Monthly",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1)
            }
        };

        BindingContext = this;
    }
}
