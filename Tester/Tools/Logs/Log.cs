using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Tools.Logs
{
    [DataContract]
    public class Log
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public TestLog.LogResult Result { get; set; }

        [DataMember]
        public string Date { get; set; }
    }
}
