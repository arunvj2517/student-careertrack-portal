namespace careerPortal.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime DateApplied { get; set; }
        public string Status { get; set; } // "Applied", "Interview", "Offer", "Rejected"
        public string? Notes { get; set; }
    }
}