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
    public class DetailsModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DetailsModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

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
    }
}
