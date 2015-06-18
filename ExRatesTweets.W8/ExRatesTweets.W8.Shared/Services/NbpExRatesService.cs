using ExRatesTweets.Common;
using System;
using System.Collections.Generic;
using System.IO;
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
        private const string URL = "http://www.nbp.pl/kursy/xml/LastA.xml";
        public DateTime RatesDate { get; set; }


        public async Task<List<Currency>> GetCurentRates()
        {
            var exchangeRates = new List<Currency>();

            var httpClient = new HttpClient();
            var xmlBytes = await httpClient.GetInputStreamAsync(new Uri(URL));

            //regarding that WP doesn't have iso8859-2 incoding, need to use own class
            //class generated via SL Encoding Generator program
            Encoding iso8859_2 = new Iso8859_2();
            var stream = new StreamReader(xmlBytes.AsStreamForRead(), iso8859_2);
            String xmlString = stream.ReadToEnd();

            XDocument xDoc = XDocument.Parse(xmlString);
            this.RatesDate = DateTime.Parse(xDoc.Element("tabela_kursow").Element("data_publikacji").Value);

            exchangeRates = (from item in xDoc.Descendants().Where(x => x.Name.LocalName == "pozycja")
                             select new Currency
                             {
                                 Code = item.Element("kod_waluty").Value,
                                 Name = item.Element("nazwa_waluty").Value,
                                 Rate = decimal.Parse(item.Element("kurs_sredni").Value.Replace(',', '.'))
                            }).ToList();

            return exchangeRates;
        }

        public Currency GetRatesForCurrency(string code)
        {
            throw new NotImplementedException();
        }
    }
}