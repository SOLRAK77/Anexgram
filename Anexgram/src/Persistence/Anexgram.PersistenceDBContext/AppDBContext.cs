using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Anexgram.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Anexgram.Model.Domain.DBHelper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Anexgram.PersistenceDBContext.Config;

namespace Anexgram.PersistenceDBContext
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddMyFilters(ref modelBuilder);

            new ApplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
            new LikesPerPhotoConfig(modelBuilder.Entity<LikesPerPhoto>());
            new CommentsPerPhotoConfig(modelBuilder.Entity<CommentsPerPhoto>());
            new PhotoConfig(modelBuilder.Entity<Photo>());

        }

        public DbSet<CommentsPerPhoto> CommentsPerPhoto { get; set; }
        public DbSet<LikesPerPhoto> LikesPerPhoto { get; set; }
        public DbSet<Photo> Photos { get; set; }

        /**/
        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Propiedad que audita las entidades
        /// </summary>
        private void MakeAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity
                    && (
                    x.State == EntityState.Added
                    || x.State == EntityState.Modified
                )
            );

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditEntity;

                if (entity != null)
                {
                    var date = DateTime.UtcNow;
                    string userId = null;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).Deleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;
                }
            }
        }

        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            #region SoftDeleted
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<CommentsPerPhoto>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<LikesPerPhoto>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<Photo>().HasQueryFilter(x => !x.Deleted);
            #endregion
        }
    }
}
