using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Moex.Api.Contracts.History
{
    public class HistoryCursor
    {
        [DeserializeAs(Name = "columns")]
        public List<string> Columns { get; set; }

        [DeserializeAs(Name = "data")]
        public List<List<int>> Data { get; set; }

        /// <summary>
        /// Current cursor index
        /// </summary>
        public int Index
        {
            get
            {
                return Data[0][0];
            }
        }

        /// <summary>
        /// Total records in history response
        /// </summary>
        public int Total
        {
            get
            {
                return Data[0][1];
            }
        }

        /// <summary>
        /// Size of the page
        /// </summary>
        public int Size
        {
            get
            {
                return Data[0][2];
            }
        }
    }
}
