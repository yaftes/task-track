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

    public async Task<IActionResult> TaskCreate(){
        TaskModel taskmodel = new TaskModel(){
            AvailableUsers = _userManager.Users.ToList(),
        };
        return View(taskmodel);
    }
    [HttpPost]
    public async Task<IActionResult> TaskCreate(TaskModel model,int id){
        var curruser = await _userManager.GetUserAsync(User);

        if (ModelState.IsValid){
            Task task = new Task(){
                Title = model.Title,
                Description = model.Description,
                Created_At = DateTime.UtcNow,
                Start_Date = DateTime.UtcNow,
                End_Date = DateTime.UtcNow,
                ProjectId = id,
                Created_by = curruser.Id.ToString(),
            };
            _dbContext.Task.Add(task);
            _dbContext.SaveChanges();
        }
        return  RedirectToAction("ProjectDetail","Project", new {
            Id = id,
        });
    }

    [HttpGet]
    public IActionResult TaskDetail(int Id){
        Invitation invitation = _dbContext.Invitation.FirstOrDefault(i => i.Id == Id);
        Task task = _dbContext.Task.SingleOrDefault(t => t.Id == invitation.TaskId);

         TaskDetail taskDetail = new TaskDetail(){
            Task = task,
            Invitation = invitation,
            };
        return View(taskDetail);
     }
        [HttpPost]
        public async Task<IActionResult> SendInvitation(string selectedUser,string TaskId,string ProjectId){
        int taskid = Convert.ToInt16(TaskId);
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == taskid);
        var RecepantUser = await _userManager.FindByIdAsync(selectedUser);
        var SenderUser = await _userManager.GetUserAsync(User);
        if(task.Assigned_to != null){
            return View();
        }
        Invitation invitation = new Invitation(){
            ProjectId = Convert.ToInt16(ProjectId),
            TaskId = taskid,
            Sender_Id = SenderUser.Id,
            Recepant_Id = selectedUser,
            Receiver_Name = RecepantUser.FirstName + " " + RecepantUser.LastName,
            Sender_Name = SenderUser.FirstName + " " + SenderUser.LastName, 
            Title = task.Title,
            Description = task.Description,

       }; 
       _dbContext.Invitation.Add(invitation);
       _dbContext.SaveChanges();
    return RedirectToAction("ProjectDetail","Project",new {
        Id = ProjectId
    });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptInvitation(string TaskId,string ProjectId,string InvitationId,string Status ){
        var curruser = _userManager.GetUserId(User);
        int invId = Convert.ToInt16(InvitationId);
        var inv = _dbContext.Invitation.FirstOrDefault(i => i.Id == invId);
        inv.status = Status;
        _dbContext.Invitation.Update(inv);
        _dbContext.SaveChanges();   
        if(inv.status == "Accepted"){
        ProjectMember pm = new ProjectMember(){
            ProjId = Convert.ToInt16(ProjectId),
            UserId = curruser,
            Joined_At = DateTime.Now,
        };
        _dbContext.ProjectMember.Add(pm);
        _dbContext.SaveChanges();
        int taskId = Convert.ToInt16(TaskId);
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == taskId);
        task.Assigned_to = curruser;
        _dbContext.Task.Update(task);
        _dbContext.SaveChanges();
        return RedirectToAction("AllProjects","Project"); 
        }
    return RedirectToAction("AllProjects","Project");   
    }

   

}
