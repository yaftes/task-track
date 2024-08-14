using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Employee")]
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
       
        return View(model);
     }

     [HttpGet]
     public IActionResult AllProjects(){
        return View();
     }  
        


}