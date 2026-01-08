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
    public class IndexModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public IndexModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        public IList<Subscription> Subscription { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Subscription = await _context.Subscription.ToListAsync();
        }
    }
}
