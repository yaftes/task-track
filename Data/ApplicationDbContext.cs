using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Task> Task {get;set;}
        public DbSet<Project> Project {get;set;}
        public DbSet<ProjectMember> ProjectMember {get;set;}
        public DbSet<Skill> Skill {get;set;}
        public DbSet<UserSkill> UserSkill {get;set;}
        public DbSet<Message> Message {get;set;}  
        public DbSet<SubTask> SubTask {get;set;} 
        public DbSet<Invitation> Invitation {get;set;}
        public DbSet<TaskWeight> TaskWeight  {get;set;}
        public DbSet<SubTaskWeight> SubTaskWeight {get;set;}
        public DbSet<SubTaskStatus> SubTaskStatus {get;set;}
        public DbSet<TaskStatus> TaskStatus {get;set;}
        public DbSet<ProjectStatus> ProjectStatus {get;set;}
        public DbSet<ProjectFile> ProjectFile {get;set;}
        public DbSet<TaskFile> TaskFile {get;set;}
        public DbSet<SubTaskFile> SubTaskFile {get;set;}


}