using System;
using System.Linq;
using System.Reflection;

namespace AppsWatcher.Common.Core
{
    internal static class Extensions
    {
        internal static ComponentDefinition ResolveComponentDefinition(this string value)
        {
            string[] parts = value.Split(',');

            return new ComponentDefinition
            {
                AssemblyName = parts[1].Trim(),
                ComponentName = parts[0].Trim()
            };
        }

        internal static Type TryGetForcedComponent(this Configuration.ModuleConfig module, string contractName)
        {
            var config =
                module.Ensure.OfType<Configuration.Component>()
                      .FirstOrDefault(
                          c => c.ContractName.Equals(contractName, StringComparison.InvariantCultureIgnoreCase));

            if (config != null)
            {
                var componentDefinition = config.Type.ResolveComponentDefinition();
                var assembly = Assembly.Load(new AssemblyName(componentDefinition.AssemblyName));
                return assembly.GetType(componentDefinition.ComponentName, true, true);
            }

            return null;
        }
    }
}
