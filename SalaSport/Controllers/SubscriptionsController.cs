using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SalaSportContext _context;

    public SubscriptionsController(SalaSportContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
    {
        return await _context.Subscription.ToListAsync();
    }
    [HttpGet("member/{memberId:int}")]
    public async Task<IActionResult> GetSubscriptionsForMember(int memberId)
    {
        var subscriptions = await _context.MemberSubscription
            .AsNoTracking()
            .Where(ms => ms.MemberId == memberId)
            .Include(ms => ms.Subscription)
            .Select(ms => new
            {
                ms.MemberSubscriptionId,
                ms.SubscriptionId,
                Name = ms.Subscription!.Name,
                Months = ms.Subscription!.Months,
                Price = ms.Subscription!.Price,
                ms.StartDate,
                ms.EndDate,
                ms.IsActive
            })
            .ToListAsync();

        return Ok(subscriptions);
    }


}
