using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly SalaSportContext _context;

        public DeleteModel(SalaSportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainer
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TrainerId == id);

            if (trainer == null) return NotFound();

            Trainer = trainer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer == null) return RedirectToPage("./Index");

            // Nu ștergem dacă există programări legate de trainer
            var hasAppointments = await _context.Appointment
                .AnyAsync(a => a.TrainerId == trainer.TrainerId);

            if (hasAppointments)
            {
                Trainer = trainer;
                ModelState.AddModelError(string.Empty,
                    "Cannot delete trainer because there are appointments assigned to them.");
                return Page();
            }

            try
            {
                _context.Trainer.Remove(trainer);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                Trainer = trainer;
                ModelState.AddModelError(string.Empty,
                    "Delete failed due to related records in the database.");
                return Page();
            }
        }
    }
}
