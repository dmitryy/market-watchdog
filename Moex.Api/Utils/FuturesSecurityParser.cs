using Market.Common.Enums;
using Market.Common.Utils;
using System;
using System.Linq;

namespace Moex.Api.Utils
{
    public class FuturesSecurityParser
    {
        private static readonly char[] Months = new char[] { 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'Q', 'U', 'V', 'X', 'Z' };

        /// <summary>
        /// Parse futures security id, according to https://www.moex.com/s205
        /// </summary>
        public static (AssetCode, DateTime) Parse(string secId)
        {
            var asset = AssetUtils.GetAssetCode(secId.Substring(0, 2));
            var month = Months.ToList().IndexOf(secId[2]) + 1;
            var year = secId[3];
            var expire = SecurityParser.GetExpireDate(year, month, 3);

            return (asset, expire);
        }
    }
}
