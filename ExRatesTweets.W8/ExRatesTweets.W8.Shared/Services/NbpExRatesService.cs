using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Web.Http;

namespace ExRatesTweets.W8
{
    public class NbpExRatesService : IExchangeRatesService
    {
        private const string URL = "http://www.nbp.pl/kursy/xml/a104z140530.xml";

        public async Task<List<Currency>> GetCurentRates()
        {
            var exchangeRates = new List<Currency>();

            var httpClient = new HttpClient();
            var xmlString = await httpClient.GetStringAsync(new Uri(URL));

            XDocument xDoc = XDocument.Parse(xmlString);
            var date = xDoc.Element("tabela_kursow").Element("data_publikacji").Value;

            exchangeRates = (from item in xDoc.Descendants().Where(x => x.Name.LocalName == "pozycja")
                            select new Currency
                            {
                                Code = item.Element("kod_waluty").Value,
                                Name = item.Element("kod_waluty").Value,
                                Rate = decimal.Parse(item.Element("kod_waluty").Value)
                            }).ToList();
            return exchangeRates;
        }

        public Currency getRatesForCurrency(string code)
        {
            throw new NotImplementedException();
        }
    }
}