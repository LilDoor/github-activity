using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_activity.Models
{
    internal class ActivityInfo
    {
        public string Type { get; set; } = string.Empty;
        public string Count { get; set; }
        public List<string> RepoName { get; set; } = new();
    }
}
