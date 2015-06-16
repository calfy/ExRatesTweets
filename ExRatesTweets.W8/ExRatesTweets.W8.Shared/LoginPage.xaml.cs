using ExRatesTweets.W8.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tweetinvi;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }

        private void signinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.twitterService = new TwitterService();
            var url = twitterService.GetAuthorizationUrl();
            webView.Navigate(new Uri(url));
        }

        private void confirmPinBtn_Click(object sender, RoutedEventArgs e)
        {
            var pin = pinTextBox.Text;
            this.twitterService.Authorize(pin);

            Tweet.PublishTweet("Hello world");
        }
    }
}
