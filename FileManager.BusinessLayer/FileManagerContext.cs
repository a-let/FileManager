using System.Data.Entity;

namespace FileManager.BusinessLayer
{
    public class FileManagerContext : DbContext
    {
        public FileManagerContext() : base()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<FileManagerContext>());
        }

        public DbSet<Series> Series { get; set; }
        public DbSet<Movie> Movie { get; set; }
        
        public DbSet<Show> Show { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Episode> Episode { get; set; }
    }
}