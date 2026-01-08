using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.MemberSubscriptions
{
    public class CreateModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public CreateModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email");
        ViewData["SubscriptionId"] = new SelectList(_context.Set<Subscription>(), "SubscriptionId", "Name");
            return Page();
        }

        [BindProperty]
        public MemberSubscription MemberSubscription { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MemberSubscription.Add(MemberSubscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
