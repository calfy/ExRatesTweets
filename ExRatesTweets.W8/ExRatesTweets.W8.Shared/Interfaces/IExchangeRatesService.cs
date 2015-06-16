using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExRatesTweets.W8
{
    interface IExchangeRatesService
    {
        Task<List<Currency>> GetCurentRates();
        Currency getRatesForCurrency(string code);
    }
}
