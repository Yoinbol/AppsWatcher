using System.Runtime.Serialization;

namespace AppsWatcher.Common.Responses
{
    public class Response
    {
        [DataMember]
        public bool Succed { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
