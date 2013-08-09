using System.Runtime.Serialization;

namespace AppsWatcher.Common.Responses
{
    public class SingleResponse<TData> : Response
    {
        [DataMember]
        public TData Data { get; set; }
    }
}
