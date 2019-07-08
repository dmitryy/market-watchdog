using RestSharp.Deserializers;

namespace Moex.Api.Contracts.History
{
    public class Securities
    {
        [DeserializeAs(Name = "history")]
        public HistoryData History { get; set; }

        [DeserializeAs(Name = "history.cursor")]
        public HistoryCursor Cursor { get; set; }
    }
}
