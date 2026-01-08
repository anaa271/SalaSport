using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalaSport.Models;

namespace SalaSport.Data
{
    public class SalaSportContext : DbContext
    {
        public SalaSportContext (DbContextOptions<SalaSportContext> options)
            : base(options)
        {
        }

        public DbSet<SalaSport.Models.Appointment> Appointment { get; set; } = default!;
        public DbSet<SalaSport.Models.Member> Member { get; set; } = default!;
        public DbSet<SalaSport.Models.MemberSubscription> MemberSubscription { get; set; } = default!;
        public DbSet<SalaSport.Models.Payment> Payment { get; set; } = default!;
        public DbSet<SalaSport.Models.Subscription> Subscription { get; set; } = default!;
        public DbSet<SalaSport.Models.Trainer> Trainer { get; set; } = default!;
    }
}
