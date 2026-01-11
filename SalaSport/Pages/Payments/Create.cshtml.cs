using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Payments;

[Authorize(Policy = "AdminPolicy")]
public class CreateModel : PageModel
{
    private readonly SalaSportContext _context;

    public CreateModel(SalaSportContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Payment Payment { get; set; } = new();

    public SelectList MemberOptions { get; set; } = default!;
    public SelectList AppointmentOptions { get; set; } = default!;
    public SelectList MemberSubscriptionOptions { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await LoadMembersAsync();

        // default values
        Payment.PaidAt = DateTime.Now;
        // Payment.ForType = PaymentType.Subscription; // dacă vrei default
        await LoadReferencesAsync(Payment.MemberId);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadMembersAsync();
        await LoadReferencesAsync(Payment.MemberId);

        // reguli simple: în funcție de ForType, trebuie să fie ales id-ul corect
        if (Payment.ForType.ToString().ToLower().Contains("appoint"))
        {
            Payment.MemberSubscriptionId = null;
            if (Payment.AppointmentId == null)
                ModelState.AddModelError("Payment.AppointmentId", "Select an appointment.");
        }
        else
        {
            Payment.AppointmentId = null;
            if (Payment.MemberSubscriptionId == null)
                ModelState.AddModelError("Payment.MemberSubscriptionId", "Select a subscription.");
        }

        if (!ModelState.IsValid)
            return Page();

        _context.Payment.Add(Payment);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private async Task LoadMembersAsync()
    {
        var members = await _context.Member
            .AsNoTracking()
            .OrderBy(m => m.FullName)
            .Select(m => new { m.MemberId, m.FullName })
            .ToListAsync();

        MemberOptions = new SelectList(members, "MemberId", "FullName");
    }

    private async Task LoadReferencesAsync(int memberId)
    {
        if (memberId <= 0)
        {
            AppointmentOptions = new SelectList(Enumerable.Empty<object>(), "AppointmentId", "Text");
            MemberSubscriptionOptions = new SelectList(Enumerable.Empty<object>(), "MemberSubscriptionId", "Text");
            return;
        }

        var appointments = await _context.Appointment
            .AsNoTracking()
            .Where(a => a.MemberId == memberId)
            .OrderByDescending(a => a.StartTime)
            .Select(a => new
            {
                a.AppointmentId,
                Text = $"#{a.AppointmentId} - {a.StartTime:dd.MM.yyyy HH:mm} - {a.EndTime:HH:mm}"
            })
            .ToListAsync();

        AppointmentOptions = new SelectList(appointments, "AppointmentId", "Text");

        var subs = await _context.MemberSubscription
            .AsNoTracking()
            .Where(ms => ms.MemberId == memberId)
            .Include(ms => ms.Subscription)
            .OrderByDescending(ms => ms.StartDate)
            .Select(ms => new
            {
                ms.MemberSubscriptionId,
                Text = $"{ms.Subscription!.Name} ({ms.StartDate:dd.MM.yyyy} - {ms.EndDate:dd.MM.yyyy})"
            })
            .ToListAsync();

        MemberSubscriptionOptions = new SelectList(subs, "MemberSubscriptionId", "Text");
    }
}
