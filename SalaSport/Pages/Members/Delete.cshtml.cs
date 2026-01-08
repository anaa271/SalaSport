using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Members
{
    public class DeleteModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DeleteModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FirstOrDefaultAsync(m => m.MemberId == id);

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                Member = member;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                Member = member;
                _context.Member.Remove(Member);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
