using careerPortal.Models;
using careerPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace careerPortal.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IJobManagerService _jobManager;

        public ApplicationsController(IJobManagerService jobManager)
        {
            _jobManager = jobManager;
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View(new UsaJob());
        }

        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsaJob job)
        {
            if (ModelState.IsValid)
            {
                job.IsFromApi = false;
                job.DateSaved = DateTime.Now;

                _jobManager.AddJob(job);
                return RedirectToAction(nameof(Read));
            }
            return View(job);
        }

        // GET: Applications/Edit/{positionId}
        public IActionResult Edit(string positionId)
        {
            var job = _jobManager.GetJobById(positionId);
            if (job == null || job.IsFromApi)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Applications/Edit/{positionId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string positionId, UsaJob job)
        {
            if (positionId != job.PositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _jobManager.UpdateJob(job);
                return RedirectToAction(nameof(Read));
            }
            return View(job);
        }

        // GET: Applications/Delete/{positionId}
        public IActionResult Delete(string positionId)
        {
            var job = _jobManager.GetJobById(positionId);
            if (job == null || job.IsFromApi)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Applications/Delete/{positionId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string positionId)
        {
            _jobManager.DeleteJob(positionId);
            return RedirectToAction(nameof(Read));
        }

        // GET: Applications/Read
        public IActionResult Read()
        {
            var savedJobs = _jobManager.GetAllJobs()
                .Where(j => !j.IsFromApi)
                .ToList();

            return View(savedJobs);
        }
    }
}
