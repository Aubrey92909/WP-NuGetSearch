using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGetApiClientLib.NuGetService;

namespace NuGetApiClientLib
{
    public interface INuGetSearcher
    {
        Task<Response<IEnumerable<V2FeedPackage>>> SearchPackagesByTermAsync(string searchTerm, int pageIndex);
    }

    public class NuGetOrgSearcher : INuGetSearcher
    {
        public int PageSize { get; set; }

        public NuGetOrgSearcher()
        {
            PageSize = 30;
        }

        public async Task<Response<IEnumerable<V2FeedPackage>>> SearchPackagesByTermAsync(string searchTerm, int pageIndex)
        {
            var nugetClient = new NuGetApiClient("https://www.nuget.org/api/v2/");
            var response = await nugetClient.SetPageSize(PageSize).GetDataAsync(searchTerm, pageIndex);
            return response;
        }

        public async Task<Response<IEnumerable<V2FeedPackage>>> GetMostPopularPackagesAsync(int pageIndex)
        {
            var nugetClient = new NuGetApiClient("https://www.nuget.org/api/v2/");
            var response = await nugetClient.SetPageSize(PageSize).GetDataAsync(string.Empty, pageIndex);
            return response;
        }

        public async Task<Response<IEnumerable<V2FeedPackage>>> GetTopMicrosoftDotNetPackagesAsync(int pageIndex)
        {
            var nugetClient = new NuGetApiClient("https://www.nuget.org/api/v2/curated-feeds/microsoftdotnet");
            var response = await nugetClient.SetPageSize(PageSize).GetDataAsync(string.Empty, pageIndex);
            return response;
        }
    }
}
