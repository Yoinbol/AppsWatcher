using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Common.Models
{
    [Alias("Sessions")]
    public class Session : SessionHeader
    {
        [DataMember]
        public DateTime AddedOn { get; set; }

        [DataMember]
        public TimeSpan Duration { get; set; }

        [DataMember]
        public List<ApplicationTrack> Applications { get; set; }

        public Session() 
        {
            this.Applications = new List<ApplicationTrack>();
        }
    }
}
