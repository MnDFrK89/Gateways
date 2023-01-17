
using Gateways.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gateways.Data
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Peripheral> Peripherals { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentDate = DateTime.Now;

            var currentChanges = ChangeTracker.Entries<BaseEntity>();
            var currentChangedList = currentChanges.ToList();

            foreach (var entry in currentChangedList)
            {
                var entity = entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = currentDate;
                        entry.Entity.ModifiedAt = currentDate;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = currentDate;
                        entry.Entity.CreatedAt = entry.OriginalValues.GetValue<DateTime>("CreatedAt");
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Deleted:
                        break;

                    case EntityState.Unchanged:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Peripheral>()
               .HasOne(u => u.Gateway)
               .WithMany(c => c.Peripherals)
               .HasForeignKey(c => c.GatewayId);

            base.OnModelCreating(modelBuilder);
        }
    }
}