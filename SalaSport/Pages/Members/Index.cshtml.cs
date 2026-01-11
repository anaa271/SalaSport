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
    public class IndexModel : PageModel
    {
        private readonly SalaSport.Data.SalaSportContext _context;

        public IndexModel(SalaSport.Data.SalaSportContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Member = await _context.Member
    .Where(m => m.Email != "admin1@salasport.local")
    .ToListAsync();

        }
    }
}
