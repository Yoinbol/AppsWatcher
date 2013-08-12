using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppsWatcher.Common.Models
{
    public class Session : SessionHeader
    {
        [DataMember]
        public Dictionary<ApplicationKey, ApplicationInfo> Applications { get; set; }
    }
}
