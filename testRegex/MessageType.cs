using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRegex
{
    public enum MessageType
    {
        PHONE = 1,
        MAIL = 2,
        SMS = 3,
        FAX = 4,
        LYNC = 5
    }
    public class MessageInfo
    {
        public string ToId { get; set; }
        public string MsgContent { get; set; }
    }

}
