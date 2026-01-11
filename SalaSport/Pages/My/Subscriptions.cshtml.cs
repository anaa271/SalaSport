using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;

namespace SalaSport.Pages.My;

public class SubscriptionsModel : PageModel
{
    private readonly SalaSportContext _context;

    public SubscriptionsModel(SalaSportContext context)
    {
        _context = context;
    }

    public List<Row> Items { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public class Row
    {
        public string SubscriptionName { get; set; } = "";
        public int Months { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActiveFlag { get; set; }
        public bool ActiveNow { get; set; }
    }

    public async Task OnGetAsync()
    {
        var email = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(email))
        {
            ErrorMessage = "You are not logged in.";
            return;
        }

        var member = await _context.Member
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Email == email);

        if (member == null)
        {
            ErrorMessage = "No Member record found for your account. (Member.Email must match your login email.)";
            return;
        }

        var today = DateTime.Today;

        Items = await _context.MemberSubscription
            .AsNoTracking()
            .Where(ms => ms.MemberId == member.MemberId)
            .Include(ms => ms.Subscription)
            .OrderByDescending(ms => ms.StartDate)
            .Select(ms => new Row
            {
                SubscriptionName = ms.Subscription != null ? ms.Subscription.Name : "(unknown)",
                Months = ms.Subscription != null ? ms.Subscription.Months : 0,
                Price = ms.Subscription != null ? ms.Subscription.Price : 0,
                StartDate = ms.StartDate,
                EndDate = ms.EndDate,
                IsActiveFlag = ms.IsActive,
                ActiveNow = ms.IsActive && today >= ms.StartDate.Date && today <= ms.EndDate.Date
            })
            .ToListAsync();
    }
}
