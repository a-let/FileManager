using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Show : IFileManagerObject, IVideo
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }

        private Show() { }

        public static Show NewShow()
        {
            using (var context = new FileManagerContext())
            {
                var show = new Show();
                show = context.Show.Create();
                return show;
            }
        }

        public async void Save()
        {
            using (var context = new FileManagerContext())
            {
                context.Show.Add(this);

                if (this.ShowId == 0)
                    context.Entry(this).State = EntityState.Added;
                else
                    context.Entry(this).State = EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<Show> GetShows()
        {
            var shows = new List<Show>();

            using (var context = new FileManagerContext())
            {
                foreach(var s in context.Show)
                {
                    shows.Add(new Show()
                    {
                        ShowId = s.ShowId,
                        Name = s.Name,
                        Category = s.Category,
                        Path = s.Path
                    });
                }
            }

            return shows;
        }

        public static Show FindShow(string name)
        {
            var show = new Show();

            using (var context = new FileManagerContext())
            {
                show = context.Show
                    .SingleOrDefault(s => s.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            }

            return show;
        }
    }
}