using System.Collections.Generic;

namespace careerPortal.Models
{
    public class VisualizationViewModel
    {
        public Dictionary<string, int> StatusData { get; set; }
        public Dictionary<string, int> LocationData { get; set; }
        public Dictionary<string, int> ApiOrgData { get; set; }
    }
}
