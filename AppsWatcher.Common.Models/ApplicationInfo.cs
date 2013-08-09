using System;
using System.Runtime.Serialization;

namespace AppsWatcher.Common.Models
{
    public class ApplicationInfo : ApplicationKey
    {
        [DataMember]
        public string ProcessName { get; set; }

        [DataMember]
        public TimeSpan Time { get; set; }

        [DataMember]
        public double Seconds { get; set; }
    }
}
