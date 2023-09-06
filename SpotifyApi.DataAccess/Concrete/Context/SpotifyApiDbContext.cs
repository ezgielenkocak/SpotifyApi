using Microsoft.EntityFrameworkCore;
using SpotifyApi.Core.Entities.Concrete;
using SpotifyApi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.DataAccess.Concrete.Context
{
    public class SpotifyApiDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=your-server-name;Database=your-db-name;Uid=sa;Password=your-password;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistFollower> PlaylistFollowers { get; set; }
        public DbSet<Album> Albumss { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<Library> Libaries { get; set; }

        public DbSet<Favourite> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18, 8)");
            }
        }
    }
}
