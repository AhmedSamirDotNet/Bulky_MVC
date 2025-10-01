using BulkeyWeb.Migrations;
using BulkeyWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkeyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } // Represents the Categories table in the database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
