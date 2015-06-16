using System;
using System.Collections.Generic;
using System.Text;

namespace ExRatesTweets.W8
{
    public class NbpExRatesService : IExchangeRatesService
    {
        private const string URL = "http://www.nbp.pl/kursy/xml/a104z140530.xml";

        public List<Currency> getCurentRates()
        {
            throw new NotImplementedException();
        }

        public Currency getRatesForCurrency(string code)
        {
            throw new NotImplementedException();
        }
    }
}