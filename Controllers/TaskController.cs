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
    public async Task<IActionResult> TaskCreate(TaskModel model,int id,List<IFormFile> taskfiles){
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
            TaskWeight taskWeight = new TaskWeight(){
                TaskId = task.Id,
                Point = model.Point,
            };
            _dbContext.TaskWeight.Add(taskWeight);
            _dbContext.SaveChanges(); 

        
            TaskStatus taskStatus = new TaskStatus(){
                TaskId = task.Id,
            };
            _dbContext.TaskStatus.Add(taskStatus);
            _dbContext.SaveChanges();

            var tasks = _dbContext.Task.Where(t => t.ProjectId == id).ToList();
            double totalpoint = 0.0;
            foreach (var ts in tasks){
                var tw = _dbContext.TaskWeight.FirstOrDefault(tw => tw.TaskId == ts.Id);
                totalpoint += tw.Point;
            };

            foreach(var ts in tasks){
                 var tw = _dbContext.TaskWeight.FirstOrDefault(tw => tw.TaskId == ts.Id);
                 tw.Weight = tw.Point / totalpoint * 100;
                 _dbContext.TaskWeight.Update(tw);
            }
            _dbContext.SaveChanges();
            var project = _dbContext.Project.FirstOrDefault(p => p.Id == task.ProjectId);
            double totalprogress = 0.0;
            foreach(var ts in tasks){
                var tw = _dbContext.TaskWeight.FirstOrDefault(tw => tw.TaskId == ts.Id);    
                totalprogress += ts.Progress * tw.Weight / 100;
            }
            project.Progress = totalprogress;
            _dbContext.Project.Update(project);
            _dbContext.SaveChanges();   
            
          
            if(taskfiles != null && taskfiles.Count() != 0){
                foreach(var file in taskfiles){
                    TaskFile taskfile = new TaskFile(){
                        TaskId = task.Id,
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                    };
                    using(var memorystream2 = new MemoryStream()){
                          await file.CopyToAsync(memorystream2);
                          taskfile.Data = memorystream2.ToArray();
                    }
                    _dbContext.TaskFile.Add(taskfile);
                    _dbContext.SaveChanges();
                }
            
            }
        }
        return  RedirectToAction("ProjectDetail","Project", new {
            Id = id,
        });
    }

    public IActionResult TaskDetail(int Id){
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == Id);
        var subtasks = _dbContext.SubTask.Where(st => st.TaskId == Id).ToList();
        var subtaskweight = new List<SubTaskWeight>();
        foreach(var st in subtasks){
            var stw = _dbContext.SubTaskWeight.FirstOrDefault(stw => stw.SubTaskId == st.Id);
            if(stw != null){
                stw.Weight = Math.Round(stw.Weight,2);
                subtaskweight.Add(stw);
            }
        }
        
        var taskfiles = _dbContext.TaskFile.Where(tf => tf.TaskId == task.Id).ToList();
        task.Progress = Math.Round(task.Progress,2);
        TaskDetail taskDetail = new TaskDetail(){
            Task = task,
            SubTasks = subtasks,
            SubTaskWeights = subtaskweight,
            TaskFiles = taskfiles,
        };
        return View(taskDetail);
    }

    public IActionResult StatusUpdate(int Id){
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == Id);
        var taskWeight = _dbContext.TaskWeight.FirstOrDefault(tw => tw.TaskId == task.Id);
        var taskstatus = _dbContext.TaskStatus.FirstOrDefault(ts => ts.TaskId == task.Id);
        // to say one task is completed the task progress should be 100 %
        if(task.Progress >= 100) {
         taskstatus.Status = "Completed";
        _dbContext.TaskStatus.Update(taskstatus);
        _dbContext.SaveChanges();
        }
        var project = _dbContext.Project.FirstOrDefault(p => p.Id == task.ProjectId);
        return RedirectToAction("ProjectDetail","Project",new {
            Id = project.Id
        }) ;
    }

    [HttpGet]
    public IActionResult InvitedTaskDetail(int Id){
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
        var check = IsInvitationUnique(selectedUser,TaskId,ProjectId);
        if(check is true){
        return RedirectToAction("ProjectDetail","Project",new {
        Id = Convert.ToInt16(ProjectId)
            });
        }
        var taskid = Convert.ToInt16(TaskId);
        var projectid = Convert.ToInt16(ProjectId);
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == taskid);
        var RecepantUser = await _userManager.FindByIdAsync(selectedUser);
        var SenderUser = await _userManager.GetUserAsync(User);
        if(task.Assigned_to != null){
            return View();
        }
        Invitation invitation = new Invitation(){
            ProjectId = projectid,
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
        var curruser = await _userManager.GetUserAsync(User);
        int invId = Convert.ToInt16(InvitationId);
        var inv = _dbContext.Invitation.FirstOrDefault(i => i.Id == invId);
        inv.status = Status;
        _dbContext.Invitation.Update(inv);
        _dbContext.SaveChanges();   
        if(inv.status == "Accepted"){
        ProjectMember pm = new ProjectMember(){
            ProjId = Convert.ToInt16(ProjectId),
            UserId = curruser.Id,
            Joined_At = DateTime.Now,
        };
        _dbContext.ProjectMember.Add(pm);
        _dbContext.SaveChanges();
        int taskId = Convert.ToInt16(TaskId);
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == taskId);
        task.Assigned_to = curruser.Id;
        task.Name = curruser.FirstName + " " + curruser.LastName;
        _dbContext.Task.Update(task);
        _dbContext.SaveChanges();
        return RedirectToAction("AllProjects","Project"); 
        }
    return RedirectToAction("AllProjects","Project");   
    }
    public bool IsInvitationUnique(string selectedUser,string TaskId,string ProjectId){
        return  _dbContext.Invitation.Any(i => i.Recepant_Id == selectedUser && i.TaskId == Convert.ToInt16(TaskId) && i.ProjectId == Convert.ToInt16(ProjectId));
    }

    // 
     public async Task<IActionResult> GetTaskFile(int id){
         var file = _dbContext.TaskFile.FirstOrDefault(tf => tf.Id == id);
         if(file != null){
            return File(file.Data,file.ContentType,file.FileName);
         }
         return NotFound();
     }
}
