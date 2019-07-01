using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Moex.Api.Contracts.History
{
    public class OptionSecurities
    {
        [DeserializeAs(Name = "history")]
        public HistoryData History { get; set; }

        [DeserializeAs(Name = "history.cursor")]
        public HistoryCursor Cursor { get; set; }
    }
}
