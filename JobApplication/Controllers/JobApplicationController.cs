using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobApplication.Areas.Identity.Data;
using JobApplication.Data;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace JobApplication.Controllers
{
    [Authorize]
    public class JobApplicationController : Controller
    {
        private readonly JobApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobApplicationController(JobApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        //create jobApplication form
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.JobApplication job)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            job.UserId = userId;
            job.Status = "Pending";
            job.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View("Index", job);
            }


            _db.JobApplications.Add(job);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var job = _db.JobApplications.Find(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.JobApplication job)
        {
            
            if (!ModelState.IsValid)
            {
                return View("Index", job);
            }


            _db.JobApplications.Update(job);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var job = await _db.JobApplications.FirstOrDefaultAsync(j => j.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);

        }


        [HttpPost, ActionName("DeleteJob")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteJob(int? id)
        {
            var jobToDelete = await _db.JobApplications.FindAsync(id);
            if (jobToDelete == null)
            {
                return NotFound();
            }

            _db.JobApplications.Remove(jobToDelete);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        //public async Task<IActionResult> AddInterview(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var interview = await _db.JobApplications.FindAsync(id);

        //    if (interview == null)
        //    {
        //        interview = new Interview();
        //    }

        //    return View(interview);
        //}

    }
}
