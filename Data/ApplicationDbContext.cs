
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().HasMany(p => p.Projects).WithOne(a => a.ApplicationUser).
        HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Project>().HasMany(t => t.Tasks).WithOne(p => p.Project).
        HasForeignKey(p => p.ProjectId);

        modelBuilder.Entity<ApplicationUser>().HasMany(t => t.Tasks).WithOne(a => a.ApplicationUser)
        .HasForeignKey(t => t.UserId);


        
    }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<Project> Project {get;set;}
    public DbSet<Task> Task {get;set;}

}