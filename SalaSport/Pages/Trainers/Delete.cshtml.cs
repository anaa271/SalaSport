using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DeleteModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer.FirstOrDefaultAsync(m => m.TrainerId == id);

            if (trainer == null)
            {
                return NotFound();
            }
            else
            {
                Trainer = trainer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer != null)
            {
                Trainer = trainer;
                _context.Trainer.Remove(Trainer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
