using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
     public IActionResult ProjectCreate(){
        
        return View();
     }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProjectCreate(TaskModel model,List<IFormFile> files)
    {
        var curruser = await _userManager.GetUserAsync(User);
        if (curruser == null){
            return View();
        }
        if(ModelState.IsValid){
             Project project = new Project{
                 Title = model.Title,
                 Description = model.Description,
                 Created_At = DateTime.Now,
                 Start_Date = DateTime.Now, 
                 End_Date = DateTime.Now,
                 Update_Date = DateTime.Now,
                 Created_By = curruser.Id, 
             };

            _dbContext.Project.Add(project);
            _dbContext.SaveChanges();

            ProjectMember pm = new  ProjectMember(){
                    UserId = curruser.Id,
                    ProjId = project.Id,
                    Joined_At = DateTime.Now  };
             _dbContext.ProjectMember.Add(pm);
             _dbContext.SaveChanges();
            
            if(files != null && files.Count() != 0){
                foreach(var file in files){
                    ProjectFile projectfile = new ProjectFile(){
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        ProjectId = project.Id,
                    };
                    using (var memorystream = new MemoryStream()){
                        await file.CopyToAsync(memorystream);
                        projectfile.Data = memorystream.ToArray();
                    }
                    _dbContext.ProjectFile.Add(projectfile);
                    _dbContext.SaveChanges();
                }
            }
        return RedirectToAction("AllProjects", "Project");}
        return View(model);
        }
      
      [HttpGet]
 
     public async Task<IActionResult> ProjectEdit(int id){
        var project = _dbContext.Project.FirstOrDefault(p=>p.Id == id);
        ProjectModel model = new ProjectModel(){
            ProjectId = project.Id,
            Title = project.Title,
            Description = project.Description,
            Start_Date = project.Start_Date.ToString("MM/dd/yyyy HH:mm"),
            End_Date = project.End_Date.ToString("MM/dd/yyyy HH:mm"),

        };
         return View(model);
     }
     [HttpPost]
      public async Task<IActionResult> ProjectEdit(ProjectModel model){
        var currpro = _dbContext.Project.FirstOrDefault(p => p.Id == model.ProjectId);
        currpro.Title = model.Title;    
        currpro.Description = model.Description;
        currpro.Start_Date = DateTime.Parse(model.Start_Date);
        currpro.End_Date = DateTime.Parse(model.End_Date);
        _dbContext.Project.Update(currpro);
        await _dbContext.SaveChangesAsync();
         return RedirectToAction("ProjectDetails","Project",new{
            Id = model.ProjectId,
         });
     }
     public async Task<IActionResult> ProjectDelete(int id){

        var project = _dbContext.Project.FirstOrDefault(p => p.Id == id);
        if(project != null){
            _dbContext.Project.Remove(project);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("AllProjects","Project");
        }
       
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
      var inv = _dbContext.Invitation.Where(i => i.Recepant_Id == curruser.Id).ToList();
        ProjectDetail projectDetails = new ProjectDetail(){
            Projects = _projects,
            Invitations = inv,
        };
        return View(projectDetails);
     } 

     [HttpGet]
     public async Task<IActionResult> ProjectDetail(int? id){
        var curruser = await _userManager.GetUserAsync(User); 
        var tasks =  _dbContext.Task.Where(t => t.ProjectId == id).ToList();
        var _project = _dbContext.Project.FirstOrDefault(p=>p.Id == id);
        var projfiles = _dbContext.ProjectFile.Where(pf => pf.ProjectId == _project.Id).ToList();
        var usersInProject = await _dbContext.ProjectMember
        .Where(pm => pm.ProjId == id)
        .Select(pm => pm.UserId)
        .Distinct()
        .ToListAsync();
        var users = await _dbContext.Users
        .Where(u => usersInProject.Contains(u.Id))
        .ToListAsync();
        var _AvailableUsers = _userManager.Users.ToList();
        var taskweight = new List<TaskWeight>();
        foreach(var ts in tasks){
            var tw = _dbContext.TaskWeight.FirstOrDefault(tw => tw.TaskId == ts.Id);
            if(tw != null){
             tw.Weight = Math.Round(tw.Weight, 2);
             taskweight.Add(tw);
            } 
        }

        //
        double totalprogress = 0.0;
        foreach(var ts in tasks){
            var tw = _dbContext.TaskWeight.FirstOrDefault(tw => tw.TaskId == ts.Id);    
            totalprogress += ts.Progress * tw.Weight / 100;
        }
        _project.Progress = totalprogress;
        _dbContext.Project.Update(_project);
        _dbContext.SaveChanges();
        _project.Progress = Math.Round(_project.Progress, 2);
        


        var message = _dbContext.Message.Where(m => m.ProjectId == id).ToList();
            ProjectDetail projectDetails = new ProjectDetail(){
            Project = _project,
            Projectmembers = users,
            Tasks = tasks,
            Messages = message,
            AvailableUsers = _AvailableUsers,
            TaskWeight = taskweight,
            ProjectFiles = projfiles,    
        };
        return View(projectDetails);
     } 

     [HttpPost]
     [ValidateAntiForgeryToken]
     public async  Task<IActionResult> ProjectDetail(int id,string message){
        var curruser = await _userManager.GetUserAsync(User);
        Message msg = new Message(){
            Text = message,
            ProjectId = id,
            CreatorId = curruser.Id,
            FullName = curruser.FirstName + " " + curruser.LastName,
            Created_At = DateTime.Now,
        };

        _dbContext.Message.Add(msg);
        _dbContext.SaveChanges();
        return RedirectToAction("ProjectDetail","Project",new {
            Id = id
        });
     }
     public IActionResult DashBoard(){
        return View();
     }

     public async Task<IActionResult> GetProjectFile(int id){
         var file = _dbContext.ProjectFile.FirstOrDefault(pf => pf.Id == id);
         if(file != null){
            return File(file.Data,file.ContentType,file.FileName);
         }
         return NotFound();
     }
      
}

