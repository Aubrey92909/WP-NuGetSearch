using System.Threading.Tasks;

namespace NuGetSearch.WinRT.Core
{
    public interface IPagedSource<T>
    {
        Task<IPagedResponse<T>> GetPage(string query, int pageIndex, int pageSize);
    }
}
