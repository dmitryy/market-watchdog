using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Moex.Api.Contracts.History
{
    public class HistoryData
    {
        [DeserializeAs(Name = "data")]
        public List<List<string>> Data { get; set; }
    }
}
