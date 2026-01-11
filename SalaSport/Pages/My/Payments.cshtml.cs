using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.My;

public class PaymentsModel : PageModel
{
    private readonly SalaSportContext _context;

    public PaymentsModel(SalaSportContext context)
    {
        _context = context;
    }

    public List<Payment> Payments { get; set; } = new();
    public string? ErrorMessage { get; set; }

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

        Payments = await _context.Payment
            .AsNoTracking()
            .Where(p => p.MemberId == member.MemberId)
            .Include(p => p.MemberSubscription).ThenInclude(ms => ms.Subscription)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync();
    }
}
