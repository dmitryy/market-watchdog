using Market.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moex.Api.Utils
{
    public class FuturesSecurityParser
    {
        private static readonly char[] Months = new char[] { 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'Q', 'U', 'V', 'X', 'Z' };

        public static (AssetCode, DateTime) Parse(string secId)
        {
            var asset = SecurityParser.GetAssetCode(secId.Substring(0, 2));
            var month = Months.ToList().IndexOf(secId[2]) + 1;
            var year = secId[3];
            var expire = SecurityParser.GetExpireDate(year, month, 3);

            return (asset, expire);
        }
    }
}
