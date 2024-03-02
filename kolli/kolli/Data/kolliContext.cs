using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kolli.Models;

namespace kolli.Data
{
    public class kolliContext : DbContext
    {
        public kolliContext (DbContextOptions<kolliContext> options)
            : base(options)
        {
        }

        public DbSet<kolli.Models.Car> Car { get; set; }

        public DbSet<kolli.Models.Flowers> Flowers { get; set; } = default!;
    }
}
