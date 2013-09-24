using System.Runtime.Serialization;

namespace AppsWatcher.Common.Models
{
    public class NewUser : User
    {
        [DataMember]
        public new string Password { get; set; }
    }
}
