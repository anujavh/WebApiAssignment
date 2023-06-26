global using Microsoft.EntityFrameworkCore;

namespace WebApiAssignemnt.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-71UULJJ\\SQLEXPRESS;Database=UserDetailsDB;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        //public DbSet<WebApiAssignemnt.Data.Entities.User> User { get; set; } = default!;
        public DbSet<UserDetail> UserDetails { get; set; } = default!;
        public DbSet<MessageDetails> MessageDetails { get; set; } = default!;

    }
}
