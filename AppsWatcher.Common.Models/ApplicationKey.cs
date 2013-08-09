using System;
using System.Runtime.Serialization;

namespace AppsWatcher.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationKey : DomainModel, IEquatable<ApplicationKey>
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ApplicationName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ApplicationTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}$$$!!!{1}", ApplicationTitle, ApplicationName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ApplicationKey other)
        {
            return other != null && this.ToString().Equals(other.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ApplicationName.GetHashCode() ^ this.ApplicationTitle.GetHashCode();
        }
    }
}
