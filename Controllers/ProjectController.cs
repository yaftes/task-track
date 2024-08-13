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
                Start_Date = DateTime.Parse(model.Start_Date),
                End_Date = DateTime.Parse(model.End_Date),
                UserId =_userManager.GetUserId(User),
            };
            _dbContext.Project.Add(project);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        return View(model);
     }


     public IActionResult ProjDetails(){
        return View();
     }

     
}