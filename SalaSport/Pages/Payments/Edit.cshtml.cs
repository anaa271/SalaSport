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

namespace SalaSport.Pages.Payments
{
    public class EditModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public EditModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment =  await _context.Payment.FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            Payment = payment;
           ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId");
           ViewData["MemberId"] = new SelectList(_context.Member, "MemberId", "Email");
           ViewData["MemberSubscriptionId"] = new SelectList(_context.MemberSubscription, "MemberSubscriptionId", "MemberSubscriptionId");
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

            _context.Attach(Payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(Payment.PaymentId))
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

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
