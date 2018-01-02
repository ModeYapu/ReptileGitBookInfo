using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class JsonParser
    {
        
        public List<error> error { get; set; }
        public List<data> data { get; set; }
        //public string data;
    }
    public class error
    {
        public string errorMsg { get; set; }
        public string msgType { get; set; }
        public string msgTo { get; set; }
    }
    public class data
    {
        public List<error> error { get; set; }
        public string requestId { get; set; }
        public List<detail> detail { get; set; }
    }
    public class detail
    {
        public string msgType { get; set; }
        public string msgTo { get; set; }
        public string msgId { get; set; }
    }
}
