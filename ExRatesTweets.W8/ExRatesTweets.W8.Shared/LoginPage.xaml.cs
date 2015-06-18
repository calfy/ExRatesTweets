using ExRatesTweets.W8.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tweetinvi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ExRatesTweets.W8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private TwitterService twitterService;
        public LoginPage()
        {
            this.InitializeComponent();
            loginGrid.Visibility = Visibility.Collapsed;
        }

        private void signinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.twitterService = TwitterService.Instance;

            var url = twitterService.GetAuthorizationUrl();
            webView.Navigate(new Uri(url));
            signinBox.Visibility = Visibility.Collapsed;
            loginGrid.Visibility = Visibility.Visible;
        }

        private void confirmPinBtn_Click(object sender, RoutedEventArgs e)
        {
            var pin = pinTextBox.Text;
            bool isAuthenticated = this.twitterService.Authorize(pin);
            if (isAuthenticated)
            {
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                MessageDialog dlg = new MessageDialog("Nie poprawny kod PIN. Wprowadź ponownie.");
                dlg.ShowAsync();

                //pinTextBox.Text = "";
                //webView.
            }
        }

        private void webView_FrameNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            MessageDialog dlg = new MessageDialog("Laduje");
            dlg.ShowAsync();
        }

        private async void pinTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            string[] scrollToTopString = new string[] { @"// Define the function: function scrollToTop() {document.body.scrollTop = 0;} // now call the function! scrollToTop();" };
            await this.webView.InvokeScriptAsync("eval", scrollToTopString);
        }
    }
}
