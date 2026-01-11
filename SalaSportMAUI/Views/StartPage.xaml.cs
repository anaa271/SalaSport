using SalaSportMAUI.Services;

namespace SalaSportMAUI.Views;

public partial class StartPage : ContentPage
{
    private readonly MembersService _membersService;

    public StartPage()
    {
        InitializeComponent();

        _membersService = new MembersService(
            new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7267/")
            }
        );
    }


    private async void OnContinueClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var name = FullNameEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name))
        {
            ErrorLabel.Text = "Please enter a name.";
            ErrorLabel.IsVisible = true;
            return;
        }

        var member = await _membersService.GetByFullNameAsync(name);

        if (member == null)
        {
            ErrorLabel.Text = "Member not found.";
            ErrorLabel.IsVisible = true;
            return;
        }

        CurrentMember.MemberId = member.MemberId;
        CurrentMember.FullName = member.FullName;

        await Shell.Current.GoToAsync("//Appointments");
    }
}
