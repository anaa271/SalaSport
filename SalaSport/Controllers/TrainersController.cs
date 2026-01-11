using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly SalaSportContext _context;

    public TrainersController(SalaSportContext context)
    {
        _context = context;
    }

    // GET: api/trainers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
    {
        return await _context.Trainer.ToListAsync();
    }
}
