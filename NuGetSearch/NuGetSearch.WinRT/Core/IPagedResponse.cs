using System.Collections.Generic;

namespace NuGetSearch.WinRT.Core
{
    public interface IPagedResponse<T>
    {
        IEnumerable<T> Items { get; }
        int VirtualCount { get; }
    }
}
