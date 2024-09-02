using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class ReportController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public ReportController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }

    // create
    // Detail
    [HttpPost]
    public async Task<IActionResult> CreateReport(string Title,string Detail,string TaskId,string ProjectId,List<IFormFile> reportfiles ){
        Report report = new Report(){
            Title = Title,
            Detail = Detail,
            TaskId = Convert.ToInt16(TaskId),
            ProjectId = Convert.ToInt16(ProjectId)  
        };
        _dbContext.Report.Add(report);
        _dbContext.SaveChanges();

        foreach(var reports in reportfiles){
            ReportFile reportfile = new ReportFile(){
                ContentType = reports.ContentType,
                FileName = reports.FileName,
                ReportId = report.Id,
            };
            using(var ms  = new MemoryStream()){
                await reports.CopyToAsync(ms);
                reportfile.Data = ms.ToArray();
            }
            _dbContext.ReportFile.Add(reportfile);
        }
        _dbContext.SaveChanges();

        return RedirectToAction("TaskDetail","Task",new {
            Id = TaskId
        });
    }

    public async Task<IActionResult> AllReports(int Id){
        // list of reports
        var reports = _dbContext.Report.Where(r => r.ProjectId == Id).ToList();
        var reportfiles = new List<ReportFile>();
        foreach(var rep in reports){
            reportfiles.Add(_dbContext.ReportFile.FirstOrDefault(r => r.ReportId == rep.Id));
        }
        ReportDetail reportDetail = new ReportDetail(){
            Reports = reports,
            ReportFiles = reportfiles
        };
        return View(reportDetail);
    }

    public async Task<IActionResult> ReportDetail(int Id){
        var report = _dbContext.Report.FirstOrDefault(r => r.Id == Id);
        var reportfiles = _dbContext.ReportFile.Where(r => r.ReportId == report.Id).ToList();
        ReportDetail reportDetail = new ReportDetail(){
            Report = report,
            ReportFiles = reportfiles
        };
        return View(reportDetail);

    }

    public async Task<IActionResult> GetReportFile(int id){
         var file = _dbContext.ReportFile.FirstOrDefault(tf => tf.Id == id);
         if(file != null){
            return File(file.Data,file.ContentType,file.FileName);
         }
         return NotFound();
     }

    
    
}