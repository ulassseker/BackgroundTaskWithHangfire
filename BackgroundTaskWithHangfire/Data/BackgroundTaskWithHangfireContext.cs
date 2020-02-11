using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackgroundTaskWithHangfire.Models;

namespace BackgroundTaskWithHangfire.Data
{
    public class BackgroundTaskWithHangfireContext : DbContext
    {
        public BackgroundTaskWithHangfireContext (DbContextOptions<BackgroundTaskWithHangfireContext> options)
            : base(options)
        {
        }

        public DbSet<Information> Information { get; set; }
    }
}
