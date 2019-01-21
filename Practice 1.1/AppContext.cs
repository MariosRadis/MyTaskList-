namespace Practice_1._1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Practice_1._1.Models;

    public partial class AppContext : DbContext
    {
        public AppContext()
            : base("name=AppContext")
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
               .ToTable("Tasks")
               .HasKey(i => i.Id);

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(i => i.Id)
                .HasMany(i => i.Roles)
                .WithMany(i => i.Users) //Many to many relationship
                .Map(m =>
                {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID");
                    m.ToTable("UserRoles");
                });
        }
    }
}
