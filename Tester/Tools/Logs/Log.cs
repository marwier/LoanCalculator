using System.Runtime.Serialization;

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
