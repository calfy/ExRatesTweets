using System;
using System.Collections.Generic;
using System.Text;

namespace ExRatesTweets.W8.Interfaces
{
    public interface ITwitterService
    {
        bool SendTweet(string message);

        bool Login(string username, string password);
    }
}
