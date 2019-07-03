using Market.Common.Enums;
using System;

namespace Moex.Api.Utils
{
    public static class SecurityParser
    {
        public static AssetCode GetAssetCode(string asset)
        {
            switch (asset)
            {
                case "Ri":
                case "RI":
                    return AssetCode.Ri;
                case "Br":
                case "BR":
                    return AssetCode.Br;
                case "Si":
                case "SI":
                    return AssetCode.Si;
                default:
                    return AssetCode.Unknown;
            }
        }

        public static DateTime GetExpireDate(char yearLastDigit, int month, int week)
        {
            int year = GetYearByLastDigit(yearLastDigit);
            var firstDayOfMonth = new DateTime(year, month, 1);
            int daysUntilThursday = ((int)DayOfWeek.Thursday - (int)firstDayOfMonth.DayOfWeek + 7) % 7;

            var thursday = firstDayOfMonth.AddDays(daysUntilThursday);
            var currentWeek = 1;

            while (month == thursday.Month && currentWeek++ != week)
            {
                thursday = thursday.AddDays(7);
            }

            return thursday;
        }

        private static int GetYearByLastDigit(char digit)
        {
            var current = 0;

            while (DateTime.Now.AddYears(current).Year.ToString()[3] != digit)
                current++;

            return DateTime.Now.AddYears(current).Year;
        }
    }
}
