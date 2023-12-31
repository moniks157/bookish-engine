﻿using Microsoft.EntityFrameworkCore;
using Shelfie.Repository.Entities;

namespace Shelfie.Repository
{
    public class ShelfieDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public ShelfieDbContext(DbContextOptions<ShelfieDbContext> options) : base(options)
        {
        }
    }
}
