using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingInfo.Services;

namespace TradingInfo.Controllers
{
    [ApiController]
    [Route("api/tradingInfo")]
    public class TradingInfoController : ControllerBase
    {

        private readonly ILogger<TradingInfoController> _logger;
        private readonly IDailyStockInfoProvider _dailyStockInfoProvider;

        public TradingInfoController(ILogger<TradingInfoController> logger, IDailyStockInfoProvider dailyStockInfoProvider)
        {
            _logger = logger;
            _dailyStockInfoProvider = dailyStockInfoProvider;
        }


        [HttpGet]
        public string GetDefault(int count)
        {
            return "Default";
        }


        [HttpGet("{count}")]
        [Route("dailyStockInfo/{count}")]
        public async Task<IEnumerable<StockInfo>> GetDailyStockInfo(int count)
        {
            List<StockInfo> dailyStockInfo = await _dailyStockInfoProvider.GetDailyStockInfo(count);
            
            if (dailyStockInfo == null)
            {
                //_logger.Log(E"Error: No stocks returned.");
                return null;
            }

            return dailyStockInfo;
        }
    }
}
