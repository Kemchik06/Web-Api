using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoviesShopApi.Models
{
    public class MyApiContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<AudioBook> AudioBooks { get; set; }


        public MyApiContext(DbContextOptions<MyApiContext> options)
            : base(options)
        { }
    }
}
