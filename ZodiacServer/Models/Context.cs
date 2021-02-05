using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZodiacServer.Models;

namespace ZodiacServer.NewFolder1
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Models.Types> items { get; set; }

        public DbSet<Models.Types> Type { get; set; }
    }
}
