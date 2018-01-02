using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ReturnMsgInfo
    {
        public string Error { get; set; }
        public string ErrorMsg { get; set; }
        public string MsgType { get; set; }
        public string MsgTo { get; set; }
        public string XsRequestId { get; set; }
        public string XsMsgId { get; set; }
        public string ZBRequestId { get; set; }
        public string ZBMsgId { get; set; }
    }
}
