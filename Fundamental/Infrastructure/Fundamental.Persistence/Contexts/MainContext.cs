using Fundamental.Domain.Entities;
using Fundamental.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Persistence.Contexts
{
    public class MainContext : IdentityDbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }   
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<MenuSlider> MenuSliders { get; set; }
        public DbSet<StorePalacedType> StorePalacedTypes { get; set; }
        public DbSet<Store> Store{ get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<SosialMedia> SosialMedias { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entiteis = ChangeTracker.Entries<BaseEntity>();

            foreach (var item in entiteis)
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                    item.Entity.UpdateAt = DateTime.UtcNow.AddHours(4);
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.UpdateAt = DateTime.UtcNow.AddHours(4);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
