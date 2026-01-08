using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Subscriptions
{
    public class DetailsModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DetailsModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        public Subscription Subscription { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription.FirstOrDefaultAsync(m => m.SubscriptionId == id);
            if (subscription == null)
            {
                return NotFound();
            }
            else
            {
                Subscription = subscription;
            }
            return Page();
        }
    }
}
