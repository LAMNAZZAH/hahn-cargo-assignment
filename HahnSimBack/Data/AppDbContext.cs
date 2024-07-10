using HahnCargoAutomation.Server.Entities;
using HahnSimBack.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HahnCargoAutomation.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<Node> Nodes { get; set; }
        public DbSet<Edge> Edges { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<CargoTransporter> CargoTransporters { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActionLog>(entity =>
            {
                entity.HasKey(e => e.LogId);
                entity.Property(e => e.ActionType).IsRequired();
                entity.Property(e => e.ActionDetails);
                entity.Property(e => e.TimeStamp).IsRequired();
                entity.Property(e => e.Success);
                entity.Property(e => e.ErrorMessage).HasMaxLength(255);
            });

            builder.Entity<CargoTransporter>(entity =>
            {
                entity.HasKey(e => e.TransporterId);
                entity.Property(e => e.TransporterId).ValueGeneratedNever();
                entity.Property(e => e.PathString).HasColumnName("Path");
                entity.Property(e => e.PathCost);
                entity.Property(e => e.PathTime);
            });

            builder.Entity<Node>(entity =>
            {
                entity.Property(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).IsRequired();
            });

            builder.Entity<Edge>(entity =>
            {
                entity.Property(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Cost).IsRequired();
                entity.Property(e => e.Time).IsRequired();
            });

            builder.Entity<Connection>(entity =>
            {
                entity.Property(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.EdgeId);
                entity.Property(e => e.FirstNodeId).IsRequired();
                entity.Property(e => e.SecondNodeId).IsRequired();
            });
        }
    }
}

