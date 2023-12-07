using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMY.Restaurant.Infrastructure.Persistence.Contexts
{
    public class EMYRestaurantDbContext : DbContext
    {

        public EMYRestaurantDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MailList> MailLists { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupRole> UserGroupRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<SliderContent> SliderContents { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CreatedAndLastUpdatedDate();
            if (Validation()) return 0;
            return await base.SaveChangesAsync(cancellationToken);
        }

        private bool Validation()
        {
            bool error = false;
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added || EntityState.Deleted == entry.State
                            select entry.Entity);
            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {

                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    if (Debugger.IsAttached)
                    {
                        Debug.Indent();
                        foreach (var valid in validationResults)
                        {
                            Debug.WriteLine(string.Join(",", valid.MemberNames) + " is not validated! Error:" + valid.ErrorMessage);
                        }

                        Debug.Unindent();
                    }
                    error = true;
                }
            }
            return error;
        }

        private void CreatedAndLastUpdatedDate()
        {
            var modifiedEntities = ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in modifiedEntities)
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedAt = DateTime.UtcNow;
                        item.Entity.UpdatedAt = DateTime.UtcNow;
                        item.Entity.IsDeleted = false;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}

