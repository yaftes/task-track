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
     public async Task<IActionResult> ProjEdit(){
         return View();
     }

     public async Task<IActionResult> ProjDelete(){
         return View();
     }
     
     [HttpGet]
     public async Task<IActionResult> AllProjects(){

      var curruser = await _userManager.GetUserAsync(User);
      // select project id from project member that matches with current user
      // then using that project id retrieve all project that matches
      var projectIds = await _dbContext.ProjectMember
        .Where(pm => pm.UserId == curruser.Id)
        .Select(pm => pm.ProjId)
        .ToListAsync();

// Retrieve projects based on the retrieved IDs
        var projects = await _dbContext.Project
            .Where(p => projectIds.Contains(p.Id))
            .ToListAsync();
        return View(projects);
     } 

     [HttpGet]
     // used for displaying project Details
     public async Task<IActionResult> ProjectDetails(int? id){

        var project = _dbContext.Project.FirstOrDefault(p=>p.Id == id);
  
        return View(project);
     } 

        
}

