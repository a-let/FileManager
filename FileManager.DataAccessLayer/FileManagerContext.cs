using FileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FileManager.DataAccessLayer
{
    public class FileManagerContext : DbContext
    {
        public FileManagerContext(DbContextOptions<FileManagerContext> options)
            : base(options) { }

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
    }
}