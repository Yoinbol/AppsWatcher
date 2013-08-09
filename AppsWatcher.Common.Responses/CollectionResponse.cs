using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppsWatcher.Common.Responses
{
    public class CollectionResponse<TData> : Response
    {
        [DataMember]
        public IEnumerable<TData> Data { get; set; }
    }
}
