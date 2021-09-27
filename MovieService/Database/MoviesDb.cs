using System;
using Microsoft.EntityFrameworkCore;
using MovieService.Database.Models;

namespace MovieService.Database
{
    [Serializable]
    public class MoviesDb : DbContext
    {
        public MoviesDb(DbContextOptions<MoviesDb> options) : base(options)
        {
        }

        public DbSet<SavedMovie> SavedMovies { get; set; }
        public DbSet<SentOffer> SentOffers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>().Property(m => m.ImdbRating).HasPrecision(2).HasColumnType("decimal");

            modelBuilder.Entity<SavedMovie>().HasKey(m => new { m.UserId, m.MovieId });

            modelBuilder.Entity<SentOffer>().HasKey(m => m.Id);
            modelBuilder.Entity<SentOffer>().HasIndex(o => new { o.UserId, o.SentAtUtc });
        }
    }
}