using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Xml.Dom;
using NuGetSearch.Core.Models;

namespace NuGetSearch.Core
{
    public class NuGetApiClient
    {
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

        private string BuildApiAddress(int pageIndex)
        {
            string template = string.Format(@"{0}/Search()?$filter=IsLatestVersion&$orderby=DownloadCount%20desc,Id&$skip={1}&$top={2}&searchTerm={3}&includePrerelease={4}", 
                ApiBaseAddress, 
                pageIndex, 
                PageSize, 
                SearchTerm, 
                IsIncludePreRelease.ToString().ToLower());

            return template;
        }

        public async Task<Response<List<Entry>>> GetDataAsync(int pageIndex)
        {
            try
            {
                var client = new HttpClient();
                var xml = await client.GetStringAsync(BuildApiAddress(pageIndex));
                var entries = ParseXmlStringToEntryList(xml);
                return new Response<List<Entry>>()
                {
                    IsSuccess = true,
                    Item = entries
                };
            }
            catch (Exception e)
            {
                return new Response<List<Entry>>()
                {
                    Message = e.Message
                };
            }
        }

        private List<Entry> ParseXmlStringToEntryList(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return new List<Entry>();
        }
    }
}
