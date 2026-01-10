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
}
