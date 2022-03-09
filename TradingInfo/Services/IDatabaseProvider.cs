using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingInfo.Services
{
    public interface IDatabaseProvider
    {
        Task<List<StockInfo>> Get();
    }
}
