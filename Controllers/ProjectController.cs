using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Authorize(Roles = "ProjMan")]
public class ProjectController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public ProjectController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }
   
     
     [HttpGet]
     public async Task<IActionResult> ProjCreate(){
        return View();
     }
     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> ProjCreate(ProjectModel model){

        var curruser = await _userManager.GetUserAsync(User);
      
         if (ModelState.IsValid){ 
            bool check = _dbContext.Project.Any(p => p.Title == model.Title);
            if(check){
                ModelState.AddModelError(string.Empty, "Already Exits");
                return View();
            }
            Project project = new Project(){
                Title = model.Title,
                Description = model.Description,
                Created_At = DateTime.UtcNow,
                Start_Date = DateTime.Parse(model.Start_Date),
                End_Date = DateTime.Parse(model.End_Date),
                Created_By = curruser.FirstName + " " + curruser.LastName,
            };
            // add the current user to project Member with project id
            _dbContext.Project.Add(project);
            _dbContext.SaveChanges();
            ProjectMember projectMember = new ProjectMember(){
                UserId = curruser.Id,
                ProjId = project.Id,
            };
            _dbContext.ProjectMember.Add(projectMember);
            _dbContext.SaveChanges();

            return RedirectToAction("AllProjects","Project");

         }
         
        return View(model);
     }

      [HttpGet]
     public async Task<IActionResult> ProjEdit(int id){
        var project = _dbContext.Project.FirstOrDefault(p=>p.Id == id);
        
         return View(project);
     }

     public async Task<IActionResult> ProjDelete(){
         return View();
     }
     
     [HttpGet]
     public async Task<IActionResult> AllProjects(){
      var curruser = await _userManager.GetUserAsync(User); 
      var projectIds = await _dbContext.ProjectMember
        .Where(pm => pm.UserId == curruser.Id)
        .Select(pm => pm.ProjId)
        .ToListAsync();
        var _projects = await _dbContext.Project
        .Where(p => projectIds.Contains(p.Id))
        .ToListAsync();
        ProjectDetail projectDetails = new ProjectDetail(){
            Projects = _projects,
        };
    
        return View(projectDetails);
     } 

     [HttpGet]
     // used for displaying project Details
     public async Task<IActionResult> ProjectDetails(int? id){
        var _project = _dbContext.Project.FirstOrDefault(p=>p.Id == id);
        var usersInProject = await _dbContext.ProjectMember
        .Where(pm => pm.ProjId == id)
        .Select(pm => pm.UserId)
        .Distinct()
        .ToListAsync();
        var users = await _dbContext.Users
        .Where(u => usersInProject.Contains(u.Id))
        .ToListAsync();
        ProjectDetail projectDetails = new ProjectDetail(){
            project = _project,
            Team_members = users,
        };
            
        return View(projectDetails);
     } 

     public IActionResult DashBoard(){
        return View();
     }

        
}

