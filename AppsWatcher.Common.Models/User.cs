using System.Runtime.Serialization;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Common.Models
{
    [Alias("Users")]
    public class User : DomainModel
    {
        [DataMember]
        [KeyProperty(Identity = true)]
        public int UserId { get; set; }

        [DataMember]
        public string UserLogin { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        public byte[] Password { get; set; }

        public int? Salt { get; set; }
    }
}
