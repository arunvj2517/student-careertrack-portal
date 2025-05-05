using careerPortal.Models;
using careerPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace careerPortal.Controllers
{
    public class DataController : Controller
    {
        private readonly IJobManagerService _jobManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public DataController(IJobManagerService jobManager, IHttpClientFactory httpClientFactory)
        {
            _jobManager = jobManager;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Visualizations()
        {
            var allJobs = _jobManager.GetAllJobs().Where(j => !j.IsFromApi).ToList();

            // Chart 1: Status breakdown
            var statusData = allJobs
                .GroupBy(j => string.IsNullOrWhiteSpace(j.Status) ? "Unknown" : j.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            // Chart 2: Location breakdown
            var locationData = allJobs
                .GroupBy(j => string.IsNullOrWhiteSpace(j.Location) ? "Unknown" : j.Location)
                .ToDictionary(g => g.Key, g => g.Count());

            // Chart 3: Organization breakdown from API
            var apiClient = _httpClientFactory.CreateClient("UsaJobs");
            var apiResponse = await apiClient.GetAsync("api/search");

            Dictionary<string, int> apiOrgData = new();

            if (apiResponse.IsSuccessStatusCode)
            {
                var content = await apiResponse.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<UsaJobsApiResponse>(content);

                apiOrgData = apiResult?.SearchResult?.SearchResultItems?
                    .Select(item => item.MatchedObjectDescriptor.OrganizationName)
                    .GroupBy(name => name)
                    .ToDictionary(g => g.Key, g => g.Count()) ?? new Dictionary<string, int>();
            }

            var model = new VisualizationViewModel
            {
                StatusData = statusData,
                LocationData = locationData,
                ApiOrgData = apiOrgData
            };

            return View(model);
        }
    }
}
