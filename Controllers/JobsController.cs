using careerPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace careerPortal.Controllers
{
    public class JobsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JobsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string keyword)
        {
            var client = _httpClientFactory.CreateClient("UsaJobs");

            var url = "api/search";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                url += $"?Keyword={Uri.EscapeDataString(keyword)}";
            }

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load job data from API.";
                return View(new List<UsaJob>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<UsaJobsApiResponse>(json);

            var jobs = data?.SearchResult?.SearchResultItems?
                .Select(item => new UsaJob
                {
                    PositionId = item.MatchedObjectDescriptor.PositionID,
                    PositionTitle = item.MatchedObjectDescriptor.PositionTitle,
                    OrganizationName = item.MatchedObjectDescriptor.OrganizationName,
                    Location = item.MatchedObjectDescriptor.PositionLocationDisplay,
                    PositionUri = item.MatchedObjectDescriptor.PositionURI,
                    SalaryRange = item.MatchedObjectDescriptor.PositionRemuneration?.FirstOrDefault() is { } pay
                        ? $"{pay.MinimumRange} - {pay.MaximumRange} {pay.Description}"
                        : "Not specified",
                    
                    IsFromApi = true,
                    DateSaved = DateTime.Now
                })
                .ToList() ?? new List<UsaJob>();

            return View(jobs);
        }
    }
}
