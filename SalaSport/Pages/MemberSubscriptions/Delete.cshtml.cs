using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.MemberSubscriptions
{
    public class DeleteModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DeleteModel(SalaSport.Data.SalaSportContext context)
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

            var membersubscription = await _context.MemberSubscription.FirstOrDefaultAsync(m => m.MemberSubscriptionId == id);

            if (membersubscription == null)
            {
                return NotFound();
            }
            else
            {
                MemberSubscription = membersubscription;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membersubscription = await _context.MemberSubscription.FindAsync(id);
            if (membersubscription != null)
            {
                MemberSubscription = membersubscription;
                _context.MemberSubscription.Remove(MemberSubscription);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
