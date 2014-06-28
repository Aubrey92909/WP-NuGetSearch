using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace NuGetSearch
{
    public static class Extensions
    {
        private static readonly Dictionary<string, object> Data = new Dictionary<string, object>();

        /// <summary>
        /// Navigates to the content specified by uniform resource identifier (URI).
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="source">The URI of the content to navigate to.</param>
        /// <param name="key">The key.</param>
        /// <param name="data">The data that you need to pass to the other page
        /// specified in URI.</param>
        public static void Navigate(this NavigationService navigationService, Uri source, string key, object data)
        {
            if (Data.ContainsKey(key))
            {
                Data.Remove(key);
            }

            Data.Add(key, data);
            navigationService.Navigate(source);
        }

        /// <summary>
        /// Gets the navigation data passed from the previous page.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="key">The key.</param>
        /// <param name="persistData">if set to <c>true</c> [persist data].</param>
        /// <returns>System.Object.</returns>
        public static object GetNavigationData(this NavigationContext context, string key, bool persistData = false)
        {
            var value = Data.ContainsKey(key) ? Data[key] : null;

            if (persistData == false)
            {
                context.Remove(key);
            }

            return value;
        }

        /// <summary>
        /// Removes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="key">The key.</param>
        public static void Remove(this NavigationContext context, string key)
        {
            if (Data.ContainsKey(key))
            {
                Data.Remove(key);
            }
        }

        /// <summary>
        /// Clears the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Clear(this NavigationContext context)
        {
            Data.Clear();
        }
    }
}
