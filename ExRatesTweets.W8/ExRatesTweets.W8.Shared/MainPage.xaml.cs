using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Tweetinvi;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ExRatesTweets.W8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IExchangeRatesService service;

        public MainPage()
        {
            this.InitializeComponent();

            this.service = new NbpExRatesService();
            var loggedUser = User.GetLoggedUser();
            this.userNameTextBlock.Text = loggedUser.Name;
            this.userImage.Source = new BitmapImage(new Uri(loggedUser.ProfileImageUrl));

            FillRatesList();

            //layout adjustment for WP
#if WINDOWS_PHONE_APP
            this.dateTextBlock.FontSize = 20;
            this.itemsGridView.Margin = new Thickness(20,0,0,0);
            this.userNameTextBlock.Visibility = Visibility.Collapsed;
            this.bottomAppBar.IsSticky = false;
#endif
        }

        private async void FillRatesList()
        {
            var rates = await this.service.GetCurentRates();
            itemsGridView.ItemsSource = rates;
            this.dateTextBlock.Text = String.Format("Kursy z dnia {0}", this.service.RatesDate.ToString("dd.MM.yyyy"));
        }

        private async void tweetBtn_Click(object sender, RoutedEventArgs e)
        {
            //show loading animation
            progressRing.IsActive = true;

            await Task.Yield();

            //cut message into around 140 characters strings
            var tweetStrings = new List<string>();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Kursy z {0}\n", this.service.RatesDate.ToString("dd.MM.yyyy"));
            foreach (var item in itemsGridView.SelectedItems)
            {
                var currency = (Currency)item;
                var nextString = String.Format("{0}: {1}\n", currency.Code, currency.Rate);
                //135 because we need to save a space for eventualy tweet fragment number indictor (1/n)
                if (stringBuilder.Length + nextString.Length > 135)
                {
                    tweetStrings.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }

                stringBuilder.Append(nextString);
            }
            //need to add last string to the list
            tweetStrings.Add(stringBuilder.ToString());

            if (tweetStrings.Count == 1)
            {
                Tweet.PublishTweet(tweetStrings[0]);
            }
            else
            {
                for (int i = tweetStrings.Count; i > 0; i--)
                {
                    var tweet = String.Format("{0} {1}", String.Format("({0}/{1})", i, tweetStrings.Count), tweetStrings[i - 1]);
                    Tweet.PublishTweet(tweet);
                }
            }
            progressRing.IsActive = false;

            MessageDialog dlg = new MessageDialog("Opublikowano tweet");
            dlg.ShowAsync();

            //clear selected items
            itemsGridView.SelectedIndex = -1;

        }

        private void selectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            itemsGridView.SelectAll();
        }

        private void unselectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            itemsGridView.SelectedIndex = -1;
        }
    }
}
