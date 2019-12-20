using System;

namespace Moex.Api.Models
{
    public class OptionTrade
    {
        public long TradeNo { get; set; }

        public string BoardName { get; set; }

        public string SecId { get; set; }

        public string TradeDate { get; set; }

        public string TradeTime { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string SysTime { get; set; }
    }
}
