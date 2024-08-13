using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        if (ModelState.IsValid){
            
            Project project = new Project(){
                Title = model.Title,
                Description = model.Description,    
                Created_At = DateTime.UtcNow,
                Update_Date = DateTime.UtcNow,
                UserId =_userManager.GetUserId(User),
            };
            _dbContext.Project.Add(project);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        return View(model);
     }

     
    //  [HttpGet]
    //  public IActionResult ProjEdit(int? id){
    //     var project = _dbContext.Project.FirstOrDefault(p => p.Id == id);
    //     if (project == null){
    //         return View("Error");
    //     }
    //     return View(project);
    //  }

    //  [HttpPost]
    //  [ValidateAntiForgeryToken]

    //  public IActionResult ProjEdit(ProjectModel model){
    //     var project = _dbContext.Project.FirstOrDefault(p => p.Id == model.Id);
    //     if(ModelState.IsValid){
    //         if (project == null){
    //         return View("Error");
    //             }
    //         project.Title = model.Title;
    //         project.Description = model.Description;    
    //         project.CreatedDate = DateTime.UtcNow;
            
    //     _dbContext.Project.Update(project);
    //     _dbContext.SaveChanges();

    //     }
    //     return View(model);

    //  }

    //  [HttpGet]
    //  public IActionResult ProjDelete(int? id){
    //     var project = _dbContext.Project.FirstOrDefault(p => p.Id == id);
    //     if (project == null){
    //         return View("Error");
    //     }
    //     return View(project);
    //  }

    //  [HttpPost]
    //  [ValidateAntiForgeryToken]

    //  public IActionResult ProjDelete(int id){
    //     var project = _dbContext.Project.FirstOrDefault(p => p.Id == id);
    //     if (project == null){
    //         return View("Error");
    //          }
            
    //     _dbContext.Project.Remove(project);
    //     _dbContext.SaveChanges();
    //         return View(project);
    //     }
    //  public IActionResult GetAllProj(){
    //     List<Project> projects = _dbContext.Project.Where(p => p.UserId == _userManager.GetUserId(User)).ToList();
    //     return View(projects);  
    //  }
    


}