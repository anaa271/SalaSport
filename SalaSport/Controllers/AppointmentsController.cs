using Microsoft.AspNetCore.Mvc;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly SalaSportContext _context;

    public AppointmentsController(SalaSportContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAppointments()
    {
        var appointments = _context.Appointment.ToList();
        return Ok(appointments);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Appointment.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(appointment);
    }
    [HttpGet("member/{memberId}")]
    public IActionResult GetAppointmentsForMember(int memberId)
    {
        var appointments = _context.Appointment
            .Where(a => a.MemberId == memberId)
            .ToList();

        return Ok(appointments);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
    {
        if (id != appointment.AppointmentId) return BadRequest("Id mismatch");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existing = await _context.Appointment.FindAsync(id);
        if (existing == null) return NotFound();

        existing.TrainerId = appointment.TrainerId;
        existing.StartTime = appointment.StartTime;
        existing.EndTime = appointment.EndTime;
        existing.Status = appointment.Status;
        existing.Notes = appointment.Notes;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var existing = await _context.Appointment.FindAsync(id);
        if (existing == null) return NotFound();

        _context.Appointment.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }


}
