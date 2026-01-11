namespace SalaSportMAUI.Services;

public static class CurrentMember
{
    public static int MemberId { get; set; }
    public static string FullName { get; set; } = "";

    public static bool IsSelected => MemberId > 0;
}
