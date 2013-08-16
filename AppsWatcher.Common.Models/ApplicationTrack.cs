using System;
using System.Runtime.Serialization;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Common.Models
{
    [Alias("ApplicationTracks")]
    public class ApplicationTrack : DomainModel
    {
        [DataMember]
        public long SessionId { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public DateTime AddedOn { get; set; }

        [DataMember]
        public TimeSpan Duration { get; set; }

        [DataMember]
        public DateTime? LastModifiedOn { get; set; }
    }
}
