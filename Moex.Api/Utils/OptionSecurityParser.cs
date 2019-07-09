using Market.Common.Enums;
using Market.Common.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moex.Api.Utils
{
    public class OptionSecurityParser
    {
        private static readonly char[] CallMonths = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L' };

        private static readonly char[] PutMonths = new char[] { 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X' };

        private static readonly char[] WeeklyExpirations = new char[] { 'A', 'B', '-', 'D', 'E' }; // 1, 2, 4,5 - thursday of the month

        /// <summary>
        /// Parse option security id, according to https://www.moex.com/s205
        /// </summary>
        public static (AssetCode, OptionType, int, DateTime) Parse(string secId)
        {
            var asset = AssetUtils.GetAssetCode(secId.Substring(0, 2));
            var strike = Regex.Split(secId, @"\D+")[1];
            var ps = secId.Substring(secId.IndexOf(strike) + strike.Length);
            var month = ps[1];

            var optionType = GetOptionType(month);
            var expireDate = GetExpireDate(secId);

            return (asset, optionType, Convert.ToInt32(strike), expireDate);
        }

        public static OptionType GetOptionType(char month)
        {
            return CallMonths.Contains(month)
                ? OptionType.Call
                : OptionType.Put;
        }

        public static DateTime GetExpireDate(string secId)
        {
            var strike = Regex.Split(secId, @"\D+")[1];
            var ps = secId.Substring(secId.IndexOf(strike) + strike.Length);

            var isWeekly = ps.Length == 4;
            var type = GetOptionType(ps[1]);
            var month = GetMonth(type, ps[1]);
            var year = ps[2];
            var week = isWeekly ? WeeklyExpirations.ToList().IndexOf(ps[3]) + 1 : 3;

            return SecurityParser.GetExpireDate(year, month, week);
        }

        private static int GetMonth(OptionType type, char month)
        {
            return type == OptionType.Call
                ? CallMonths.ToList().IndexOf(month) + 1
                : PutMonths.ToList().IndexOf(month) + 1;
        }
    }
}
