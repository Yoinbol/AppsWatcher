using System;
using System.Runtime.Serialization;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Common.Models
{
    public class SessionHeader : DomainModel
    {
        [DataMember]
        [KeyProperty(Identity = true)]
        public long SessionId { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public DateTime Day { get; set; }
    }
}
