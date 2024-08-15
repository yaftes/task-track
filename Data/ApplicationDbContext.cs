using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Task> Task {get;set;}
        public DbSet<Project> Project {get;set;}
        public DbSet<ProjectMember> ProjectMember {get;set;}
        public DbSet<Skill> Skill {get;set;}
        public DbSet<UserSkill> UserSkill {get;set;}

}