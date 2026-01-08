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
    public class DetailsModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public DetailsModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

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
    }
}
