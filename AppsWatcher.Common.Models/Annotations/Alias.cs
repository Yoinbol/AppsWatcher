using System;

namespace AppsWatcher.Common.Models.Annotations
{
    public class Alias : Attribute
    {
        public string Value { get; set; }

        public Alias(string value)
        {
            this.Value = value;
        }
    }
}
