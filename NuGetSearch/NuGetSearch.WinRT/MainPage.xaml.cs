using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NuGetSearch.WinRT.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace NuGetSearch.WinRT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            UIHelper.ShowSystemTrayAsync(Color.FromArgb(255, 0, 114, 188), Colors.White, text: "NuGet Search (v3.0)");
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtonsOnBackPressed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //this.navigationHelper.OnNavigatedFrom(e);
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtonsOnBackPressed;
        }

        async private void HardwareButtonsOnBackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;

            if (!Frame.CanGoBack)
            {
                var dig = new MessageDialog("Are you sure you want to close this app?", "Confirm");
                var btnOk = new UICommand("Yes");
                dig.Commands.Add(btnOk);
                var btnCancel = new UICommand("No");
                dig.Commands.Add(btnCancel);

                var result = await dig.ShowAsync();
                if (null != result && result.Label == "Yes")
                {
                    Application.Current.Exit();
                }
            }
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private async void MenuReview_Click(object sender, RoutedEventArgs e)
        {
            await Tasks.OpenReviewAsync();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search));
        }
    }
}
