using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Pages.My;

public class AppointmentsModel : PageModel
{
    private readonly SalaSportContext _context;

    public AppointmentsModel(SalaSportContext context)
    {
        _context = context;
    }

    public List<Appointment> Appointments { get; set; } = new();
    public SelectList TrainerOptions { get; set; } = default!;
    public string? ErrorMessage { get; set; }

    [BindProperty]
    public CreateInput Input { get; set; } = new();

    public class CreateInput
    {
        [Required]
        [Display(Name = "Trainer")]
        public int TrainerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;

        [Range(15, 240)]
        [Display(Name = "Duration (minutes)")]
        public int DurationMinutes { get; set; } = 60;

        [StringLength(200)]
        public string? Notes { get; set; }
    }

    public async Task OnGetAsync()
    {
        await LoadAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadAsync(); // ca să ai trainer dropdown + lista încărcate

        var email = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(email))
        {
            ErrorMessage = "You are not logged in.";
            return Page();
        }

        var member = await _context.Member
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Email == email);

        if (member == null)
        {
            ErrorMessage = "No Member record found for your account. (Member.Email must match your login email.)";
            return Page();
        }

        if (!ModelState.IsValid)
            return Page();

        var start = Input.Date.Date.Add(Input.Time);
        var end = start.AddMinutes(Input.DurationMinutes);

        if (end <= start)
        {
            ModelState.AddModelError(string.Empty, "End time must be after start time.");
            return Page();
        }

        var appointment = new Appointment
        {
            MemberId = member.MemberId,
            TrainerId = Input.TrainerId,
            StartTime = start,
            EndTime = end,
            Status = AppointmentStatus.Pending,
            Notes = Input.Notes
        };

        _context.Appointment.Add(appointment);
        await _context.SaveChangesAsync();

        return RedirectToPage(); // refresh
    }

    private async Task LoadAsync()
    {
        // trainer dropdown
        var trainers = await _context.Trainer
            .AsNoTracking()
            .OrderBy(t => t.FullName)
            .Select(t => new { t.TrainerId, t.FullName })
            .ToListAsync();

        TrainerOptions = new SelectList(trainers, "TrainerId", "FullName");

        // lista programărilor membrului logat
        var email = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(email))
            return;

        var member = await _context.Member
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Email == email);

        if (member == null)
            return;

        Appointments = await _context.Appointment
            .AsNoTracking()
            .Where(a => a.MemberId == member.MemberId)
            .OrderByDescending(a => a.StartTime)
            .ToListAsync();
    }
}
