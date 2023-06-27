global using Microsoft.EntityFrameworkCore;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MessageDetails>()
                    .HasOne(e => e.SenderDetails)
                    .WithMany(e => e.sentMessages)
                    .HasForeignKey(e => e.senderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<MessageDetails>()
                    .HasOne(e => e.ReceiverDetails)
                    .WithMany(e => e.receivedMessages)
                    .HasForeignKey(e => e.receiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull); 

            //modelBuilder.Entity<MessageDetails>()
            //    .HasKey(e => e.MessageId);


        }



        //public DbSet<WebApiAssignemnt.Data.Entities.User> User { get; set; } = default!;
        public DbSet<UserDetail> UserDetails { get; set; } = default!;
        public DbSet<MessageDetails> MessageDetails { get; set; } = default!;
        public DbSet<LogRequests> LogRequests { get; set; } = default!;

    }
}
