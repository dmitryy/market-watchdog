using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moex.Api.Contracts.History
{
    public class FuturesSecurities
    {
        [DeserializeAs(Name = "history")]
        public HistoryData History { get; set; }

        [DeserializeAs(Name = "history.cursor")]
        public HistoryCursor Cursor { get; set; }
    }
}
