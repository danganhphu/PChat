using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PChat.Application.Abstractions.Data;
using PChat.Domain.Entities;
using PChat.Domain.Abstractions;

namespace PChat.Persistence.Context;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User>(options), IApplicationDbContext, IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Customize the identity user table
        modelBuilder.Entity<User>(entity =>
        {
            entity.Ignore(u => u.PhoneNumber);
            entity.Ignore(u => u.PhoneNumberConfirmed);
            entity.Ignore(u => u.AccessFailedCount);
            entity.Ignore(u => u.LockoutEnabled);
            entity.Ignore(u => u.LockoutEnd);
            entity.Ignore(u => u.SecurityStamp);
            entity.Ignore(u => u.TwoFactorEnabled);
            entity.Ignore(u => u.EmailConfirmed);
            entity.Ignore(u => u.ConcurrencyStamp);
        });

        // Ignore Identity-related entities
        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
    }

    public DbSet<Call> Calls { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<GroupCall> GroupCalls { get; set; }

    public DbSet<GroupUser> GroupUsers { get; set; }
    
    public DbSet<Message> Messages { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.State == EntityState.Added)
                entry.Property(p => p.CreatedDate)
                    .CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property(p => p.UpdatedDate)
                    .CurrentValue = DateTime.Now;
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}