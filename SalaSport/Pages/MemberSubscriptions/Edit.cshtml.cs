using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.MemberSubscriptions
{
    public class EditModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public EditModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MemberSubscription MemberSubscription { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membersubscription =  await _context.MemberSubscription.FirstOrDefaultAsync(m => m.MemberSubscriptionId == id);
            if (membersubscription == null)
            {
                return NotFound();
            }
            MemberSubscription = membersubscription;
           ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email");
           ViewData["SubscriptionId"] = new SelectList(_context.Set<Subscription>(), "SubscriptionId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MemberSubscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberSubscriptionExists(MemberSubscription.MemberSubscriptionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MemberSubscriptionExists(int id)
        {
            return _context.MemberSubscription.Any(e => e.MemberSubscriptionId == id);
        }
    }
}
