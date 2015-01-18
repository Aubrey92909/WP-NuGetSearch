using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace NuGetSearch.WinRT.Core
{
    public class UIHelper
    {
        public static async Task ShowSystemTrayAsync(Color backgroundColor, Color foregroundColor,
            double opacity = 1, string text = "", bool isIndeterminate = false)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = backgroundColor;
            statusBar.ForegroundColor = foregroundColor;
            statusBar.BackgroundOpacity = opacity;

            statusBar.ProgressIndicator.Text = text;
            if (!isIndeterminate)
            {
                statusBar.ProgressIndicator.ProgressValue = 0;
            }
            await statusBar.ProgressIndicator.ShowAsync();
        }
    }
}
