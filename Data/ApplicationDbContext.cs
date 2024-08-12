
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().HasMany(p => p.Projects).WithOne(a => a.ApplicationUser).
        HasForeignKey(a => a.UserId);

        
    }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<Project> Project {get;set;}

}