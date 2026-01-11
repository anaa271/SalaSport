using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;
using SalaSport.Models;

namespace SalaSport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly SalaSportContext _context;

        public MembersController(SalaSportContext context)
        {
            _context = context;
        }

        [HttpGet("by-fullname")]
        public async Task<ActionResult<Member>> GetByFullName(string fullName)
        {
            var normalized = fullName.Trim().ToLower();

            var member = await _context.Member
                .FirstOrDefaultAsync(m =>
                    m.FullName != null &&
                    m.FullName.Trim().ToLower() == normalized);

            if (member == null)
                return NotFound();

            return Ok(member);
        }

    }
}
