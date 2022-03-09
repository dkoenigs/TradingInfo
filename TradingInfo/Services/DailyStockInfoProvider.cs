using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingInfo.Services
{
    public class DailyStockInfoProvider : IDailyStockInfoProvider
    {
        private readonly IDatabaseProvider _databaseProvider;

        public DailyStockInfoProvider(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public async Task<List<StockInfo>> GetDailyStockInfo(int count)
        {
            List<StockInfo> dailyStockInfo = await _databaseProvider.Get();

            if (dailyStockInfo != null)
            {
                dailyStockInfo = GetTopNStocks(dailyStockInfo, count);
            }

            return dailyStockInfo;
        }

        private List<StockInfo> GetTopNStocks(List<StockInfo> dailyStockInfo, int count)
        {
            if (count > dailyStockInfo.Count)
            {
                //throw error
                return null;
            }

            dailyStockInfo = dailyStockInfo.OrderByDescending(x => x.UnitPrice).ToList(); //overwrite

            List<StockInfo> topNStocks = new List<StockInfo>();

            int i = 0;
            foreach (StockInfo stockInfo in dailyStockInfo)
            {
                if (i != count)
                {
                    topNStocks.Add(stockInfo);
                } else
                {
                    break;
                }
                i++;
            }

            return topNStocks;
        }

    }
}
