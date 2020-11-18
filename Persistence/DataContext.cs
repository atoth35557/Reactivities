using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence {
    public class DataContext : IdentityDbContext<AppUser> {
        public DataContext (DbContextOptions options) : base (options) { }
        public DbSet<Value> Values {
            get;
            set;
        }
        public DbSet<Activity> Activities {
            get;
            set;
        }
        public DbSet<UserActivity> UserActivities {
            get;
            set;
        }
        public DbSet<Photo> Photos {
            get;
            set;
        }
        public DbSet<UserFollowing> Followings {
            get;
            set;
        }
        public DbSet<Comment> Comments {
            get;
            set;
        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.Entity<Value> ()
                .HasData (new Value {
                        Id = 1,
                            Name = "value one"
                    },
                    new Value {
                        Id = 2,
                            Name = "value two"
                    },
                    new Value {
                        Id = 3,
                            Name = "value three"
                    }
                );
            builder.Entity<UserActivity> (x => x.HasKey (ua => new {
                ua.AppUserId,
                    ua.ActivityId
            }));

            builder.Entity<UserActivity> ()
                .HasOne (a => a.AppUser)
                .WithMany (a => a.UserActivities)
                .HasForeignKey (u => u.AppUserId);

            builder.Entity<UserActivity> ()
                .HasOne (a => a.Activity)
                .WithMany (u => u.UserActivities)
                .HasForeignKey (a => a.ActivityId);

            builder.Entity<UserFollowing> (b => {
                b.HasKey (k => new {
                    k.ObserverId, k.TargetId
                });
                b.HasOne (f => f.Observer)
                    .WithMany (f => f.Followings)
                    .HasForeignKey (f => f.ObserverId)
                    .OnDelete (DeleteBehavior.Restrict);

                b.HasOne (f => f.Target)
                    .WithMany (f => f.Followers)
                    .HasForeignKey (f => f.TargetId)
                    .OnDelete (DeleteBehavior.Restrict);
            });
            builder.Entity<AppUser> (entity => entity.Property (m => m.NormalizedEmail).HasMaxLength (200));
            builder.Entity<AppUser> (entity => entity.Property (m => m.NormalizedUserName).HasMaxLength (200));
            builder.Entity<IdentityRole> (entity => entity.Property (m => m.NormalizedName).HasMaxLength (200));
            builder.Entity<IdentityUserLogin<string>> (entity => entity.Property (m => m.UserId).HasMaxLength (200));
            builder.Entity<IdentityUserRole<string>> (entity => entity.Property (m => m.UserId).HasMaxLength (200));
            builder.Entity<IdentityUserRole<string>> (entity => entity.Property (m => m.RoleId).HasMaxLength (200));
            builder.Entity<IdentityUserToken<string>> (entity => entity.Property (m => m.UserId).HasMaxLength (200));
            builder.Entity<IdentityUserClaim<string>> (entity => entity.Property (m => m.UserId).HasMaxLength (200));
            builder.Entity<IdentityRoleClaim<string>> (entity => entity.Property (m => m.RoleId).HasMaxLength (200));

        }
    }
}