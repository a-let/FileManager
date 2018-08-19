using FileManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.DataAccessLayer
{
    public class FileManagerContext : DbContext
    {
        public FileManagerContext(DbContextOptions<FileManagerContext> options)
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
    }
}