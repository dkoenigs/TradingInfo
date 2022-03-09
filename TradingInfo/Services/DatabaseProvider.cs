using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingInfo.Services
{
    public class DatabaseProvider : IDatabaseProvider
    {

        public DatabaseProvider()
        {
        }

        public async Task<List<StockInfo>> Get()
        {
            return DummyDBRetriever();
        }

        private List<StockInfo> DummyDBRetriever()
        {
            string[] stockTickers = new string[] { "Walmart", "RocketLab NZ", "Amazon", "Meta", "Target", "Citrix", "Bank of America", "Harris Teeter" };

            var rng = new Random();
            List<StockInfo> dailyStockInfo = new List<StockInfo>();

            foreach (string stock in stockTickers)
            {
                dailyStockInfo.Add(
                    new StockInfo
                    {
                        StockTicker = stock,
                        CompanyInfo = "company info",
                        UnitPrice = rng.Next(100, 500),
                        Date = DateTime.Now
                    }
                    );
            }

            return dailyStockInfo;
        }
    }
}
