using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MessageResult
    {
        [JsonProperty("error")]
        public List<MessageResultErrorInfo> error { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; }
        [JsonProperty("detail")]
        public List<MessageResultInfo> detail { get; set; }
    }

    public class MessageResultErrorInfo
    {
        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }
        [JsonProperty("msgType")]
        public string MsgType { get; set; }
        [JsonProperty("msgTo")]
        public string MsgTo { get; set; }

    }

    public class MessageResultInfo
    {
        [JsonProperty("msgTo")]
        public string MsgTo { get; set; }
        [JsonProperty("msgType")]
        public string MsgType { get; set; }
        [JsonProperty("msgId")]
        public string MsgId { get; set; }
    }
}
