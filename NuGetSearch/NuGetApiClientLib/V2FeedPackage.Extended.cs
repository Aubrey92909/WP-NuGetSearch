using System.Reflection;

namespace NuGetApiClientLib.NuGetService
{
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
    }
}
