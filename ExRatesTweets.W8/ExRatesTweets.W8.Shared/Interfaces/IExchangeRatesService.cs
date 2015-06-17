using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExRatesTweets.W8
{
    interface IExchangeRatesService
    {
        DateTime RatesDate { get; }

        Task<List<Currency>> GetCurentRates();
        Currency GetRatesForCurrency(string code);
    }
}
