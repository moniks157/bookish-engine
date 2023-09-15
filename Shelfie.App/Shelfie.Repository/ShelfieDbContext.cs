using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shelfie.Repository.Models;

namespace Shelfie.Repository
{
    public class ShelfieDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ShelfieDbContext(DbContextOptions<ShelfieDbContext> options) : base(options)
        {
        }
    }
}
