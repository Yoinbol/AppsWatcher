using System.Runtime.Serialization;

namespace AppsWatcher.Common.Models
{
    public class User : DomainModel
    {
        [DataMember]
        public string Name { get; set; }
    }
}
