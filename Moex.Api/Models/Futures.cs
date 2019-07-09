using Market.Common.Enums;
using System;

namespace Moex.Api.Models
{
    public class Futures : Asset
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
    }
}
