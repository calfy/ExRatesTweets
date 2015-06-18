using ExRatesTweets.W8.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;
using Tweetinvi.Core.Interfaces.Credentials;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml.Controls;

namespace ExRatesTweets.W8.Services
{
    public sealed class TwitterService
    {
        private const string CONSUMER_KEY = "731m7GaKBh6ujc5LfzkpsF1pB";
        private const string CONSUMER_SECRET = "uJNfOSKf1UCjck0b4xTX1hAjOE2NG8Dp6qaVSmLZhMR0WCYGzn";

        private ITemporaryCredentials applicationCredentials;

        private static readonly TwitterService instance = new TwitterService();
        public static TwitterService Instance
        {
            get
            {
                return instance;
            }
        }

        public TwitterService()
        {
            // Store the application only credentials into a variable
            this.applicationCredentials = CredentialsCreator.GenerateApplicationCredentials(CONSUMER_KEY, CONSUMER_SECRET);          
        }

        public bool Authorize(string pinCode)
        {
            // Generate the credentials based on the given PIN Code
            var userCredentials = CredentialsCreator.GetCredentialsFromVerifierCode(pinCode, applicationCredentials);

            if (userCredentials != null)
            {
                TwitterCredentials.SetCredentials(userCredentials);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PublishTweet(string message)
        {
            Tweet.PublishTweet(message);
        }

        public string GetAuthorizationUrl()
        {
            //// Get the URL that the user needs to visit to accept your application
            var url = CredentialsCreator.GetAuthorizationURL(applicationCredentials);
            return url;
        }
    }
}
