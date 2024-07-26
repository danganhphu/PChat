using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PChat.Domain.Entities;
using PChat.Domain.Abstractions;

namespace PChat.Persistance.Context;

public sealed class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet")) {
                entityType.SetTableName (tableName.Substring (6));
            }
        }
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefence).Assembly);
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
        modelBuilder.Ignore<IdentityRole<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
    }
    

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entires = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entires)
        {
            if(entry.State == EntityState.Added)
                entry.Property(p=> p.CreatedDate)
                    .CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property(p => p.UpdatedDate)
                    .CurrentValue = DateTime.Now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
    //                  .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
    //     {
    //         entry.Entity.DateModified = DateTime.Now;
    //         entry.Entity.ModifiedBy = _userService.UserId;
    //         if (entry.State == EntityState.Added)
    //         {
    //             entry.Entity.DateCreated = DateTime.Now;
    //             entry.Entity.CreatedBy = _userService.UserId;
    //         }
    //     }
    //
    //     return base.SaveChangesAsync(cancellationToken);
    // }
}