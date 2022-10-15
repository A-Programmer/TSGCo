using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common.Utilities;
using Project.Domain.Models;

namespace Project.Infrastructure
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            #region Register All Entities
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            #endregion

            #region Apply Entities Configuration
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            #endregion

            #region Config Delete Behevior for not Cascade Delete
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            #endregion

            #region Add Sequential GUID for Id properties
            modelBuilder.AddSequentialGuidForIdConvention();
            #endregion

            #region Pluralize Table Names
            modelBuilder.AddPluralizingTableNameConvention();
            #endregion

        }




        #region Override SaveChanges methods
        public override int SaveChanges()
        {
            FixYeke();
            SetDetailFields();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            FixYeke();
            SetDetailFields();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            FixYeke();
            SetDetailFields();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FixYeke();
            SetDetailFields();
            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Fix Persian Chars
        public void FixYeke()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
        #endregion

        #region Setting detail fields

        public void SetDetailFields()
        {
            //x.State == EntityState.Modified
            var addedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added);
            foreach (var item in addedEntities)
            {
                if (item.Entity == null)
                    continue;

                var createdAtProperty = item.Entity.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == "CreatedAt");
                var modifiedAtProperty = item.Entity.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == "ModifiedAt");
                
                if (createdAtProperty != null)
                {
                    createdAtProperty.SetValue(item.Entity, DateTimeOffset.UtcNow);
                }

                if (modifiedAtProperty != null)
                {
                    modifiedAtProperty.SetValue(item.Entity, DateTimeOffset.UtcNow);
                }
            }


            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var modifiedAtProperty = item.Entity.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == "ModifiedAt");
                
                if (modifiedAtProperty != null)
                {
                    modifiedAtProperty.SetValue(item.Entity, DateTimeOffset.Now);
                }

            }

        }
        #endregion
    }
}
