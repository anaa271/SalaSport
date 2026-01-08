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
    public class DeleteModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DeleteModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription.FindAsync(id);
            if (subscription != null)
            {
                Subscription = subscription;
                _context.Subscription.Remove(Subscription);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
