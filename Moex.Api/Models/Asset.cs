using System;

namespace Moex.Api.Models
{
    /// <summary>
    /// Basic class for different asset types including derivatives
    /// </summary>
    public class Asset
    {
        public string BoardId { get; set; }

        public DateTime TradeDate { get; set; }

        public string SecId { get; set; }

        public decimal Open { get; set; }

        public decimal Low { get; set; }

        public decimal High { get; set; }

        public decimal Close { get; set; }

        public decimal OpenPositionValue { get; set; }

        public decimal Value { get; set; }

        public decimal Volume { get; set; }

        public decimal OpenPosition { get; set; }
    }
}
