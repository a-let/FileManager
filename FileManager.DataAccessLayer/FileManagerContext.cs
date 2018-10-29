using FileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManager.DataAccessLayer
{
    public class FileManagerContext : DbContext
    {
        public FileManagerContext(DbContextOptions<FileManagerContext> options)
            : base(options) { }

        public DbSet<Episode> Episode { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Show> Show { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Series> Series { get; set; }
    }
}