using System;

namespace careerPortal.Models
{
    public class UsaJob
    {
        public string PositionId { get; set; } = Guid.NewGuid().ToString();

        public string PositionTitle { get; set; }

        public string OrganizationName { get; set; }

        public string Location { get; set; }

        public string SalaryRange { get; set; }

        public string PositionUri { get; set; }

        public DateTime PublicationStartDate { get; set; }

        public bool IsFromApi { get; set; }

        public string? Status { get; set; }

        public DateTime DateSaved { get; set; }
    }
}
