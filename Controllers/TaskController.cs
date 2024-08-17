using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
// when the project Manager creates the task it will assign them 
public class TaskController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public TaskController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }

    public async Task<IActionResult> TaskAssign(){
        TaskModel taskmodel = new TaskModel(){
            AvailableUsers = _userManager.Users.ToList(),
        };
        return View(taskmodel);
    }

    [HttpPost]
    public async Task<IActionResult> TaskAssign(TaskModel model,int id,string selecteduser){

        if (ModelState.IsValid){
            Task task = new Task(){
                Title = model.Title,
                Description = model.Description,
                Created_At = DateTime.UtcNow,
                Start_Date = DateTime.UtcNow,
                End_Date = DateTime.UtcNow,
                ProjectId = id,
                Assigned_to = selecteduser,
            };
            _dbContext.Task.Add(task);
            _dbContext.SaveChanges();
            ProjectMember projectMember = new ProjectMember(){
                ProjId = id,
                UserId = selecteduser
            };
            _dbContext.ProjectMember.Add(projectMember);
            _dbContext.SaveChanges();
        }
        return  RedirectToAction("ProjectDetails","Project", new {
            id = id,
        });

    }

}