using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGetSearch.Core.Models
{
    public class Entry
    {
        public Guid InternalUID { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime Updated { get; set; }
        public Author Author { get; set; }
        public string Version { get; set; }
        public string NormalizedVersion { get; set; }
        public string Copyright { get; set; }
        public string Created { get; set; }
        public List<Dependency> Dependencies { get; set; }
        public string Description { get; set; }
        public int DownloadCount { get; set; }
        public string GalleryDetailsUrl { get; set; }
        public bool IsPrerelease { get; set; }
        public DateTime Published { get; set; }
        public string ProjectUrl { get; set; }
        public string ReportAbuseUrl { get; set; }
        public string ReleaseNotes { get; set; }
        public bool RequireLicenseAcceptance { get; set; }
        public string LicenseUrl { get; set; }
    }
}
