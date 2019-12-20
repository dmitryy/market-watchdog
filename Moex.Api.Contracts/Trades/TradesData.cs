using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Moex.Api.Contracts
{
    public class TradesData
    {
        [DeserializeAs(Name = "data")]
        public List<List<string>> Data { get; set; }
    }
}
