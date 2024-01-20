using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AfterlifeApp.Models;

namespace AfterlifeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AfterlifeApp.Models.Category>? Category { get; set; }
        public DbSet<AfterlifeApp.Models.Bundle>? Bundle { get; set; }
        public DbSet<AfterlifeApp.Models.Game>? Game { get; set; }
        public DbSet<AfterlifeApp.Models.Order>? Order { get; set; }
    }
}