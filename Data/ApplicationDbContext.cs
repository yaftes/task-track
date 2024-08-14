
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        base.OnModelCreating(modelBuilder);
       
    }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<Project> Project {get;set;}
    public DbSet<Task> Task {get;set;}
    public DbSet<UserProject> UserProject {get;set;}

}