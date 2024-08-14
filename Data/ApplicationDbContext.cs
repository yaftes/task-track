using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.projects)
            .WithOne( u => u.ApplicationUser).HasForeignKey(u => u.UserId);

            modelBuilder.Entity<ApplicationUser>().HasOne(t => t.Task).WithOne( u => u.ApplicationUser).
            HasForeignKey<Task>( u => u.Assigned_to); 

            modelBuilder.Entity<Project>().HasMany(t => t.Tasks).WithOne(p => p.Project).HasForeignKey(p => p.ProjectId); 
        }

        public DbSet<Task> Task {get;set;}
        public DbSet<Project> Project {get;set;}
    


}