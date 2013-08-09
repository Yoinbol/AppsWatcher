using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AppsWatcher.Common.Models
{
    public class Session : DomainModel
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public DateTime Day { get; set; }

        [DataMember]
        public Dictionary<ApplicationKey, ApplicationInfo> Applications { get; set; }

        public Session()
        {
            this.Applications = new Dictionary<ApplicationKey, ApplicationInfo>();
        }
    }
}
