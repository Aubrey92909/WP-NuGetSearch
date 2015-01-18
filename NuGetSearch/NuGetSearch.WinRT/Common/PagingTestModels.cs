using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGetSearch.WinRT.Core;

namespace NuGetSearch.WinRT.Common
{
    public class RandomNumbers : IPagedSource<int>
    {
        public async Task<IPagedResponse<int>> GetPage(string query, int pageIndex, int pageSize)
        {
            Debug.WriteLine("pageIndex: {0}, pageSize: {1}", pageIndex, pageSize);

            // silumate out of items
            if (pageIndex <= 10)
            {
                var numbers = Enumerable.Range(new Random().Next(0, 500), 10);
                return new RandomNumberResponse(numbers, int.MaxValue);
            }
            return null;
        }
    }

    public class RandomNumberResponse : IPagedResponse<int>
    {
        public RandomNumberResponse(IEnumerable<int> items, int virtualCount)
        {
            this.Items = items;
            this.VirtualCount = virtualCount;
        }

        public int VirtualCount { get; private set; }
        public IEnumerable<int> Items { get; private set; }
    }
}
