using System.Collections.Generic;
using System.Runtime.Serialization;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Common.Models
{
    [Alias("Sessions")]
    public class Session : SessionHeader
    {
        [DataMember]
        public Dictionary<ApplicationKey, ApplicationInfo> Applications { get; set; }
    }
}
