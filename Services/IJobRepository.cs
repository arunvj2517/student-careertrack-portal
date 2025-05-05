using careerPortal.Models;

namespace careerPortal.Services
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        UsaJob? GetJobById(string positionId);
        void AddJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(int id);
        object GetJobStatistics();
        object GetChartData();
    }
}