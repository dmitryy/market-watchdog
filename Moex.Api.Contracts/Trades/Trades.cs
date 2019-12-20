using RestSharp.Deserializers;

namespace Moex.Api.Contracts
{
    public class Trades
    {
        [DeserializeAs(Name = "trades")]
        public TradesData TradesData { get; set; }
    }
}
