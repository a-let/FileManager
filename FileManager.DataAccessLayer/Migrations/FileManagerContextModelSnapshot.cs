﻿// <auto-generated />
using FileManager.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileManager.DataAccessLayer.Migrations
{
    [DbContext(typeof(FileManagerContext))]
    partial class FileManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileManager.Models.Episode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EpisodeNumber");

                    b.Property<string>("Format");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int>("SeasonId");

                    b.HasKey("EpisodeId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("FileManager.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Format");

                    b.Property<bool>("IsSeries");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int>("SeriesId");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("FileManager.Models.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path");

                    b.Property<int>("SeasonNumber");

                    b.Property<int>("ShowId");

                    b.HasKey("SeasonId");

                    b.HasIndex("ShowId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("FileManager.Models.Series", b =>
                {
                    b.Property<int>("SeriesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("SeriesId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("FileManager.Models.Show", b =>
                {
                    b.Property<int>("ShowId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("ShowId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("FileManager.Models.Episode", b =>
                {
                    b.HasOne("FileManager.Models.Season")
                        .WithMany("EpisodeList")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FileManager.Models.Season", b =>
                {
                    b.HasOne("FileManager.Models.Show")
                        .WithMany("Seasons")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}