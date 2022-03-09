using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingInfo.Controllers
{
    [ApiController]
    [Route("api/tradingInfo")]
    public class TradingInfo : ControllerBase
    {
        private static readonly string[] StockTickers = new[]
        {
            "Walmart", "RocketLab NZ", "Amazon", "Meta"
        };

        private readonly ILogger<TradingInfo> _logger;

        public TradingInfo(ILogger<TradingInfo> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public string GetDefault(int count)
        {
            return "Default";
        }


        [HttpGet]
        [Route("dailyStockInfo")]
        public IEnumerable<StockInfo> GetDailyStockInfo()
        {
            var rng = new Random();
            List<StockInfo> dailyStockInfo = new List<StockInfo>();
            foreach (string stock in StockTickers)
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
            return dailyStockInfo.ToArray();
        }
    }
}
