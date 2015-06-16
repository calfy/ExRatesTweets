using System;
using System.Collections.Generic;
using System.Text;

namespace ExRatesTweets.W8
{
    interface IExchangeRatesService
    {
        List<Currency> getCurentRates();
        Currency getRatesForCurrency(string code);
    }
}
