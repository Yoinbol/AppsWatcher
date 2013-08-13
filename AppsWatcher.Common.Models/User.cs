using System.Runtime.Serialization;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Common.Models
{
    [Alias("Users")]
    public class User : DomainModel
    {
        [DataMember]
        public string UserLogin { get; set; }
    }
}
