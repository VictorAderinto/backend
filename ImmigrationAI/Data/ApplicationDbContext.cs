using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ImmigrationAI.Models;

namespace ImmigrationAI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ImmigrationAI.Models.User> User { get; set; } = default!;
        public DbSet<ImmigrationAI.Models.Conversation> Conversation { get; set; } = default!;
        public DbSet<ImmigrationAI.Models.Message> Message { get; set; } = default!;
    }
}
