using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NuGetApiClientLib.NuGetService
{
    public class DependencyInfo
    {
        public string Id { get; set; }
        public string Version { get; set; }
    }

    public class V2FeedPackageEx : V2FeedPackage
    {
        public string DisplayTitle
        {
            get
            {
                return string.IsNullOrEmpty(Title) ? Id : Title;
            }
        }

        public string DisplaySummary
        {
            get
            {
                return string.IsNullOrEmpty(Summary) ? Description : Summary;
            }
        }

        public List<DependencyInfo> DependenciesList
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Dependencies))
                {
                    var arr = Dependencies.Split(new[] { ":|" }, StringSplitOptions.RemoveEmptyEntries);
                    var dep = arr.Select(p => new DependencyInfo
                    {
                        Id = p.Split(':')[0],
                        Version = p.Split(':')[1]
                    });

                    return dep.ToList();
                }
                return new List<DependencyInfo>();
            }
        }

        public V2FeedPackageEx(V2FeedPackage pkgEntity = null)
        {
            if (pkgEntity != null)
            {
                var orgFields = pkgEntity.GetType().GetRuntimeFields(); //(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                foreach (var fi in orgFields)
                {
                    fi.SetValue(this, fi.GetValue(pkgEntity));
                }
            }
        }

        public override string ToString()
        {
            return string.Format(@"{0}{5} Id:{1}{5} Version:{2}{5} Description:{3}{5} Gallery Url:{4}{5}",
                DisplayTitle, Id, NormalizedVersion, DisplaySummary, GalleryDetailsUrl, Environment.NewLine);
        }
    }
}
