using JobApplication.Areas.Identity.Data;
using JobApplication.Data;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly JobApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;



        public HomeController(ILogger<HomeController> logger, JobApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            //var user = _userManager.FindByEmailAsync(ClaimTypes.Email);

            var userId = _userManager.GetUserId(HttpContext.User);
            
            if (userId == null)
            {
                return NotFound();
            }

            var jobApplications = await _db.JobApplications.Where(j => j.UserId == userId).ToListAsync();
            jobApplications = jobApplications.OrderByDescending(j => j.Date).ToList();


            return View(jobApplications);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
