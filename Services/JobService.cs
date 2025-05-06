using System.Net.Http;
using System.Text.Json;
using careerPortal.Models;

namespace careerPortal.Services
{
    public class JobService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JobService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }   

        public async Task<List<JobResult>> GetJobsAsync()
        {
            var client = _httpClientFactory.CreateClient("UsaJobs");

            var response = await client.GetAsync("api/search");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API call failed: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var jobsList = new List<JobResult>();
            try
            {
                var jsonDoc = JsonDocument.Parse(content);

                foreach (var job in jsonDoc.RootElement
                    .GetProperty("SearchResult")
                    .GetProperty("SearchResultItems")
                    .EnumerateArray())
                {
                    var descriptor = job.GetProperty("MatchedObjectDescriptor");

                    jobsList.Add(new JobResult
                    {
                        Title = descriptor.GetProperty("PositionTitle").GetString() ?? "N/A",
                        OrganizationName = descriptor.GetProperty("OrganizationName").GetString() ?? "N/A",
                        Location = descriptor.GetProperty("PositionLocationDisplay").GetString() ?? "N/A",
                        Url = descriptor.GetProperty("PositionURI").GetString() ?? "#"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to parse API response: " + ex.Message);
            }

            return jobsList;
        }
    }
}
