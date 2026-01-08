using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Subscriptions
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
            return Page();
        }

        [BindProperty]
        public Subscription Subscription { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Subscription.Add(Subscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
