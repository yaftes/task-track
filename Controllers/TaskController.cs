

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

    
    public async Task<IActionResult> TaskAsign(){
        TaskModel taskModel = new TaskModel(){
           AvailableUsers = _userManager.Users.ToList(),
        };
        return View(taskModel);
    }

    [HttpPost]
    public async Task<IActionResult> TaskAsign(TaskModel model){

        if (ModelState.IsValid){
            Task task = new Task(){
                Title = model.Title,
                Description = model.Description,
                Created_At = DateTime.UtcNow,
                Start_Date = DateTime.Parse(model.Start_Date),
                End_Date = DateTime.Parse(model.End_Date),
                // SeletedUser Assigned
                // Project Id Assigned  
            };
            _dbContext.Task.Add(task);
            _dbContext.SaveChanges();
        }
        return View(model);

    }

}