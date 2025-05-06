using careerPortal.Models;
using System.Collections.Generic;

namespace careerPortal.Services
{
    public interface IJobManagerService
    {
        void AddJob(UsaJob job);
        void UpdateJob(UsaJob job);
        void DeleteJob(string positionId);
        List<UsaJob> GetAllJobs();
        UsaJob? GetJobById(string positionId);
    }
}