using Market.Common.Enums;

namespace Market.Common.Utils
{
    public class AssetUtils
    {
        /// <summary>
        /// Returns enum value by string equivalent
        /// </summary>
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

                case "Sr":
                case "SR":
                    return AssetCode.Sr;

                default:
                    return AssetCode.Unknown;
            }
        }

        /// <summary>
        /// Returns base asset by asset code
        /// </summary>
        public static string GetAssetCodeString(AssetCode asset)
        {
            switch (asset)
            {
                case AssetCode.Ri:
                    return "RTS";

                case AssetCode.Sr:
                    return "SBRF";

                default:
                    return asset.ToString();
            }
        }
    }
}
