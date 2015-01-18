using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NuGetSearch.WinRT.Core
{
    public class IncrementalSource<T, K> : ObservableCollection<K>, ISupportIncrementalLoading
        where T : IPagedSource<K>, new()
    {
        private string Query { get; set; }
        private int VirtualCount { get; set; }
        private int CurrentPage { get; set; }
        private IPagedSource<K> Source { get; set; }

        public int PageSize { get; set; }

        public IncrementalSource(string query, int pageSize = 10)
        {
            this.Source = new T();
            this.VirtualCount = int.MaxValue;
            this.CurrentPage = 0;
            this.Query = query;
            this.PageSize = pageSize;
        }

        #region ISupportIncrementalLoading

        public bool HasMoreItems
        {
            get { return this.VirtualCount > this.CurrentPage * PageSize; }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher dispatcher = Window.Current.Dispatcher;

            return Task.Run<LoadMoreItemsResult>(
                async () =>
                {
                    IPagedResponse<K> result = await this.Source.GetPage(this.Query, ++this.CurrentPage, this.PageSize);

                    if (null != result)
                    {
                        this.VirtualCount = result.VirtualCount;

                        await dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                foreach (K item in result.Items)
                                    this.Add(item);
                            });

                        return new LoadMoreItemsResult() { Count = (uint)result.Items.Count() };
                    }
                    else
                    {
                        this.VirtualCount = 0;
                        return new LoadMoreItemsResult();
                    }
                }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        #endregion
    }
}