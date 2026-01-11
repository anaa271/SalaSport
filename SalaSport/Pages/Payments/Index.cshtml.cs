using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Payments;

[Authorize(Policy = "AdminPolicy")]
public class IndexModel : PageModel
{
    private readonly SalaSportContext _context;

    public IndexModel(SalaSportContext context)
    {
        _context = context;
    }

    public IList<Payment> Payments { get; set; } = new List<Payment>();

    public async Task OnGetAsync()
    {
        Payments = await _context.Payment
            .AsNoTracking()
            .Include(p => p.Member)
            .Include(p => p.Appointment)
            .Include(p => p.MemberSubscription)
                .ThenInclude(ms => ms.Subscription)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync();
    }
}
