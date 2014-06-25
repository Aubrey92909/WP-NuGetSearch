using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using NuGetApiClientLib.NuGetService;

namespace NuGetApiClientLib
{
    public class NuGetApiClient
    {
        private V2FeedContext context;

        #region Properties

        public string ApiBaseAddress { get; set; }

        public string SearchTerm { get; set; }

        public int PageSize { get; set; }

        public bool IsIncludePreRelease { get; set; }

        #endregion

        public NuGetApiClient(string apiBaseAddress)
        {
            ApiBaseAddress = apiBaseAddress;
            SearchTerm = string.Empty;
            context = new V2FeedContext(new Uri(ApiBaseAddress));
        }

        public NuGetApiClient SetPageSize(int pageSize)
        {
            PageSize = pageSize;
            return this;
        }

        public NuGetApiClient SearchFor(string keyword)
        {
            SearchTerm = keyword;
            return this;
        }

        public NuGetApiClient IncludePreRelease(bool includePreRelease)
        {
            IsIncludePreRelease = includePreRelease;
            return this;
        }


        public async Task<Response<IEnumerable<V2FeedPackage>>> GetDataAsync(string searchTerm, int pageIndex, bool includePreRelease = false)
        {
            try
            {
                IDictionary<string, object> queryOptions = new Dictionary<string, object> {
                    { "filter", "IsLatestVersion" },
                    { "searchTerm", "'" + UrlEncodeOdataParameter(searchTerm) + "'" },
                    { "includePrerelease", includePreRelease.ToString().ToLower() }
                };

                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }

                int startRow = (pageIndex - 1) * PageSize;
                var nquery = context.CreateQuery<V2FeedPackage>("Search")
                                    .OrderByDescending(p => p.DownloadCount)
                                    .Skip(startRow)
                                    .Take(PageSize);

                var query = (DataServiceQuery<V2FeedPackage>)nquery;
                if (queryOptions.Any())
                {
                    query = queryOptions.Aggregate(query, (current, pair) => current.AddQueryOption(pair.Key, pair.Value));
                }

                var taskFactory = new TaskFactory<IEnumerable<V2FeedPackage>>();
                var result = await taskFactory.FromAsync(query.BeginExecute(null, null), query.EndExecute);
                var packages = result.ToList();

                return new Response<IEnumerable<V2FeedPackage>>()
                {
                    IsSuccess = true,
                    Item = packages
                };
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<V2FeedPackage>>()
                {
                    Message = e.Message
                };
            }
        }

        private static string UrlEncodeOdataParameter(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                // OData requires that a single quote MUST be escaped as 2 single quotes.
                // In .NET 4.5, Uri.EscapeDataString() escapes single quote as %27. Thus we must replace %27 with 2 single quotes.
                // In .NET 4.0, Uri.EscapeDataString() doesn't escape single quote. Thus we must replace it with 2 single quotes.
                return Uri.EscapeDataString(value).Replace("'", "''").Replace("%27", "''");
            }

            return value;
        }
    }
}
