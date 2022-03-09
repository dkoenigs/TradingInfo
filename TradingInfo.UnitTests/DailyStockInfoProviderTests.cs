using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingInfo;
using TradingInfo.Services;
using Xunit;

namespace DailyStockInfoProviderTests
{
    public class DailyStockInfoProviderTests
    {
        private readonly IDailyStockInfoProvider _dailyStockInfoProvider;
        private readonly IDatabaseProvider _databaseProvider;

        public DailyStockInfoProviderTests()
        {
            _databaseProvider = new DatabaseProvider(); //mock this later

            _dailyStockInfoProvider = new DailyStockInfoProvider(
                _databaseProvider
            );
        }

        [Fact]
        public async Task GetTopNStocks_CountGreaterThanN_ReturnsNull()
        {
            var stocks = await _dailyStockInfoProvider.GetDailyStockInfo(100);

            Assert.Null(stocks);
        }

        [Fact]
        public async Task GetTopNStocks_CountLessThanN_ReturnsCorrectStocks()
        {
            var stocks = await _dailyStockInfoProvider.GetDailyStockInfo(5);

            Assert.Equal(5, stocks.Count);
        }
    }
}
