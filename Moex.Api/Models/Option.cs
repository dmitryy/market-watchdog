using Market.Common.Enums;
using System;

namespace Moex.Api.Models
{
    public class Option : Asset
    {
        public AssetCode Asset { get; set; }

        public DateTime Expire { get; set; }

        public int ExpireDays
        {
            get
            {
                return ((TimeSpan)(Expire - DateTime.Now)).Days + 1;
            }
        }

        public OptionType Type { get; set; }

        public Futures Futures { get; set; }

        public int Strike { get; set; }
    }
}
