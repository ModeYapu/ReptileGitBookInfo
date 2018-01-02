using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Message
    {
        [JsonProperty("msgType")]
        public string MsgType { get; set; }
        [JsonProperty("msgTo")]
        public string MsgTo { get; set; }
        [JsonProperty("msgContent")]
        public MsgContent MsgContent { get; set; }
    }

    public class MsgContent
    {
        [JsonProperty("msgSubject")]
        public string MsgSubject { get; set; }
        [JsonProperty("msgBody")]
        public string MsgBody { get; set; }
    }
}
