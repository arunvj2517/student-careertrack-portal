using careerPortal.Models;
using System.Collections.Concurrent;
using System.Linq;

namespace careerPortal.Services
{
    public class JobManagerService : IJobManagerService
    {
        private readonly ConcurrentDictionary<string, UsaJob> _jobs = new();

        public void AddJob(UsaJob job)
        {
            if (string.IsNullOrEmpty(job.PositionId))
                job.PositionId = Guid.NewGuid().ToString();

            _jobs[job.PositionId] = job;
        }

        public void UpdateJob(UsaJob job)
        {
            if (_jobs.ContainsKey(job.PositionId))
                _jobs[job.PositionId] = job;
        }

        public void DeleteJob(string positionId)
        {
            _jobs.TryRemove(positionId, out _);
        }

        public List<UsaJob> GetAllJobs()
        {
            return _jobs.Values
                       .OrderByDescending(j => j.DateSaved)
                       .ToList();
        }

        public UsaJob? GetJobById(string positionId)
        {
            return _jobs.TryGetValue(positionId, out var job) ? job : null;
        }
    }
}